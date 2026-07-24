---
tags: [empeño, memoria-tecnica, inventario]
rama: Change_IVA
---

> Barrido exhaustivo (6 zonas, 223 operaciones brutas). Base para el servicio de lógica compartida — para no dejar NADA por fuera.

# Inventario Consolidado — Módulo Empeños

> **Base para el servicio compartido.** Consolida las 6 zonas (frmEmpeno, frmPagar, frmEmpeñoInteres/frmProroga/frmVencidos, Funciones/frmPIN/EmailFuncion, frmShell/Dashboard, Entidades+Comprobantes). Sin pérdida de operaciones. Solo se dedup el duplicado exacto entre zonas: `ReimprimirPagoPorId` y las propiedades calculadas `MontoTotal` aparecen en varias zonas y se listan una sola vez marcadas como **ambos**.
>
> **Convención de columnas:** Plata = toca/muestra dinero. Imp = imprime comprobante. PIN = pide PIN. Disp = clásico / dashboard / ambos.
>
> **Verdad transversal:** el Dashboard nuevo (`frmShell` + `Dashboard/*Data.cs` + `shell.html`) es **solo-lectura + lanzadera**. NINGUNA escritura de dinero, transición de estado ni impresión de comprobante de operación vive ahí: todo delega al clásico o reusa `ReimprimirPagoPorId`.

---

## 1. Inventario por categoría

### 1.1 Alta de empeño

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| Alta de empeño (crear) | Crea empeño (empeñoId==0): valida, persiste, crea 1ª cuota, imprime, correo al cliente | frmEmpeno.cs:299 (rama else 422-582) | Monto=montoNuevo; MontoPendiente=montoNuevo; FechaVencimiento=fecha.AddMonths(interes.Meses>0?Meses:mesesVenc); 1ª cuota Monto=Truncate(MontoPendiente*Porcentaje/100), MontoAvaluo=avaluoNuevo, MontoBodega=bodegajeNuevo; valida montoNuevo>0 | Sí (ComprobanteEmpeño + opcional Contrato + email) | Sí ("Empeño") | clásico |
| GetConsecutivo (próximo EmpenoId) | Devuelve Max(EmpenoId)+1 (o 1) para el nuevo empeño | frmEmpeno.cs:103 | Consecutivo = Max(EmpenoId)+1 | No | No | clásico |
| btnEmpeñar_Click (cargar cliente) | Carga cliente seleccionado al formulario | frmEmpeno.cs:256 | No | No | No | clásico |
| btnEmpeñar_Click_2 (nuevo empeño p/ cliente) | Resetea form, fecha hoy, carga cliente para nuevo empeño | frmEmpeno.cs:664 | No | No | No | clásico |
| **GAP: Nuevo empeño (dashboard)** | Botón "Nuevo empeño" DELEGA: abre frmEmpeno clásico maximizado | frmShell.cs:123→173 (openEmpenos) | No en dashboard | No en dashboard | No en dashboard | dashboard |

### 1.2 Cobro / Pago

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| Abrir Pagar (frmPagar) | Abre modal de pago con valorInteres preseleccionado de cuotas | frmEmpeno.cs:1480 (iconButton10_Click) | Suma valorInteres de cuotas seleccionadas; cobro real en frmPagar | No aquí | No aquí | clásico |
| Constructor frmPagar(id, valor) | Carga empeño, guarda valorInteres preseleccionado | frmPagar.cs:29 | Solo captura valorInteres | No | No | clásico |
| frmPagar_Load (cálculo inicial) | Base de cobro: valorInteres o cuota más vieja; montoMinimo=Σ saldos interés; próxima fecha | frmPagar.cs:814 | Sin selección=solo cuota más vieja (oldest-first); montoMinimo=Sum(MontoTotal-Pagado) | No | No | clásico |
| **Guardar() (orquestador de pago)** | Central: valida, cap montos, crea Pago Principal y/o cobra interés, decide cancelación/abono | frmPagar.cs:126 | Descuenta MontoPendiente-=pago.Monto; cap pagoMonto→montoPendiente; cap pagoIntereses→montoIntereses | Delega Print* | Sí ("Empeño") 128 | clásico |
| Crear Pago Principal (abono a capital) | Crea Pago TipoPago.Principal, baja MontoPendiente | frmPagar.cs:178 | Único punto donde baja capital: MontoPendiente-=pago.Monto (201) | Delega | Heredado | clásico |
| Abono parcial (MontoPendiente>=1) | Estado=Vigente, cobra interés asociado, imprime abono | frmPagar.cs:221 | Cobra interés vía PagaInteres; capital ya bajó | PrintAbono + PrintInteres | Heredado | clásico |
| Solo cobro de interés (pagoMonto==0) | Solo PagaInteres, no toca capital ni Estado | frmPagar.cs:230 | Cobro puro interés/bodegaje/avalúo | PrintInteres | Heredado | clásico |
| **PagaInteres (split proporcional + avance vencimiento)** | Núcleo del cobro de interés: aplica pago cuota por cuota oldest-first, split, avanza vencimiento | frmPagar.cs:254 | Where(Pagado<MontoTotal).OrderBy(FechaVenc); paga=Min(due,sobrante); split; cuota saldada→FechaVencimiento+1mes | PrintInteres si print=true | No propio | clásico |
| Split proporcional avalúo/bodegaje/interés | Reparte cada peso cobrado en 3 componentes según fracción de la cuota | frmPagar.cs:287 (y 392 en SetPagaInteres) | fraccion=paga/MontoTotal; accInteres/accAvaluo/accBodega=Truncate(Round(comp*fraccion)); residuo→pago.Monto | No | No | clásico |
| Selección de meses / oldest-first | Determina qué cuotas y en qué orden se pagan (más viejas primero) | frmPagar.cs:817 (y 278/383) | valorInteres preselec o cuota más antigua; consumo OrderBy(FechaVencimiento) | No | No | clásico |
| GetConsecutivo (nº comprobante pago) | Max(Consecutivo)+1 global sobre tabla Pago | frmPagar.cs:240 | Asigna correlativo del recibo (Max global, no por sucursal) | No | No | clásico |
| btnGuardarEmpeño_Click (disparador) | Handler botón Guardar → await Guardar() | frmPagar.cs:121 | Delega | Delega | Delega | clásico |
| KeyUp Enter → Guardar | Enter en interés/monto/pagacon dispara Guardar() | frmPagar.cs:778 (786/798/810) | Delega | Delega | Delega (PIN) | clásico |
| btnPagar (foco desde flecha abajo) | Flecha ↓ mueve foco al botón Pagar | frmEmpeno.cs:856 | No | No | No | clásico |
| **GAP: Cobrar/Abonar (dashboard)** | Botón "Cobrar/Abonar" DELEGA: abre frmEmpeno clásico (no pasa el id) | shell.html:323→frmShell.cs:334→123 | No en dashboard | No en dashboard | No en dashboard | dashboard |

