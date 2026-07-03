---
tags: [empeño, memoria-tecnica, flujo-negocio]
rama: Change_IVA
---

# Flujo de Negocio

Ciclo de vida completo de un préstamo prendario en el sistema, de la alta a su cierre (redención o decomiso). Para el detalle técnico (código, contextos EF) ver [[06 - Flujo Técnico]].

## 1. Alta de empeño

Rama de guardado en `frmEmpeno.cs:427-566`. El operador ingresa el `Monto`, y el sistema sugiere una tasa (`Interes`) automáticamente según el rango de monto (`Interes.Mayor`, `frmEmpeno.cs:1509`), disparando `SetupInteres` (`frmEmpeno.cs:994-1012`) que pre-llena `txtAvaluo`/`txtBodegaje` como % del monto según el tier.

Al guardar: `Empeno.MontoAvaluo` se toma de `txtAvaluo` (`frmEmpeno.cs:486-487`); la primera fila de `Intereses` recibe explícitamente `MontoAvaluo`/`MontoBodega` (`frmEmpeno.cs:512-515`). Si hay avalúo/bodegaje configurado, se ofrece imprimir un "Contrato" (`frmEmpeno.cs:559-564`, `PrintContrato` en `frmEmpeno.cs:573-617`). El vencimiento (`FechaVencimiento`) se calcula como `Fecha + Meses`, tomando `Meses` de `Configuracion` por defecto o del tier `Interes.Meses` si está definido.

## 2. Acumulación mensual de interés

Motor: `Funciones.ReviewEmpeños` (`Funciones.cs:463-579`) y `ReviewEmpeño(int)` (`Funciones.cs:581-720`). Crea automáticamente una fila de `Intereses` por cada período de ~30 días transcurrido. `Monto = Truncate(MontoPendiente × Porcentaje%)`, y se calcula `MontoBodega` — **pero la línea de `MontoAvaluo` está comentada** (`Funciones.cs:497`), de modo que el avalúo solo se cobra en el primer período y luego queda en 0 indefinidamente. Puede ser un bug o intencional (si el avalúo es un cargo único) — es una duda de negocio abierta, ver [[17 - Pendientes para SDD]].

## 3. Pago

`frmPagar.Guardar` (`frmPagar.cs:122-224`). Divide el pago entre principal (`TipoPago.Principal`, decrementa `MontoPendiente`; si queda `< 1` → `Estado=Cancelado` + `Retirado=true`) e interés (`PagaInteres` en `frmPagar.cs:240-346` / `SetPagaInteres` en `frmPagar.cs:348-431`).

En el camino de interés, se recomponen los montos de avalúo/bodegaje para poblar `Pago.Monto`/`Pago.MontoAvaluo`/`Pago.MontoBodega`, y luego se asigna `pago.MontoTotal` (`frmPagar.cs:268`) contra las filas de `Intereses` pendientes, comparando/asignando `item.MontoTotal` (`frmPagar.cs:272,275,278`) e incrementando `item.Pagado` con el monto **avalúo-inclusive**. Cada escritura registra `SaveBitacora`.

Este es el punto de comparación clave con el reverso — ver la tabla de asimetría en [[10 - Pagos y Reversos]].

## 4. Prórroga

`frmProroga.cs:55-94` hace un UPSERT de `Prorroga`, activa `Program.Proroga`, y el llamador marca `Empeno.Prorroga = true` y notifica al cliente por correo.

## 5. Redención total

Dentro de `frmPagar.Guardar`: `Estado = Cancelado`, `Retirado = true`, `FechaRetiro = Today`, y se eliminan las filas de `Intereses` no pagadas restantes.

> **Bug detectado (`frmPagar.cs:195`)**: el filtro de limpieza compara `i.EmpenoId == empleadoId`, es decir, compara la columna de empeño con una variable de **empleado**. Debería ser `empeñoId`. Esto hace que la limpieza no elimine nada (no-op) o afecte filas incorrectas. Es un bug pre-existente, presente también en `master`.

## 6. Vencimiento / decomiso

`Funciones.ReviewEmpeño(s)` cambia `Estado` a `Vencido`/`Pendiente` según corresponda. `frmVencidos`/`frmArqueo` permiten extender o decomisar administrativamente (`RetiradoAdministrador = true`, `Estado = Retirado`), registrando en `Vencimientos` e imprimiendo/notificando.

## 7. Anulación del empeño (distinto de reverso de pago)

`frmEmpeno.btnEliminar_Click` (soft-delete del préstamo, marca `IsDelete = true`). **No debe confundirse** con "Borrar Pago" (reverso de un pago puntual) — son operaciones distintas descritas en formularios/botones distintos. Ver [[10 - Pagos y Reversos]] para el reverso de pago.

## Ver también

- [[00 - Índice del Proyecto]]
- [[06 - Flujo Técnico]]
- [[07 - Entidades y Modelos]]
- [[10 - Pagos y Reversos]]
- [[11 - Avalúo y GAGY]]
