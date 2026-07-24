---
tags: [empeño, memoria-tecnica, modulos]
rama: Change_IVA
---

# Módulos Principales

Inventario de los módulos funcionales del sistema, cada uno respaldado por uno o más formularios y su lógica de code-behind.

## Empeños (alta y gestión del préstamo)

Formulario principal: `frmEmpeno.cs` (~2750 líneas — el "god-form" del sistema). Cubre alta de empeño, selección de tasa de interés, avalúo/bodegaje, reverso de pagos y anulación del empeño. Ver [[05 - Flujo de Negocio]] y [[10 - Pagos y Reversos]].

## Pagos

Formulario: `frmPagar.cs`. Cobro de abonos a principal e interés, cálculo de `MontoTotal` (base + bodegaje + avalúo), redención total del empeño. Ver [[10 - Pagos y Reversos]].

## Prórrogas

Formulario: `frmProroga.cs`. Extiende el vencimiento de un empeño. Modelo UPSERT: conserva solo la prórroga más reciente por empeño aunque la tabla soporte historial (`frmProroga.cs:55-94`).

## Vencidos y decomiso

Formularios: `frmVencidos.cs`, `frmArqueo.cs`. Gestión de empeños vencidos, extensión o decomiso administrativo de la prenda (`RetiradoAdministrador=true`).

## Tasas de interés / configuración de tiers

Formulario: `frmIntereses.cs`. Configura los tiers de `Interes` (porcentaje, avalúo%, bodegaje%, meses, rangos de monto). Es la única superficie de UI nueva agregada específicamente para avalúo/bodegaje en esta rama, además del campo `txtAvaluo` en `frmEmpeno`.

## Cierre de caja

Formulario: `frmCierreCaja.cs`. Cierre diario con cálculo de IVA sobre avalúo y bodegaje del día. Ver [[12 - IVA y Cierre de Caja]].

## Login y seguridad de acceso

Formulario: `frmLogin.cs`. Autenticación por contraseña en texto plano (ver [[13 - Riesgos Técnicos]]). `frmPIN.cs` gatea operaciones sensibles (como "Borrar Pago") por PIN.

## Dashboard

Formulario: `frmTablero.cs`, respaldado por el DTO `Models/Transaccion.cs`.

## Ver también

- [[00 - Índice del Proyecto]]
- [[05 - Flujo de Negocio]]
- [[08 - Formularios y Navegación]]
