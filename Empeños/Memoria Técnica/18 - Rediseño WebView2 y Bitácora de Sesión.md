---
tags: [empeño, memoria-tecnica, webview2, rediseño, bitacora]
rama: Change_IVA
fecha: 2026-07-03
---

# 18 — Rediseño WebView2 y Bitácora de Sesión

Bitácora de la sesión de rediseño (2026-07-03) en la que el sistema Empeño ganó una **segunda versión de interfaz en WebView2**, construida **en paralelo** a la clásica (WinForms nativa), sin cambiar lógica de negocio ni base de datos. Es "el mismo sistema con otra ropa". La versión clásica queda **100% intacta** como paracaídas.

## Resumen ejecutivo

Se montó una capa de presentación nueva basada en **WebView2** (Chromium embebido): un shell SPA (barra lateral + topbar + contenido) dibujado con HTML/CSS/JS y alimentado con datos reales de EF a través de un puente `postMessage` ↔ C#. La regla de oro: **la vista nueva solo CONSULTA (read-only); toda ESCRITURA delega en el formulario clásico correspondiente**, para no reimplementar reglas de negocio. La app arranca directo en el **Tablero** nuevo (`frmLogin → frmShell`), con un toggle bidireccional clásico ↔ nuevo. Todo compila limpio con el MSBuild de Visual Studio 2026. El trabajo quedó respaldado en el fork `djarquin02/Empe-o` y propuesto a `ShenLong03` vía **PR #10**. Se generó el ejecutable de Release `Empeño.WindowsForms.Nueva.exe`.

## Arquitectura de las dos versiones

```
        Login  →  arranca en TABLERO (versión NUEVA WebView2)
                         │
      ┌──────────────────┴───────────────────┐
   VERSIÓN NUEVA (frmShell)          VERSIÓN CLÁSICA (frmInicio)
   sidebar+contenido en WebView2     app actual, 100% INTACTA
   "Volver a versión clásica"  ⇄  botón "Versión nueva" (menú)
```

- **`frmShell`** (`Views/frmShell.cs`): Form borderless que hostea un único `WebView2` con `Dashboard/shell.html`. Bridge `WebMessageReceived`: navegación, controles de ventana, WhatsApp (`wa.me` vía `Process.Start`), rango de gráfica, abrir forms clásicos, logout, volver a clásica.
- **Helpers de datos** (`Dashboard/*.cs`, SOLO LECTURA): `TableroData`, `EmpenosData`, `ClientesData`, `CajaData`, `ConfigData`, `ArqueoData`, `EmpleadosData`, `InteresesData`, `PagosData`, `ReportesData`. Arman objetos anónimos → JSON (Newtonsoft) → inyectados en el HTML.
- **HTML**: `Dashboard/shell.html` (SPA), `tablero.html` (tablero embebido en `frmTablero`), `login.html`.
- **Delegación**: los módulos que aún necesitan escritura abren el form clásico (`frmEmpeno`, `frmClientes`, `frmCierreCaja`, `frmArqueo`, `frmConfiguracionGeneral`, etc.) con su lógica intacta.

## Módulos migrados (primera pasada)

| Módulo | Vista nueva | Escrituras |
|---|---|---|
| Tablero | KPIs, dona por estado, morosidad por antigüedad, flujo ingresos/egresos, seguimiento + WhatsApp | — (read-only) |
| Empeños | Lista + filtros + búsqueda server-side + ficha (cuotas/pagos) | → `frmEmpeno` clásico |
| Clientes | Directorio + ficha + empeños del cliente + ganancias | → `frmClientes` clásico |
| Caja | Resumen del día (cobrado/prestado/flujo) + movimientos | → `frmCierreCaja` / `frmArqueo` |
| Reportes | Lanzador de reportes | → forms clásicos |
| Configuración | Datos del negocio (read-only) | → `frmConfiguracionGeneral` + Empleados/Intereses |

## Fixes aplicados en la sesión

| Área | Fix |
|---|---|
| **Bunifu** | Reemplazado el DLL de dll-files por el **legítimo 1.5.2** (MSIL/AnyCPU) en `libs/Bunifu_UI_v1.52.dll` con ruta relativa; arreglado el alias `ns1` → `Bunifu.Framework.UI` en 6 Designer |
| **Prórroga** | `frmProroga` ahora **mueve `FechaVencimiento`** (regla: `max(vencimiento, hoy) + días`); anti-clobber en `frmArqueo`/`frmVencidos` |
| **Cierre de caja** | Corregido el **doble conteo** de avalúo/bodegaje (línea "Intereses" sumaba `MontoTotal`) |
| **Seguridad (nuevo)** | PIN replicado al abrir forms desde el shell (no bypass); escape HTML (XSS); cerrar = `Application.Exit`; fallback a la clásica si falta el runtime de WebView2; guarda de config sin instalar |
| **Plata (nuevo)** | Interés del seguimiento/WhatsApp calculado sobre **`MontoPendiente`**, no sobre el monto original |
| **Layout** | Shell con `min-height:0` (el cuerpo scrollea de verdad); sidebar/topbar fijos; menú colapsable a solo iconos |

