---
tags: [empeño, memoria-tecnica, indice]
rama: Change_IVA
---

# Índice del Proyecto — Sistema Empeño

Mapa de contenidos (MOC) de la memoria técnica del sistema **Empeño**, una aplicación de escritorio WinForms para la gestión de casas de empeño en Costa Rica. Esta memoria documenta el estado del código en la rama **`Change_IVA`** (commit `2c4563c`, 2023-07-04, "consecutivo final"), que es la línea de desarrollo viva del proyecto.

## Resumen ejecutivo

El sistema Empeño gestiona el ciclo de vida completo de un préstamo prendario: alta, acumulación de intereses, pagos, prórrogas, vencimientos y decomisos. Es una app .NET Framework 4.7.2 + WinForms + Entity Framework 6 Code-First, sin capa de servicios, sin tests automatizados y con lógica de negocio embebida en el code-behind de los formularios (destaca `frmEmpeno.cs`, ~2750 líneas). La rama `Change_IVA` agrega avalúo, bodegaje e IVA como campos transversales sobre las entidades existentes (`Empeno`, `Interes`, `Intereses`, `Pago`) para cumplir con requisitos de Hacienda de Costa Rica. Esa misma decisión de diseño —agregar avalúo/bodegaje sin actualizar todos los flujos que tocan dinero— es la causa raíz del hallazgo más importante de esta auditoría: **el reverso de pagos no compensa avalúo ni bodegaje**, lo que subestima silenciosamente la deuda del cliente cada vez que se revierte un pago de interés. La rama `master` está ~16 meses desactualizada y NO tiene avalúo/bodegaje/IVA, por lo que no debe usarse como referencia para nada de esto.

## ⚠️ Hallazgo crítico

> El reverso de pagos ("Borrar Pago") ignora los montos de avalúo y bodegaje al descontar `Intereses.Pagado`, dejando el sistema con una deuda del cliente subestimada de forma permanente y sin registro de auditoría. Ver el detalle completo, la tabla de asimetría y el ejemplo numérico en:
> - [[10 - Pagos y Reversos]]
> - [[11 - Avalúo y GAGY]]

## Nota de persistencia (Engram)

La exploración de este repositorio se guardó en Engram bajo el proyecto **`escritorio`** (no `empeno`), porque el directorio de trabajo (cwd) era el Escritorio del usuario y "empeno" no está registrado como proyecto en Engram. Es una particularidad del flujo de trabajo del usuario, **no un bug de la aplicación Empeño**. Ver también [[16 - Decisiones Técnicas]].

## Mapa de notas

| Nota | Contenido |
|---|---|
| [[01 - Visión General del Sistema]] | Qué es Empeño, para quién, alcance funcional a alto nivel |
| [[02 - Arquitectura Técnica]] | Stack, EF6, WinForms, dependencias clave, decisión de rama `Change_IVA` vs `master` |
| [[03 - Estructura del Repositorio]] | Organización de carpetas y proyectos de la solución |
| [[04 - Módulos Principales]] | Inventario de módulos funcionales (empeños, pagos, prórrogas, vencimientos, cierre de caja) |
| [[05 - Flujo de Negocio]] | Ciclo de vida del préstamo prendario, de alta a redención/decomiso |
| [[06 - Flujo Técnico]] | Cómo se traduce el flujo de negocio en código: contextos EF, formularios, capas |
| [[07 - Entidades y Modelos]] | Modelo de datos completo: `Empeno`, `Pago`, `Intereses`, `Interes`, `Prorroga`, etc. |
| [[08 - Formularios y Navegación]] | Superficie de formularios WinForms (NO hay API REST — se aclara explícitamente) |
| [[09 - Integraciones Externas]] | Excel COM, SMTP, RDLC/ReportViewer |
| [[10 - Pagos y Reversos]] | ⚠️ DOCUMENTO CENTRAL — el bug del reverso de pagos |
| [[11 - Avalúo y GAGY]] | ⚠️ DOCUMENTO CENTRAL — qué es "GAGY", cómo se implementó el avalúo |
| [[12 - IVA y Cierre de Caja]] | Cálculo de IVA en el cierre de caja y la regresión detectada |
| [[13 - Riesgos Técnicos]] | Lista consolidada de riesgos con `file:line` |
| [[14 - Deuda Técnica]] | Deuda técnica estructural (sin tests, god-form, duplicación, etc.) |
| [[15 - Oportunidades de Mejora]] | Mejoras recomendadas, con plantilla por mejora |
| [[16 - Decisiones Técnicas]] | Decisiones técnicas observadas (ADRs implícitos) |
| [[17 - Pendientes para SDD]] | Dudas técnicas abiertas y candidatos a especificación SDD |

## Ver también

- [[01 - Visión General del Sistema]]
- [[10 - Pagos y Reversos]]
- [[11 - Avalúo y GAGY]]