### 1.3 Cancelación / Retiro (con pago)

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **Cancelación total** | MontoPendiente<1→Cancelado/Retirado, cobra interés final, borra cuotas impagas | frmPagar.cs:203 | SetPagaInteres(pagoIntereses,false); Estado=Cancelado, Retirado=true, FechaRetiro=Today; RemoveRange de Intereses con Pagado==0; <1=tolerancia redondeo | PrintRetiro (con/sin pagoInteres) | Heredado | clásico |
| SetPagaInteres (cobro interés en cancelación) | Gemelo de PagaInteres; devuelve el Pago; print opcional (false en cancelación) | frmPagar.cs:359 | Mismo split proporcional y avance de vencimiento; devuelve Pago para consolidar consecutivos | PrintInteres solo si print=true | No propio | clásico |

### 1.4 Prórroga

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **Prórroga - Guardar (crear/editar)** | Otorga o edita prórroga, mueve FechaVencimiento, marca Prorroga=true | frmProroga.cs:57 | No toca plata; Editar: FechaVencimiento+=(dias-diasAnteriores); Crear: base=max(FechaVenc,Hoy)+dias | No | No | clásico |
| Prórroga - Upsert Prorroga (INSERT/UPDATE) | ProrogaId>0→UPDATE, si no→INSERT | frmProroga.cs:73-108 | No (días+comentario) | No | No | clásico |
| Prórroga - Auditoría (SaveBitacora) | Bitácora Crear/Editar {EmpenoId,DiasProrroga,Comentario} | frmProroga.cs:119-124 | No | No | No | clásico |
| Prórroga - Carga inicial | Precarga última prórroga si existe; default 7 días | frmProroga.cs:38 | No | No | No | clásico |
| Prórroga - Cancelar | Cierra sin guardar; resetea flags | frmProroga.cs:133 | No | No | No | clásico |
| **Vencidos - Otorgar prórroga desde grilla + email** | Abre frmProroga, refresca FechaVencimiento, envía email al cliente | frmVencidos.cs:257 | No calcula; refresca FechaVencimiento desde BD | Email al cliente (no Excel) | No (rutina) | clásico |
| Vencidos - Auditoría prórroga | Bitácora Módulo 'Vencidos' Acción 'Prórroga' | frmVencidos.cs:293-302 | No | No | No | clásico |
| **GAP: Prórroga (dashboard)** | No existe pantalla propia; solo tarjeta "Cartera vencida"→frmVencidos clásico | frmShell.cs:201 | Solo clásico | Solo clásico | PIN "Empeño" para abrir clásico | clásico |

### 1.5 Vencimiento / Retiro administrativo

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| Vencidos - Carga inicial + ReviewEmpeños | Corre motor, carga vencidos no retirados, arma grilla | frmVencidos.cs:34 | Indirecto vía ReviewEmpeños (genera cuotas) | No | No | clásico |
| Vencidos - Totales y grilla (LoadDetalle) | Proyecta empeños + agrega columnas botón + totales | frmVencidos.cs:312 | Σ Monto, Σ intereses, totalProrroga, totalVencido, totalRetirados; MontoPendiente+Σ(MontoTotal-Pagado) | No | No | clásico |
| **Vencidos - Retiro administrativo + comprobante** | Marca RetiradoAdministrador, crea Vencimientos, imprime comprobante | frmVencidos.cs:189 | Consecutivo Vencimientos=Max+1; imprime MontoPendiente | Sí (PrintVencido) | No (rutina, solo confirmación) | clásico |
| Vencidos - Alta registro Vencimientos | Crea Vencimientos con consecutivo, fecha hoy | frmVencidos.cs:211-234 | Secuencia (no monetario) | No | No | clásico |
| Vencidos - Auditoría retiro admin | Bitácora 'Retiro Administrativo' {EmpenoId,Consecutivo} | frmVencidos.cs:237-242 | No | No | No | clásico |
| Vencidos - Comprobante individual (PrintVencido) | Comprobante Excel de vencimiento puntual | frmVencidos.cs:133 | Imprime MontoPendiente | Sí (ComprobanteVencimiento.xlsx) | No | clásico |
| Vencidos - Refresco selección tras acción | Recarga grilla, restaura fila | frmVencidos.cs:304-308 | No | No | No | clásico |
| Vencidos - Buscar por rango de fechas | Filtra vencidos por dtDesde..dtHasta | frmVencidos.cs:419 | No | No | No | clásico |
| Vencidos - Imprimir listado (Print) | Reporte de vencidos/retirados/prórroga | frmVencidos.cs:440→78 | Imprime saldoVencido/Retirado/Prórroga N2 | Sí (ComprobanteVencidos.xlsx) | No | clásico |
| Vencidos - Notificar/cierre por email | Envía tabla HTML de totales por correo (Print comentado) | frmVencidos.cs:56 | Transporta totales | No (email) | No | clásico |
| Vencidos - btnEliminar (residual) | Solo MessageBox con identificación; NO elimina | frmVencidos.cs:173 | No | No | No | clásico |
| Filtro empeños que vencen hoy (btnHoy) | Lista empeños con cuota/pendiente que vence hoy | frmEmpeno.cs:1594 | Filtra (Monto+Bodega+Avaluo)>Pagado con FechaVenc==hoy o MontoPendiente>0 | No | No | clásico |
| **GAP: Vencidos/retiro (dashboard)** | No hay pantalla propia; muestra "Perdido"=RetiradoAdministrador en lectura; opera solo el clásico | frmShell.cs:201 | Solo clásico | Solo clásico | PIN "Empeño" abre clásico | clásico |