## Auditoría de paridad (clásico vs nuevo)

Se corrió una auditoría por módulo. **Reportes**: paridad completa. **Configuración**: aceptable (solo SMTP/clave/IVA quedan en el clásico). Se corrigieron 3 gaps altos de consulta: cuota con avalúo+bodegaje, clientes inactivos visibles, y rótulos honestos en Caja (cobrado/prestado en vez de "ingresos" mezclado).

## ⚠️ Bugs de plata PENDIENTES (confirmados, NO arreglados)

> Estos tres son **pre-existentes** en `Change_IVA` y quedaron para una próxima con luz verde de negocio. Comparten raíz con el hallazgo central de [[10 - Pagos y Reversos]] y [[11 - Avalúo y GAGY]]: los flujos de dinero no siempre compensan avalúo/bodegaje y no acotan el pendiente.

1. **Sobrepago no se registra** — `Views/frmPagar.cs:416`: `pago.Monto = aplicado - accAvaluo - accBodega`, con `aplicado = pagoIntereses - sobrante`; el excedente cobrado en efectivo se pierde del registro → caja corta.
2. **`MontoPendiente` negativo** — `Views/frmEmpeno.cs:341` y `:2970`: `MontoPendiente += montoEditado - Monto` sin piso; editar el monto hacia abajo lo deja negativo → envenena la generación de intereses y la cartera.
3. **Reverso duplicado** — `Views/frmEmpeno.cs:2141`: `tx.Rollback()` revierte la BD pero **no** el change-tracker en memoria del `_context`; un reintento tras fallo vuelve a aplicar el revés.

Ver también [[13 - Riesgos Técnicos]] y [[14 - Deuda Técnica]] (duplicación `frmTablero`↔`TableroData`, `shell.html`↔`tablero.html`; ~17 queries EF al cargar el tablero).

## Notas de despliegue

- **`App.config`** apunta a `(localdb)\MSSQLLocalDB` — hay que ajustar `Data Source` antes de correr contra la BD real/servidor.
- Requiere el **runtime de WebView2** (viene en Windows 11; si falta, cae a la clásica).
- El ensamblado se renombró a **`Empeño.WindowsForms.Nueva`** (ver `Properties/AssemblyInfo.cs`).

## Estado en Git y entrega

