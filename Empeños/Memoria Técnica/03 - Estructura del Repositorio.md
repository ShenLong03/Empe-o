---
tags: [empeño, memoria-tecnica, repositorio]
rama: Change_IVA
---

# Estructura del Repositorio

Cómo está organizado el código fuente de la solución `Empeño.sln`, para orientarse rápido al navegar el repo.

## Proyectos de la solución

- **`Empeño.CommonEF`** — librería de entidades EF6 compartida (netstandard2.0, cross-targeting — ver [[02 - Arquitectura Técnica]]).
- **`Empeño.WindowsForms`** — la aplicación WinForms en sí (`OutputType=WinExe`).
- **`Empeños.Setup2`** — proyecto de instalación/setup (no auditado en profundidad).

## Carpetas relevantes

### `Empeño.CommonEF`

| Carpeta/archivo | Contenido |
|---|---|
| `Entities/*.cs` | Entidades POCO de EF6: `Empeno`, `Pago`, `Intereses`, `Interes`, `Prorroga`, `Vencimientos`, `CierreCaja`, `DetalleCierreCaja`, `Cliente`, `Empleado`, `Bitacora`, `Configuracion` |
| `Enum/Estado.cs` | Estados del empeño |
| `Enum/TipoPago.cs` | Tipo de pago (interés/principal) |
| `Enum/PlaceHolderType.cs` | Tipos de placeholder de UI |
| `Models/ValorBitacora.cs` | DTO del payload de auditoría |

### `Empeño.WindowsForms`

| Carpeta/archivo | Contenido |
|---|---|
| `Views/*.cs` (+ `.Designer.cs`) | Todos los formularios (código + diseño) |
| `Data/DataContext.cs` | `DbContext` principal |
| `Data/EmpenoMap.cs` | Configuración Fluent para las dos FKs de `Empeno` hacia `Empleado` (creador y editor) — `EmpenoMap.cs:22-24` |
| `Funciones/Funciones.cs` | Validaciones, helpers de UI de placeholders, gating por PIN, `SaveBitacora`, motor de acumulación de interés (`ReviewEmpeño`/`ReviewEmpeños`) |
| `Funciones/EmailFuncion.cs` | Envío de correo SMTP |
| `Migrations/Configuration.cs` | Configuración de migraciones EF |
| `SeedDb/*` | Bootstrap de roles (`Perfil`) y usuario super-admin; `ClienteSeedDb` está comentado (`Program.cs:84`) |
| `Reports/*`, `ViewReports/*` | Formularios de reportes RDLC (presencia por estructura; uso no reencontrado en esta rama — duda técnica) |
| `Models/Transaccion.cs` | DTO del dashboard, usado activamente por `frmTablero.cs` |

### Fuera del código

- `Empeños\Comprobantes\*.xlsx` — plantillas Excel de comprobantes y contratos, dependencia externa de archivo junto al ejecutable (ver [[09 - Integraciones Externas]]).
- `Empeños\Memoria Técnica\` — esta memoria técnica (Obsidian).

## Ver también

- [[00 - Índice del Proyecto]]
- [[02 - Arquitectura Técnica]]
- [[04 - Módulos Principales]]
- [[07 - Entidades y Modelos]]