### 1.6 Edición de empeño / cuota

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **Editar empeño (guardar cambios)** | Actualiza empeño existente (empeñoId>0); recalcula única cuota sin pagos | frmEmpeno.cs:299 (rama if 305-421) | MontoPendiente+=(montoEditado-Monto); Monto=montoEditado; si 1 cuota Pagado==0: interes.Monto=Monto*Porcentaje/100 y MontoBodega=Truncate(Monto*PorcentajeBodegaje); Fecha/venc solo si PerfilId!=4 | No | Sí ("Editar Empeño") 307 | clásico |
| **Editar cuota (interés) - Guardar override** | Override manual de la cuota (Pagado/Monto), o cambio de plan del empeño | frmEmpeñoInteres.cs:35 (btnImprimir_Click) | Valida nuevoPagado>=0; Rama A cambio plan; Rama B intereses.Monto=nuevoMonto; valida nuevoPagado<=MontoTotal; Pagado=nuevoPagado | No (pese al nombre) | Sí ("Editar Empeño") 37 | clásico |
| Editar cuota - Cambio de plan (recálculo condicional) | Cambia InteresId del empeño; recalcula si 1 sola cuota impaga | frmEmpeñoInteres.cs:60-76 | Monto=Empeno.Monto*Porcentaje/100; MontoBodega=Truncate(Empeno.Monto*PorcentajeBodegaje); avalúo NO se recalcula | No | Sí (heredado) | clásico |
| Editar cuota - Auditoría override | Snapshot antes/después en JSON, Módulo 'Intereses' Acción 'Editar' | frmEmpeñoInteres.cs:47-58,102-118 | No calcula (audita cambio de plata) | No | No propio | clásico |
| Editar cuota - Carga inicial | Llena planes activos, precarga datos de la cuota | frmEmpeñoInteres.cs:123 | txtTotal=Monto-Pagado (visual, sin bodegaje/avalúo) | No | No | clásico |
| Editar cuota - Cerrar/Cancelar | Cierra sin guardar | frmEmpeñoInteres.cs:30 / 138 | No | No | No | clásico |
| Abrir editar cuota (frmEmpeñoInteres) | Abre modal de edición de cuota pasando interesId | frmEmpeno.cs:1905 (iconButton3_Click) | Cálculo en frmEmpeñoInteres | No aquí | Sí ("Editar Pago") 1909 | clásico |
| btnEditarEmpeño_Click_1 (gate supervisor) | Carga empeño a editar; PIN solo si PerfilId==4 | frmEmpeno.cs:681 | Muestra Monto N2 | No | Sí condicional ("Editar Empeño") 687 | clásico |
| btnEditarEmpeño_Click (básico) | Carga empeño sin gate PIN ni grilla de pagos | frmEmpeno.cs:645 | Muestra Monto N2 | No | No | clásico |
| **GAP: Editar empeño (dashboard)** | Botón "Editar" (chip PIN informativo) DELEGA a frmEmpeno; PIN lo pide el clásico | shell.html:323→frmShell.cs:334→123 | No en dashboard | No en dashboard | Shell NO valida PIN (etiqueta visual) | dashboard |

### 1.7 Reverso / Borrado

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **Reverso/borrar pago o cuota (transaccional)** | Revierte pago o borra cuota en transacción, con bitácora antes/después | frmEmpeno.cs:2007 (iconButton4_Click) | Interés: reversa MontoTotal, Pagado→0, resta meses a FechaVenc, borra cuotas futuras impagas (InteresesId>max); Principal: MontoPendiente+=pago.Monto; bloquea borrar cuota Pagado>0 | No | Sí ("Borrar Pago") 2012 | clásico |
| **Eliminar/anular empeño (IsDelete)** | Borrado lógico IsDelete=true, guarda EditorId, imprime anulación | frmEmpeno.cs:1854 (btnEliminar_Click) | No recalcula (borrado lógico) | Sí (PrintAnulacion) | Sí ("Borrar Empeño") 1861 | clásico |
| **GAP: retirar/borrar (dashboard)** | Retiro/eliminación solo en clásico | frmShell.cs:171-181 | Solo clásico | Solo clásico | Solo clásico | clásico |