| Ítem | Detalle |
|---|---|
| Commit | `9deea79` — "feat(webview2): versión nueva WebView2 en paralelo a la clásica" (+3190 líneas), compila limpio |
| Remoto `origin` | `github.com/ShenLong03/Empe-o` — push directo **denegado (403)**, `djarquin02` no tiene escritura |
| Respaldo | Fork `github.com/djarquin02/Empe-o`, rama `Change_IVA` (`2c4563c..9deea79`) ✅ |
| PR a ShenLong03 | **[#10](https://github.com/ShenLong03/Empe-o/pull/10)** — base `Change_IVA`, head `djarquin02:Change_IVA` |
| Ejecutable | `bin/Release/Empeño.WindowsForms.Nueva.exe` (7.6 MB) — con HTML del dashboard, WebView2 + loaders y Bunifu incluidos |

---

# Continuación — Sesión 2026-07-04

> Segunda jornada sobre la versión WebView2. Se pulió la UI a fondo, se migraron los módulos que faltaban, se blindó la seguridad por PIN y se agregó tema claro + caché de preferencias. **Sin cambios de BD ni de lógica de negocio** (las escrituras nuevas reutilizan la lógica exacta del clásico, extraída a métodos *Headless*).

## Lo que se hizo

| Área | Detalle |
|---|---|
| **Bundle / coexistencia** | `AssemblyName` = `Empeño.WindowsForms.Nueva`; **ProductCode/PackageCode/UpgradeCode nuevos** + `ProductName` "Empeños Nueva Versión" en `Empeños.Setup2.vdproj` → la versión clásica y la nueva pueden **convivir instaladas** (carpeta, accesos y entrada de "Agregar/quitar" separados). El `.vdproj` **no compila con MSBuild** (requiere Visual Studio). |
| **Login + splash** | Migrados a WebView (`Views/frmLoginWeb.cs` + `Dashboard/login.html`), misma autenticación exacta. Fix crítico: al entrar usa `Hide()` **no** `Close()` (es la ventana de `Application.Run`; cerrarla mataba la app). Splash: `ReviewEmpeños` fire-and-forget (no bloquea el paso al dashboard). |
| **Botones segmentados** | Componente `.segbtn` (píldora unida) para Prórroga/Sacar, Prórroga/Retirar (Arqueo) y Editar/Desactivar (Empleados + Intereses). Antes se apilaban feo. |
| **Contadores / layout** | KPIs del día en Empeños + contadores en Clientes, **movidos al header** (no al toolbar) para no apretar; buscador ensanchado (competía con la píldora). |
| **Empeños** | Búsqueda **persiste al cambiar de chip** y **combina** texto+estado (`EmpenosData.Buscar` con `filtro`). **Vence automático** = fecha + `Meses` del plan (o +1 si el plan no define meses). Cuotas muestran **año** (`MMM yyyy`) para no confundir meses con mucho atraso. |
| **Migrados a WebView** | **Intereses** (modal CRUD) y **Empleados** (modal CRUD + lista inline en Configuración), reutilizando la lógica clásica vía `*Headless`. Diálogo de PIN (`frmPIN`) **repintado** al tema oscuro violeta. |
| **Caja** | Panel **"Movimientos de hoy"** (pagos + empeños del día). **Saldo inicial del cierre auto-calculado** = acumulado del cierre anterior (antes siempre 0, verificado en BD). |
| **Reportes** | Fechas Desde/Hasta **persisten entre reportes y entre sesiones**. |
| **Anular pago** | Nuevo en WebView (pestaña Pagos): `frmEmpeno.AnularPagoHeadless` reusa el **reverso EXACTO** del clásico (transaccional: revierte interés/vencimiento o repone principal y reactiva empeño). |
| **Diseño** | Fucsia difuminado en todo (sidebar/topbar/contenido/hero/modales/login). **Tema LIGHT completo** (shell + modales + login) con **toggle** ☀️/🌙 en el appbar. |
| **Caché de preferencias** | Archivo TXT real `%APPDATA%\Empeno\prefs.txt` (JSON, vía `frmShell.LeerPrefs/GuardarPrefs`). Persiste: **tema, fechas de reportes, rango del gráfico del tablero (8d…1A), y última búsqueda de Empeños (chip+texto)**. |
| **Calendario** | Date-picker propio (offline, temado) en **todos** los inputs de fecha, con cambio rápido de año (`«` `»`). Escribe en `dd/MM/yyyy` (no rompe el backend). |

## Seguridad — PIN solo Administrador

Se agregaron módulos **admin-only** en `frmPIN.cs` (sólo `Administrador`/`SuperUsuario`, **NO** Supervisor/Empleado), con handler round-trip en `frmShell`:

| Acción | Módulo PIN | Cuándo pide |
|---|---|---|
| Editar empeño | `Editar Empeño Admin` | al tocar **Editar** (antes solo al guardar) |
| Anular pago | `Borrar Pago Admin` | al confirmar la anulación |
| Entrar a Configuración | `Configuración Admin` | al tocar **Configuración** (no se abre si no pasa) |

## Fix importante — WebView2 `0x8007139F` (alternar clásico↔nuevo)

Al ir de la versión **clásica → nueva** salía "No se pudo cargar la versión nueva… HRESULT 0x8007139F" y caía al clásico. **Causa:** `frmLoginWeb` y `frmShell` creaban **cada uno** su `CoreWebView2Environment` sobre la **misma carpeta** de datos → múltiples entornos = conflicto. **Fix:** un **entorno único por proceso** (`Program.WebViewEnv()`, cacheado) reutilizado por ambos formularios (patrón recomendado por Microsoft).

## Estado de Git al cierre

- Rama `Change_IVA`, **ahead 3** de `origin` (sin pushear): `9deea79` (versión nueva WebView2) + `7671ece` (**fix**: agrega `frmLoginWeb.cs` que faltaba → build ya **no** roto) + `55777b5` (fuente del instalador WiX).
- ✅ El **commit roto quedó arreglado** — `frmLoginWeb.cs` ya está versionado.
- Falta: **push** de la rama y actualizar el **PR #10** cuando se decida.

## Instalador (WiX) — convive con la versión clásica ✅

La extensión "Microsoft Visual Studio Installer Projects" **NO está instalada** (el `.vdproj` no compila ni con MSBuild ni con `devenv`). Se generó el instalador con **WiX v5** (CLI de .NET, headless):

- **Fuente**: `Empeño.WindowsForms/Installer.wxs` (commiteado). Cosecha **todo `bin\Debug`** con `<Files Include="bin\Debug\**">`: exe + DLLs (EF, Newtonsoft, WebView2 Core/WinForms, Syncfusion, ReportViewer…) + **`WebView2Loader.dll`** (runtimes/win-x64/x86/arm64) + `Dashboard\*.html` + `Empeños\Comprobantes\*` + idiomas. 93 archivos.
- **Build**: `wix build Installer.wxs -arch x64 -o "Empeños Nueva V2.0.0.msi"` → **`Empeño.WindowsForms/Empeños Nueva V2.0.0.msi`** (~19 MB).
- **Instalación POR USUARIO** (`Scope="perUser"`, carpeta `%LOCALAPPDATA%\Programs\Tico Manager\Empeños Nueva Versión`). ⚠️ **Fix clave**: al principio era per-machine (Program Files) y **fallaba 1603 (acceso denegado)** al doble-clic sin admin → "no pasaba nada". Per-user **NO requiere administrador** y el doble-clic funciona. **Verificado**: instalado con `msiexec /qn` sin elevar (exit 0) y el exe instalado **arranca OK**.
- **Convivencia (verificado leyendo el MSI)**: `ProductName`="Empeños Nueva Versión", `UpgradeCode`=`{99FC9FF4-D354-49BB-8E48-7A2440CC6507}`, versión 2.0.0, `Manufacturer`="Tico Manager". Todo **distinto** del clásico → entrada aparte en "Agregar o quitar programas", carpeta y acceso directo propios; **NO pisa** la instalación vieja. Ambas usan la MISMA base de datos.
- **Instalar**: doble clic en el `.msi` (sin UAC). NOTA: per-user = por perfil de Windows; en locales con un solo usuario, perfecto.
- **Reconstruir**: `dotnet tool install --global wix --version 5.0.2` (WiX v7 exige aceptar la licencia de pago OSMF; por eso **v5**).

### Instalador `.EXE` (bundle WiX Burn) — el que se distribuye
`Empeño.WindowsForms/Bundle.wxs` → **`Empeño.WindowsForms/Empeños Nueva Setup.exe`** (~22 MB). Un solo doble-clic que:
1. Instala el **Runtime de WebView2** (`MicrosoftEdgeWebview2Setup.exe`, bootstrapper Evergreen) **solo si falta** (detecta `HKLM\...\EdgeUpdate\Clients\{F3017226-…}\pv`; si ya está, no lo toca y no fuerza UAC).
2. Instala la **app** (MSI per-user).
- Extensiones: `wix extension add -g WixToolset.BootstrapperApplications.wixext/5.0.2` + `WixToolset.Util.wixext/5.0.2` (⚠️ la v7 de las ext no es compatible con WiX v5).
- Build: `wix build Bundle.wxs -ext WixToolset.BootstrapperApplications.wixext -ext WixToolset.Util.wixext -arch x64 -o "Empeños Nueva Setup.exe"`.
- **Entregar a los locales el `.exe`** (incluye el runtime). El `.msi` suelto sirve si la PC ya tiene WebView2.

### ⚠️ Cadena de conexión — corregida
El commit de WebView2 (`816a96e`) había cambiado por error el `App.config` de **`Data Source=.`** a `(localdb)\MSSQLLocalDB`. Verificado en git: TODOS los commits originales usan **`Data Source=.`** (instancia local por defecto = la que usan los locales). Con `(localdb)` la versión nueva **no vería la base real**. **Revertido a `Data Source=.`** (commit `20c390d`) y regenerados MSI + `.exe`. Nota: en una PC de dev que use LocalDB, habría que ajustar; en los locales (`.`) anda directo.

## Pendientes al cierre

1. **Push** de `Change_IVA` + actualizar el **PR #10** (3 commits locales sin subir; decisión pendiente).
2. Los **3 bugs de plata pre-existentes** de arriba (sobrepago, `MontoPendiente` negativo, reverso duplicado) **siguen sin arreglar** — esperan luz verde de negocio.
3. **Código muerto** en `shell.html`: rama `cancelados`, `ecounts()`, `n-can` (quedaron al sacar el chip "Retirados").
4. **Probar con credenciales reales**: anular pago (interés y principal), bloqueo de no-admin en Configuración/Editar/Anular, alternancia clásico↔nuevo, e **instalar el `.msi`** en un local para confirmar la convivencia.

## Ver también

- [[00 - Índice del Proyecto]]
- [[04 - Módulos Principales]]
- [[10 - Pagos y Reversos]]
- [[13 - Riesgos Técnicos]]
- [[17 - Pendientes para SDD]]
