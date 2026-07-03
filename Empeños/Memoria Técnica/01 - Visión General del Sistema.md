---
tags: [empeño, memoria-tecnica, vision-general]
rama: Change_IVA
---

# Visión General del Sistema

Qué es Empeño, a quién sirve y qué cubre funcionalmente, sin entrar en detalle técnico (eso está en [[02 - Arquitectura Técnica]] y siguientes).

## Qué es

**Empeño** es una aplicación de escritorio para la gestión operativa de una casa de empeños en Costa Rica: alta de préstamos prendarios, cobro de intereses, avalúo y bodegaje de la prenda, prórrogas, vencimientos, decomisos y cierre de caja diario. Es una app WinForms monolítica de un solo puesto/base de datos SQL Server compartida (no hay indicios de arquitectura multi-sucursal en el código revisado).

## Alcance funcional

- Registro de clientes y empleados.
- Alta de un empeño (préstamo con garantía prendaria), con selección de tasa de interés por rango de monto.
- Avalúo y bodegaje como cargos asociados al empeño (ver [[11 - Avalúo y GAGY]]).
- Acumulación mensual automática de intereses sobre el saldo pendiente.
- Cobro de pagos (abono a principal o a interés), impresión de comprobantes.
- Reverso de pagos ("Borrar Pago") — ver [[10 - Pagos y Reversos]] por el hallazgo crítico asociado.
- Prórrogas de vencimiento.
- Gestión de vencidos y decomiso administrativo de la prenda.
- Cierre de caja diario con cálculo de IVA sobre avalúo y bodegaje — ver [[12 - IVA y Cierre de Caja]].
- Impresión de comprobantes/contratos vía Excel y notificaciones por correo al cliente.

## Rama documentada

Esta memoria describe el estado de **`Change_IVA`** (commit `2c4563c`, 2023-07-04), no `master`. `master` está ~16 meses desactualizada y no incluye avalúo, bodegaje ni IVA — ver [[02 - Arquitectura Técnica]] para el detalle de por qué se eligió `Change_IVA` como fuente de verdad.

## Ver también

- [[00 - Índice del Proyecto]]
- [[02 - Arquitectura Técnica]]
- [[05 - Flujo de Negocio]]
