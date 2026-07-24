---
tags: [empeño, memoria-tecnica, arquitectura]
rama: Change_IVA
---

# Arquitectura Técnica

Stack tecnológico, capas (o ausencia de ellas) y decisiones estructurales del sistema Empeño.

## Stack

- **.NET Framework 4.7.2**, WinForms, `OutputType=WinExe` (`Empeño.WindowsForms.csproj:12`).
- **Entity Framework 6.4.4**, Code-First. `DataContext : DbContext` (`Data/DataContext.cs:13`), connection string name `"DefaultConnection"` (`Data/DataContext.cs:15`), inicializador `MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>` (`Data/DataContext.cs:17`).
- **Migraciones automáticas**: `Migrations/Configuration.cs:12` tiene `AutomaticMigrationsEnabled = true`, `Seed()` vacío, y no hay archivos de migración con timestamp → el esquema se aplica de forma silenciosa, sin migración revisable en control de versiones. Riesgo elevado tratándose de una base de datos financiera — ver [[13 - Riesgos Técnicos]].
- **Base de datos**: SQL Server. Cadena de conexión `Data Source=.;Initial Catalog=Empeno;Integrated Security=True;Connection Timeout=120` (`App.config:10`).
- **`Empeño.CommonEF`** es una librería `netstandard2.0` (`Empeño.CommonEF.csproj:4`) que referencia manualmente ensamblados de referencia de .NET Framework 4.7.2 vía `HintPath` (`Empeño.CommonEF.csproj:12-14`) — un hack de cross-targeting para poder compartir las entidades EF6 entre proyectos con distinto TFM.

## Rama `Change_IVA` vs `master`

Esta documentación describe **`Change_IVA`** (commit `2c4563c`, 2023-07-04, "consecutivo final") porque es la línea de desarrollo viva: incluye avalúo, bodegaje e IVA, funcionalidad requerida para cumplimiento con Hacienda de Costa Rica. `master` está aproximadamente 16 meses desactualizada respecto a `Change_IVA` y **no tiene** avalúo ni bodegaje. Cualquier análisis, fix o especificación SDD debe basarse en `Change_IVA`, no en `master`. Ver también [[16 - Decisiones Técnicas]].

## Dependencias NuGet clave

| Paquete | Uso |
|---|---|
| `Microsoft.Office.Interop.Excel` | Generación de comprobantes/contratos por COM — ver [[09 - Integraciones Externas]] |
| `ReportViewerControl.Winforms` | Reportes RDLC (presencia confirmada por estructura de carpetas; uso no reencontrado en la lectura de esta rama — duda técnica, ver [[17 - Pendientes para SDD]]) |
| `Newtonsoft.Json` | Serialización del payload de auditoría (`Bitacora.Valor`) |
| `CircularProgressBar`, `FontAwesome.Sharp`, `VisualBasic.PowerPacks`, `WinFormAnimation` | Componentes visuales WinForms |
| `Microsoft.SqlServer.Types` | Presente en el proyecto pero sin columnas espaciales usadas — probablemente dependencia transitiva (inferido) |

`frmEmpeno.cs:6-7` importa `Syncfusion.DocIO`/`Syncfusion.DocIO.DLS` pero no se encontró uso de esos tipos en el archivo — posible import muerto o feature a medio cablear. Ver duda técnica en [[17 - Pendientes para SDD]].

## Ausencia de capas

No hay inyección de dependencias, ni capa de repositorio/servicio: la lógica de negocio vive directamente en el code-behind de los formularios WinForms y en la clase "cajón de sastre" `Funciones`. Cada formulario instancia su propio `DataContext` (`new DataContext()`), y varios métodos crean contextos EF adicionales de forma ad-hoc (`using (DataContext temp = ...)`). El caso más delicado es el reverso de pago, que usa **dos contextos simultáneos** (`_context` y `_contextTemp`) — ver [[10 - Pagos y Reversos]].

No existe manejo explícito de transacciones (`DbContextTransaction`/`TransactionScope`) en ninguna operación de dinero revisada, por lo que las operaciones multi-paso no son atómicas.

## Generación de identificadores

Varios identificadores de negocio se generan en el cliente mediante `Max(...) + 1` en vez de identity/secuencia de base de datos:

- `EmpenoId` (`frmEmpeno.cs:100-112`)
- `Pago.Consecutivo` (`frmPagar.cs:226-238`)
- `Vencimientos.Consecutivo` (`frmVencidos.cs:209-229`)

Esto es una condición de carrera potencial con cajeros concurrentes — ver [[13 - Riesgos Técnicos]].

## Dinero como `double`

Todos los montos de dinero se modelan como `double`, no `decimal`, mitigado ad-hoc con `Math.Truncate` en algunos puntos. Riesgo de precisión de punto flotante en un sistema financiero — ver [[13 - Riesgos Técnicos]] y [[14 - Deuda Técnica]].

## Manejo de errores

Patrón recurrente de `catch (Exception) { }` vacío en toda la base de código, por ejemplo `Funciones.cs:168-170, 457-460, 574-577, 716-719`, `frmEmpeno.cs:614-616, 907-911`, `EmailFuncion.cs:189-193`. Esto oculta fallas silenciosamente, dificultando el diagnóstico en producción.

## Estado global estático

`Program.cs:16-26` mantiene estado de sesión como campos estáticos: `Program.Usuario`, `Program.EmpleadoId`, `Program.Cliente`, `Program.PerfilId`, `Program.Proroga`. Es un patrón típico de apps WinForms de este tamaño, pero acopla fuertemente los formularios a un estado global mutable.

## Pruebas automatizadas

No existen pruebas automatizadas en la solución.

## Ver también

- [[00 - Índice del Proyecto]]
- [[03 - Estructura del Repositorio]]
- [[13 - Riesgos Técnicos]]
- [[14 - Deuda Técnica]]
- [[16 - Decisiones Técnicas]]