### 1.8 Búsqueda / Navegación / Carga

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| Buscar (cliente/empeño) | Busca cliente por texto y empeño por número; recalcula vía ReviewEmpeño | frmEmpeno.cs:117 | Muestra Monto/Pendiente N2; ReviewEmpeño recalcula | No | No | clásico |
| txtBuscar_TextChanged (autobúsqueda) | Dispara Buscar() con 3+ chars | frmEmpeno.cs:195 | Delega | No | No | clásico |
| txtBuscar_KeyUp (Enter/flechas) | Enter=Buscar; ←/→=empeño ant/sig; ↓=foco btnPagar; desbloquea si Anulado | frmEmpeno.cs:813 | Indirecto | No | No | clásico |
| txtBuscar_Enter/Leave (placeholder) | Gestiona placeholder ' Buscar' | frmEmpeno.cs:220/211 | No | No | No | clásico |
| BuscarEmpeño (revisar y cargar) | Resuelve empeñoId, ReviewEmpeño + ReviewDuplicate, LoadFormEmpeño | frmEmpeno.cs:977 | ReviewEmpeño recalcula | No | No | clásico |
| LoadFormEmpeño (pintar datos) | Carga campos, decide bloqueo y grilla según switchPago | frmEmpeno.cs:1007 | Muestra Monto N2 | No | No | clásico |
| btnVer_Click (solo lectura) | Carga empeño bloqueado | frmEmpeno.cs:2179 | Muestra Monto N2 | No | No | clásico |
| dgvEmpeños_DoubleClick | Abre empeño, avisa si retirado con fecha | frmEmpeno.cs:1262 | Indirecto | No | No | clásico |
| Cliente - Selección/búsqueda/alta | dgvClientes_DoubleClick (1427), btnIdentificacion (1411), txtIdentificacion_KeyUp (1307)/Leave (877), FindIdentification (1329), btnClienteNuevo (1565)/btnNewCustomer (1881)/iconButton1 (2613), btnEditar cliente (2591) | frmEmpeno.cs (varias) | No | No | No | clásico |
| Filtros de grilla empeños | btnPendientes (1642), comboBox1 estado (1900), LoadEmpeños (728), btnVerEmpleado (808), dgvEmpeños_CellFormatting (1688) | frmEmpeno.cs (varias) | Muestra Monto/Pendiente N2 | No | No | clásico |
| Grilla cuotas/pagos | CargarPagos() (1169), CargarPagos(id) (1217), LoadPays (2231), btnPagos (2214), btnIntereses (2248), btnPagados filtro (2576) | frmEmpeno.cs (varias) | Monto mostrado=Monto+MontoAvaluo+MontoBodega N2; filtro impagas Truncate(Pagado)+1<Truncate(total) | No | No | clásico |
| **Dashboard: Tablero KPIs** | Cartera, activos, nuevos, por vencer 7d, vencidos, monto en riesgo, recaudo interés mes | TableroData.cs:14/63-74 | Lectura: cartera=Σ MontoPendiente activos; montoRiesgo=Σ MontoPendiente vencidos; recaudoMes=Σ Pago interés del mes | No | No | dashboard |
| Dashboard: donut estados / aging / seguimiento | Conteos por estado; buckets 1-30/31-60/60+; listas por vencer/vencidos | TableroData.cs:32/35-44/46-92 | Lectura: monto=Truncate(MontoPendiente*(pct+bodegaje)/100), avalúo NO | No | No | dashboard |
| Dashboard: cambio de rango gráfica (range) | Recalcula serie ingresos vs egresos por período | frmShell.cs:93-96→Serie() 94 | Lectura: ingresos=Pago(Monto+Bodega+Avaluo); egresos=Empenos.Monto por bucket | No | No | dashboard |
| Dashboard: WhatsApp de cobro | Arma mensaje con monto a pagar, normaliza tel, abre wa.me | frmShell.cs:150→221 | Lectura: interes=Truncate(MontoPendiente*(pct+bodegaje)/100) | No | No | dashboard |
| Dashboard: Empeños lista/buscar/detalle | loadEmpenos (300 últimos), searchEmpenos (server-side), empenoDet (cuotas+pagos) | frmShell.cs:97-122; EmpenosData.cs:13/21/87 | Lectura: expone Monto/MontoAvaluo/MontoPendiente; cuota=Monto+Avaluo+Bodega | No | No | dashboard |
| Dashboard: Clientes lista/detalle | loadClientes (1000), clienteDet (empeños+ganancias) | frmShell.cs:124-135; ClientesData.cs:11/47 | Lectura: ganancias=Σ Pago.Monto del cliente | No | No | dashboard |
| Dashboard: Caja resumen del día (loadCaja) | Cobrado, intereses, abonos, prestado, flujo | frmShell.cs:137-142; CajaData.cs:12 | Lectura: intereses=Pago interés (Monto+Bodega+Avaluo); abonos=Pago principal; flujo=cobrado-prestado | No | No | dashboard |
| Dashboard: navegación/ventana | Load (36), min/max/close (87-89), clasico (90→164), nav (92→158) | frmShell.cs (varias) | No | No | No | dashboard |
| Dashboard: filtros/subtabs solo-UI | chips estado empeños (298-305), subtabs Cuotas/Pagos (321), buscar clientes (338), "done" contactado (293) | shell.html (varias) | No; "done" no persiste (contactados=[] en Build) | No | No | dashboard |
| Dashboard: WhatsApp al cliente (solo JS) | window.open a wa.me sin pasar por C# | shell.html:348/355 | No | No | No | dashboard |
| Dashboard: fallback preview (sin WebView2) | Datos de demo hardcodeados; no toca BD | shell.html:379-388 | No (demo) | No | No | dashboard |

### 1.9 Reimpresión / Comprobantes

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| Reimpresión de empeño | Reimprime empeño (o retiro si Cancelado) | frmEmpeno.cs:1823 | No (reimpresión) | Sí (Print/PrintRetiro) | No | clásico |
| Reimpresión de pago (grid) | Reimprime pago/cuota según tipo y estado | frmEmpeno.cs:2785 | No | Sí (PrintInteres/PrintRetiro/PrintAbono) | No | clásico |
| **ReimprimirPagoPorId (API pública)** | Reimprime pago por Id reusando lógica del grid; invocable desde el shell | frmEmpeno.cs:2845 | No (determina tipo por TipoPago/Estado) | Sí | No | **ambos** |
| Dashboard: reprintPago | Instancia frmEmpeno oculto y llama ReimprimirPagoPorId(id), luego Dispose | frmShell.cs:109-116→frmEmpeno.cs:2845 | No | Sí (reimprime) | No | dashboard |
| Dashboard: Reimprimir (botón ficha) | DELEGA: abre frmEmpeno clásico (no reimprime directo) | shell.html:324→frmShell.cs:334→123 | No | No directo | No | dashboard |
| **Comprobantes de impresión (clásico):** | | | | | | |
| Print (ComprobanteEmpeño.xlsx) | Ticket del empeño creado | frmEmpeno.cs:1732 | Monto N2; interés=Monto*Porcentaje/100; bodegaje=Monto*PorcentajeBodegaje; avalúo=Monto*PorcentajeAvaluo; estado 'Pendiente' | Sí | No | clásico |
| PrintContrato (ContratoEmpeños.xlsx) | Contrato largo (montos comentados) | frmEmpeno.cs:599 | No muestra montos (comentados) | Sí | No | clásico |
| PrintAnulacion (ComprobanteAnulacion.xlsx) | Comprobante de anulación | frmEmpeno.cs:1777 | Mismos cargos que Print; estado 'Anulada' | Sí | No | clásico |
| PrintAbono (ComprobanteAbono.xlsx) | Recibo de abono a principal | frmPagar.cs:533 (y frmEmpeno.cs:2265) | Saldo ant=MontoPendiente+pago.Monto; abono=pago.Monto; nuevo=MontoPendiente; total=pago.MontoTotal | Sí | No | clásico |
| PrintRetiro (ComprobanteCancelacion.xlsx) | Cancelación total; 3 sobrecargas (con pago / con pago+interés / sin pago) | frmPagar.cs:586/637; frmEmpeno.cs:2311/2358 | Σ Intereses.Monto; MontoPendiente; pago.MontoTotal; estado 'Retirado'; combina consecutivos | Sí | No | clásico |
| PrintInteres (ComprobanteInteres.xlsx) | Detalle interés por mes; 2 sobrecargas (Pago / pagoId nullable) | frmPagar.cs:688; frmEmpeno.cs:2414/2475 | Pagado N2 por cuota; total=Σ Pagado; MontoPendiente | Sí | No | clásico |
| PrintVencido (ComprobanteVencimiento.xlsx) | Comprobante individual de vencimiento | frmVencidos.cs:133 | MontoPendiente | Sí | No | clásico |
| Print vencidos (ComprobanteVencidos.xlsx) | Listado de vencidos/retirados/prórroga | frmVencidos.cs:78 | saldoVencido/Retirado/Prórroga N2 | Sí | No | clásico |
| Print cierre (ComprobanteCierreCaja.xlsx) | Cierre detallado del día | frmCierreCaja.cs:330 | saldoInicial; total; desglose por concepto | Sí | No | clásico |
| Print cierre IVA (Cierre.xlsx) | Resumen contable con IVA | frmCierreCaja.cs:459 | Acumulado, principal, avalúo, bodegaje, interés, abonos, vencimientos, cancelados, IVA | Sí | No | clásico |
| Print arqueo (ComprobanteArqueo.xlsx) | Arqueo de cartera por estado | frmArqueo.cs:79 | 7 saldos: principal/intereses/general/aldía/vencido/retirado/prórroga | Sí | No | clásico |

### 1.10 Estados (transiciones)

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| ChangeStatusEmpeño (recálculo por reglas, en memoria) | Decide Estado por fechas y cuotas; no persiste | frmEmpeno.cs:270 | Cuota con FechaVenc<hoy y Truncate(Round(Pagado))<Truncate(MontoTotal)→Pendiente | No | No | clásico |
| lblEstado_Click (gate PIN submenú) | Advierte supervisor, pide PIN, abre submenú de estados | frmEmpeno.cs:2632 | No | No | Sí ("Editar Empeño") 2643 | clásico |
| Cambiar estado a Vigente | Fuerza Vigente, limpia todas las marcas de retiro | frmEmpeno.cs:2661 | No (solo flags) | No | Indirecto (submenú) | clásico |
| Cambiar estado a Vencido | Fuerza Vencido, limpia marcas de retiro | frmEmpeno.cs:2692 | No | No | Indirecto | clásico |
| Cambiar estado a Cancelado | Fuerza Cancelado, marca retiro cliente (FechaRetiro=hoy, Retirado=true) | frmEmpeno.cs:2723 | No (marca retiro cliente) | No | Indirecto | clásico |
| Cambiar estado a Retirado/Perdido | Fuerza Retirado, marca retiro admin (FechaRetiroAdministrador=hoy) | frmEmpeno.cs:2754 | No (marca retiro admin) | No | Indirecto | clásico |
| ChangeState(label,estado) / (…,empeño) | Pinta etiqueta de estado (visual) | frmEmpeno.cs:1064 / 1108 | No | No | No | clásico |
| Transición: última cuota impaga→Pendiente / al día→Vigente | Motor: mira la última cuota tras generar | Funciones.cs:518 (y 627 individual) | Compara FechaVenc<hoy && Truncate(Round(Pagado))<Truncate(MontoTotal) | No | No | clásico |
| Transición: vencido→Cancelado/Retirado/Vencido | Motor: FechaVenc<hoy decide por flags de retiro | Funciones.cs:531 (masiva) / 640 (individual) | Retirado→Cancelado; RetiradoAdministrador→Retirado; [solo individual] MontoPendiente==0→Cancelado; else Vencido | No | No | clásico |

### 1.11 Motor / Cálculos (Funciones.cs)

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **ReviewEmpeños() (generación mensual masiva)** | Barrido de todos los activos: genera cuotas mes a mes hasta hoy, recalcula estado, bitácora 'Automatico' | Funciones.cs:463 | Por mes faltante: Monto=Truncate(MontoPendiente*Porcentaje/100), MontoBodega=Bodegaje!=null?Truncate(MontoPendiente*PorcentajeBodegaje):0; avalúo NO mensual | No | No (PIN en pantalla que invoca) | clásico |
| **ReviewEmpeño(int id) (individual)** | Igual que masiva para un empeño; tiene rama extra MontoPendiente==0→Cancelado | Funciones.cs:577 | Idéntica generación; rama extra de estado | No | No | clásico |
| ReviewDuplicateEmpeños() (dedup masivo) | Borra cuotas duplicadas (mismo empeño+FechaVenc, InteresesId mayor) | Funciones.cs:415 | Afecta saldos al eliminar duplicados; sin callers activos | No | No | clásico |
| **ReviewDuplicateEmpeños(id) (dedup + poda futuras)** | Dedup individual + borra cuotas futuras impagas (>hoy+1mes+1día, Pagado<=0) | Funciones.cs:440 | RemoveRange duplicados + poda futuras impagas | No | No | clásico |
| Cálculo interés mensual (Monto cuota) | Fórmula central del interés por mes | Funciones.cs:499 (dup 607) | Monto=Truncate(MontoPendiente*(Porcentaje/100)); sobre saldo, no monto original; trunca | No | No | clásico |
| Cálculo bodegaje mensual (MontoBodega) | Cargo mensual de bodegaje | Funciones.cs:500 (dup 608) | MontoBodega=Bodegaje!=null?Truncate(MontoPendiente*PorcentajeBodegaje):0; PorcentajeBodegaje=Bodegaje/100 | No | No | clásico |
| Avalúo (cargo único, NO mensual) | El motor mensual NO setea MontoAvaluo (queda 0); solo al crear el empeño | Funciones.cs:495 | Avalúo no se acumula mensual; PorcentajeAvaluo=Avaluo/100 no consumido por motor | No | No | clásico |
| **MontoTotal de la cuota (prop calculada)** | Total a pagar por cuota; insumo de decisión de estado | Intereses.cs:26 | MontoTotal=Monto+(MontoBodega??0)+(MontoAvaluo??0) | No | No | **ambos** |
| SaveBitacora (auditoría) | Crea fila Bitacora serializando ValorBitacora JSON | Funciones.cs:698 | No | No | No | **ambos** |
| EmailFuncion (envío de negocio) | SendMail empeño (18/39), cierre (60), arqueo (83), vencidos (110), SMTP base (154), hardcodeado Outlook (197) | EmailFuncion.cs | Muestra interés=Monto*Porcentaje/100 (no persiste). ⚠️ credenciales hardcodeadas en 203/212/238/247 | Email | No | clásico |

### 1.12 Configuración / Seguridad / Ciclo de vida

| Operación | Qué hace | Archivo:línea | Lógica de plata | Imp | PIN | Disp |
|---|---|---|---|---|---|---|
| **ValidatePIN(modulo) (gate de PIN)** | Abre frmPIN, espera, devuelve true/false por Program.Acceso | Funciones.cs:820 | No | No | Este ES el punto de PIN | **ambos** |
| frmPIN.Aceptar() (acceso base) | Valida Codigo==PIN; Administrador/SuperUsuario acceden a todo | frmPIN.cs:57 | No | No | Regla base | **ambos** |
| Matriz de roles frmPIN | Configuración (79, ambos), Cierre Caja (83), Arqueo (87), Empeño (91, ambos), Editar Empeño (95), Borrar Empeño (99), Borrar Pago (103), Editar Pago (107), Pago (111), Empleado (115, ambos), Cliente (119), default niega (123) | frmPIN.cs | No | No | Roles por módulo | ambos/clásico |
| Dashboard: openForm con PIN | arqueo/vencidos PIN "Empeño" (198/201); config/intereses PIN "Configuración" (202/204); empleados PIN "Empleado" (203); cierre/reportes SIN PIN (197/199/200) | frmShell.cs:197-204 | No en dashboard | No en dashboard | Shell valida PIN de apertura; ejecución en clásico | dashboard |
| Dashboard: loadConfig / editar config | Muestra datos del negocio (no IVA); editar delega a frmConfiguracionGeneral | frmShell.cs:143-148/202; ConfigData.cs:10 | No; NO expone IVA por decisión | No | Editar: PIN "Configuración" | dashboard |
| Dashboard: cierre/arqueo/reportes | Realizar cierre (frmCierreCaja SIN PIN 197), arqueo (PIN 198), Ingresos (199), Empeños (200), Cartera vencida (PIN 201) | frmShell.cs:149/197-201 | Cálculo en clásico | No en dashboard | Ver PIN por módulo | dashboard |
| CleanForm / btnCancelar (reset) | Resetea form, recalcula próximo nº y vencimiento | frmEmpeno.cs:1447 / 1302 | lblNumeroEmpeño=Max(EmpenoId)+1; lblVence=hoy+configuracion.Meses | No | No | clásico |
| GetInteresId (lookup plan por nombre) | Devuelve InteresId por nombre (0 si no) | frmEmpeno.cs:865 | No | No | No | clásico |
| Cálculo plan de interés por monto | txtMonto_TextChanged (1517), cbInteres (1936/969), InteresChanged (1945), SetupInteres (1982), Fecha_Leave (2562) | frmEmpeno.cs | Elige plan Mayor<=monto; txtAvaluo=Monto*PorcentajeAvaluo; txtBodegaje=Monto*PorcentajeBodegaje; Vence=hoy+Meses (solo nuevo) | No | No | clásico |
| frmEmpeno_Load (init) | Setea usuario/perfil, planes, config, fecha, próximo nº | frmEmpeno.cs:52 | lblNumeroEmpeño=Max(EmpenoId)+1; lblVence=hoy+Meses | No | No | clásico |
| Placeholders / formato / handlers vacíos | Enter/Leave, KeyNumber/FormatNumber (877-967, 2174), cbInteres (1584/1589), stubs/paints (varios), "en desarrollo" (239/244/249) | frmEmpeno.cs | txtMonto_Leave: FormatNumber; KeyNumber restringe numérico | No | No | clásico |
| **Entidades de dominio (campos de dinero):** | | | | | | |
| Empeno | Monto (principal), MontoAvaluo, MontoPendiente (saldo), Estado, FechaVencimiento=Fecha+3m, EsOro, InteresId | Empeno.cs:10 | Almacena principal/avalúo/saldo; sin MontoTotal calculado | No | No | ambos |
| Pago + MontoTotal | Monto, MontoAvaluo, MontoBodega, TipoPago, Consecutivo | Pago.cs:27 | MontoTotal=Monto+(MontoBodega??0)+(MontoAvaluo??0) | No | No | ambos |
| Intereses + MontoTotal | Monto, MontoBodega, MontoAvaluo, Pagado, PagoId | Intereses.cs:26 | MontoTotal=Monto+(MontoBodega??0)+(MontoAvaluo??0) | No | No | ambos |
| Interes + PorcentajeAvaluo/Bodegaje | Porcentaje (default 1), Avaluo, Bodegaje, Meses, Mayor/Menor/Igual, Activo | Interes.cs:25 | PorcentajeAvaluo=Avaluo/100; PorcentajeBodegaje=Bodegaje/100 | No | No | ambos |
| Prorroga | DiasProrroga, Fecha, Comentario (sin campos monetarios) | Prorroga.cs:9 | Solo plazo; efecto indirecto en generación de cuotas | No | No | ambos |
| Vencimientos | Consecutivo, Fecha (sin monto propio) | Vencimientos.cs:7 | Dinero desde el Empeno (MontoPendiente) | No | No | clásico |
| CierreCaja + DetalleCierreCaja | SaldoInicial, Detalles (Concepto, Valor) | CierreCaja.cs:9 | Total se arma en la View, no en la entidad | No | No | clásico |
| Configuracion + PorcentajeIVA | Encabezado empresa, Meses (default 3), IVA, SMTP | Configuracion.cs:32 | PorcentajeIVA=(IVA??0)/100; Meses fija plazo por defecto | No | No | ambos |

---

## 2. GAP: qué tiene el clásico y le FALTA al dashboard nuevo

El Dashboard es **solo-lectura + lanzadera**. Concretamente, NO hace (solo delega o ni siquiera eso):

1. **Alta de empeño** — "Nuevo empeño" abre frmEmpeno clásico (`frmShell.cs:123→173`). El dashboard no crea, no calcula la 1ª cuota, no imprime comprobante ni manda correo.
2. **Cobro de interés** — "Cobrar/Abonar" abre frmEmpeno clásico y **ni siquiera pasa el id** seleccionado (`shell.html:323→frmShell.cs:334`). Todo el split proporcional, oldest-first y avance de vencimiento vive en `frmPagar` clásico.
3. **Abono a principal** — Ídem: la baja de `MontoPendiente` solo ocurre en `frmPagar.cs:178/201`.
4. **Cancelación total / retiro con pago** — solo `frmPagar.cs:203` (Estado=Cancelado/Retirado, borra cuotas impagas). El dashboard no cancela.
5. **Prórroga** — no hay pantalla propia (`frmProroga` no existe en el shell); solo la tarjeta "Cartera vencida" abre `frmVencidos` clásico (`frmShell.cs:201`).
6. **Vencimiento / retiro administrativo** — muestra "Perdido"=RetiradoAdministrador **en lectura**, pero no permite retirar; el retiro real está en `frmVencidos.cs:189`.
7. **Edición de empeño** — "Editar" abre frmEmpeno; el chip "PIN" es **solo visual**, el shell NO valida PIN (`shell.html:323`).
8. **Edición manual de cuota (override)** — `frmEmpeñoInteres` no existe en el dashboard.
9. **Reverso/borrado de pago o cuota** — `iconButton4_Click` (`frmEmpeno.cs:2007`) solo en clásico.
10. **Eliminar/anular empeño (IsDelete)** — `frmEmpeno.cs:1854` solo en clásico.
11. **Cambio manual de estado** (submenú Vigente/Vencido/Cancelado/Retirado) — `frmEmpeno.cs:2661-2754` solo en clásico.
12. **Cierre de caja** — "Realizar cierre" abre `frmCierreCaja` clásico y **sin PIN en el shell** (`frmShell.cs:197`); el cálculo e impresión del cierre son del clásico.
13. **Impresión de comprobantes de operación** — contrato, empeño, abono, cancelación, interés, vencimiento, cierre, arqueo: todos en el clásico. **Única excepción:** el dashboard SÍ reimprime un pago puntual vía `reprintPago→ReimprimirPagoPorId` (`frmShell.cs:109→frmEmpeno.cs:2845`), pero reusando la lógica del clásico.
14. **El motor `ReviewEmpeños`/`ReviewEmpeño`** (generación de cuotas y transición de estado) — el dashboard solo lee estados/saldos ya calculados; nunca dispara la generación.
15. **Búsqueda server-side** existe en empeños (`searchEmpenos`), pero clientes es **solo filtro cliente-side** (`shell.html:338`).
16. **"Contactado" (done)** en seguimiento — es solo UI, **no persiste** (`contactados=[]` siempre en Build, `TableroData.cs:73`).

**En una línea:** el dashboard reimplementa toda la *lectura* (KPIs, listas, detalle, caja del día) pero **cero escritura de dinero, cero transición de estado, cero comprobante de operación** — todo eso sigue exclusivo del clásico.

---

## 3. Lógica de plata a centralizar en el servicio compartido

Estas son las operaciones que **tocan dinero de verdad** (crean/mutan Pago, Intereses, MontoPendiente, Estado) y que HOY viven duplicadas o dispersas en las Views clásicas. Deben migrar a un servicio compartido para que la versión nueva y la vieja usen **exactamente** la misma lógica.

### A. Cálculos base (fórmulas puras — hoy duplicadas)
1. **Interés mensual de la cuota:** `Monto = Truncate(MontoPendiente * (Porcentaje/100))`. Duplicada en `Funciones.cs:499` y `:607`, y replicada en frmEmpeno (alta/edición) y en los estimados del dashboard. **Sobre el saldo (MontoPendiente), no sobre el monto original, y truncado.**
2. **Bodegaje mensual:** `MontoBodega = Bodegaje!=null ? Truncate(MontoPendiente * PorcentajeBodegaje) : 0` (`PorcentajeBodegaje = Bodegaje/100`). Duplicada en `Funciones.cs:500`/`:608`. Ojo: con `Bodegaje=0` igual entra y da 0.
3. **Avalúo = cargo único, NO mensual.** Solo se asigna en el alta (frmEmpeno). El motor mensual NO lo acumula. Regla que debe quedar explícita en el servicio para no repetir el bug de sumarlo mes a mes.
4. **`MontoTotal` de cuota y de pago:** `Monto + (MontoBodega??0) + (MontoAvaluo??0)` (`Intereses.cs:26`, `Pago.cs:27`). Es el criterio de "cuota saldada". Ya es prop de entidad — mantenerla como fuente única.
5. **Selección del plan por monto:** plan cuyo `Mayor<=monto` (el último que cumple) — `frmEmpeno.cs:1517`. Determina Porcentaje/Avaluo/Bodegaje aplicables.

### B. Motor de generación y estado (hoy en Funciones.cs, con divergencia masiva vs individual)
6. **Generación de cuotas mes a mes** hasta hoy (`ReviewEmpeños` `:463` / `ReviewEmpeño` `:577`): cursor `proxima` desde la última FechaVencimiento, `AddMonths(1)` mientras `<= hoy`, crea `Intereses` con las fórmulas de A. **Unificar masiva e individual en una sola rutina.**
7. **Transición de estado por reglas** (`Funciones.cs:518/531` masiva vs `:627/640` individual): última cuota impaga→Pendiente / al día→Vigente; vencido→Cancelado/Retirado/Vencido por flags. ⚠️ **Divergencia a resolver:** la individual tiene la rama `MontoPendiente==0→Cancelado` que la masiva NO tiene. El servicio debe tener UNA sola tabla de transiciones.
8. **Dedup + poda de cuotas futuras impagas** (`ReviewDuplicateEmpeños` `:415`/`:440`): borra duplicados por (EmpenoId+FechaVenc) y poda futuras impagas (>hoy+1mes+1día, Pagado<=0). Afecta saldos — debe ser determinista y compartido.

### C. Cobro (el corazón, hoy 100% en frmPagar)
9. **Cobro de interés con split proporcional oldest-first** (`PagaInteres` `frmPagar.cs:254` y su gemelo `SetPagaInteres` `:359`): consumir cuotas `OrderBy(FechaVencimiento)`, `paga=Min(due,sobrante)`, repartir en interés/bodegaje/avalúo por `fraccion=paga/MontoTotal` (Truncate+Round), residuo de redondeo a `pago.Monto`, avanzar `FechaVencimiento+1mes` por cuota saldada. **`PagaInteres` y `SetPagaInteres` son casi idénticos — unificar en un método con flag `print`/`returnPago`.**
10. **Abono a principal:** creación de `Pago` TipoPago.Principal y **`MontoPendiente -= pago.Monto`** (`frmPagar.cs:178/201`) — único punto donde baja el capital.
11. **Cancelación total:** `MontoPendiente<1` → cobra interés final (SetPagaInteres) + `Estado=Cancelado, Retirado=true, FechaRetiro=Today` + `RemoveRange` de cuotas `Pagado==0` (`frmPagar.cs:203`). Incluye la tolerancia de redondeo `<1`.
12. **Reglas de validación de cobro** (hoy caps y guards en `Guardar`): cap `pagoMonto→MontoPendiente` (`:158`), cap `pagoIntereses→montoIntereses` (`:163`), regla "para abonar a capital hay que pagar TODO el interés pendiente" (`montoMinimo`, `:140` y `:172` con tolerancia `-1`).

### D. Reverso (hoy en frmEmpeno)
13. **Reverso/borrado transaccional de pago** (`frmEmpeno.cs:2007`): interés→revertir MontoTotal, `Pagado→0`, restar meses a FechaVenc, borrar cuotas futuras impagas; principal→`MontoPendiente += pago.Monto` y reabrir empeño (Vigente). Es la operación inversa exacta de C — debe vivir junto a ella para garantizar simetría.

### E. Numeración y auditoría (transversales)
14. **Consecutivo de pago:** `Max(Consecutivo)+1` global (`frmPagar.cs:240`). ⚠️ Es global, no por sucursal/tipo — decidir si se mantiene al centralizar.
15. **Consecutivo de vencimiento:** `Max+1` (`frmVencidos.cs:211`).
16. **Bitácora antes/después** (`SaveBitacora` `Funciones.cs:698`): ya es reutilizable (marcada **ambos**); el servicio debe llamarla en cada mutación de plata.

### Reglas de negocio que NO deben perderse al centralizar
- **PIN por operación** (`ValidatePIN` + matriz `frmPIN`): Alta/Pago=Empeño/Pago (Empleado+), Editar/Borrar Empeño, Borrar/Editar Pago = solo Admin/Supervisor. El servicio debe exponer el punto de autorización, no enterrarlo en la UI. Hoy el dashboard **no valida PIN de operación** (delega al clásico) — al centralizar, el gate debe ser del servicio.
- **Comprobantes** siguen siendo responsabilidad de la capa de presentación (Excel Interop), pero **el servicio debe entregar los mismos números** que hoy calculan Print/PrintAbono/PrintRetiro/PrintInteres, para que ambas versiones impriman idéntico.
- ⚠️ **Deuda de seguridad a resolver de paso:** credenciales SMTP hardcodeadas en `EmailFuncion.cs:203/212/238/247` (Outlook personal en claro). No es lógica de plata, pero está en el mismo módulo — anotar para rotar/eliminar.

---

**Archivos base del servicio compartido (por peso de lógica de dinero):**
- `Empeño.WindowsForms/Views/frmPagar.cs` — cobro, split, cancelación, abono (núcleo)
- `Empeño.WindowsForms/Funciones/Funciones.cs` — motor de generación + transiciones + interés/bodegaje
- `Empeño.WindowsForms/Views/frmEmpeno.cs` — alta, edición, reverso, cambio manual de estado
- `Empeño.WindowsForms/Views/frmEmpeñoInteres.cs` — override manual de cuota
- `Empeño.WindowsForms/Views/frmVencidos.cs` — retiro administrativo, prórroga desde vencidos
- `Empeño.WindowsForms/Views/frmProroga.cs` — prórroga (fechas)
- `Empeño.CommonEF/Entities/{Empeno,Pago,Intereses,Interes,Configuracion}.cs` — campos y props calculadas de dinero