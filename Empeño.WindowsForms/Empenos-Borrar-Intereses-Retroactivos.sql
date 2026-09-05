/* =====================================================================================
   BORRADO DE INTERESES RETROACTIVOS MAL GENERADOS
   -------------------------------------------------------------------------------------
   Los datos vienen migrados de otro sistema y solo se paso parte del historial. El
   barrido de la version 2.5.22 rellenaba TODOS los meses faltantes desde la fecha de
   apertura del empeno, asi que invento cuotas de anos que el cliente ya habia saldado
   en el sistema viejo. El sistema quedo cobrando de mas.

   QUE BORRA -- las TRES condiciones se cumplen a la vez o la cuota NO se toca:
     1. La cuota no tiene ni un colon pagado  (Pagado <= 0 y sin PagoId)
     2. Vence ANTES del ultimo interes que el cliente realmente pago
     3. Se creo mas de 6 meses DESPUES de su propio vencimiento

   QUE NO BORRA, NUNCA:
     - Ningun pago. La tabla Pagoes no se toca en ninguna linea de este script.
     - Ninguna cuota con plata encima.
     - Nada posterior al ultimo interes pagado.

   POR QUE 6 MESES: medido contra el respaldo real, el hueco entre creacion y
   vencimiento separa dos poblaciones sin solaparse. Lo legitimo del sistema viejo
   llega a 3 meses de atraso; lo inventado por el barrido arranca en 8 y llega a 218.

   COMO USARLO
     Paso 1  Respaldar la base. No es opcional.
     Paso 2  Correr el script tal cual. Termina en ROLLBACK: no cambia nada,
             solo muestra que HARIA.
     Paso 3  Revisar los cuadros. Si cuadra, buscar  >>> CAMBIAR AQUI <<<
             al final, cambiar ROLLBACK por COMMIT y volver a correrlo.
   ===================================================================================== */

SET NOCOUNT ON;
SET XACT_ABORT ON;

-- ---------------------------------------------------------------------------------
-- Conjunto a borrar. Se materializa UNA vez y todo lo demas lee de aqui, para que el
-- diagnostico y el borrado no puedan discrepar.
-- ---------------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#basura') IS NOT NULL DROP TABLE #basura;

;WITH activos AS (
    SELECT EmpenoId
    FROM Empenoes
    WHERE IsDelete = 0
),
piso AS (
    -- El ultimo interes que el cliente REALMENTE pago. Sin un solo pago no hay
    -- referencia de hasta donde estaba al dia, y sin referencia no se borra nada.
    SELECT EmpenoId, MAX(FechaVencimiento) AS UltimoPagado
    FROM Intereses
    WHERE Pagado > 0
    GROUP BY EmpenoId
)
SELECT
    i.InteresesId,
    i.EmpenoId,
    i.FechaVencimiento,
    i.FechaCreacion,
    p.UltimoPagado,
    DATEDIFF(month, i.FechaVencimiento, i.FechaCreacion) AS HuecoMeses,
    CAST(i.Monto + ISNULL(i.MontoBodega,0) + ISNULL(i.MontoAvaluo,0) AS decimal(18,2)) AS MontoTotal
INTO #basura
FROM Intereses i
JOIN piso    p ON p.EmpenoId = i.EmpenoId
JOIN activos a ON a.EmpenoId = i.EmpenoId
WHERE i.Pagado <= 0
  AND (i.PagoId IS NULL OR i.PagoId = 0)
  AND i.FechaVencimiento < p.UltimoPagado
  AND i.FechaCreacion > DATEADD(month, 6, i.FechaVencimiento);

-- ---------------------------------------------------------------------------------
-- 1. RESUMEN
-- ---------------------------------------------------------------------------------
PRINT '=== 1. RESUMEN ===';
SELECT
    COUNT(*)                  AS CuotasABorrar,
    COUNT(DISTINCT EmpenoId)  AS EmpenosAfectados,
    SUM(MontoTotal)           AS MontoLiberado,
    MIN(HuecoMeses)           AS HuecoMinimoMeses,
    MAX(HuecoMeses)           AS HuecoMaximoMeses
FROM #basura;

-- ---------------------------------------------------------------------------------
-- 2. GUARDAS DE SEGURIDAD -- las tres deben decir OK
-- ---------------------------------------------------------------------------------
PRINT '=== 2. GUARDAS DE SEGURIDAD ===';
SELECT 'Cuotas con algo PAGADO en el conjunto' AS Guarda,
       COUNT(*) AS Cantidad,
       CASE WHEN COUNT(*) = 0 THEN 'OK' ELSE '*** PARE, NO CORRA EL BORRADO ***' END AS Estado
FROM #basura b JOIN Intereses i ON i.InteresesId = b.InteresesId WHERE i.Pagado > 0
UNION ALL
SELECT 'Cuotas con PagoId en el conjunto',
       COUNT(*),
       CASE WHEN COUNT(*) = 0 THEN 'OK' ELSE '*** PARE, NO CORRA EL BORRADO ***' END
FROM #basura b JOIN Intereses i ON i.InteresesId = b.InteresesId
WHERE i.PagoId IS NOT NULL AND i.PagoId <> 0
UNION ALL
SELECT 'Cuotas posteriores al ultimo pago',
       COUNT(*),
       CASE WHEN COUNT(*) = 0 THEN 'OK' ELSE '*** PARE, NO CORRA EL BORRADO ***' END
FROM #basura WHERE FechaVencimiento >= UltimoPagado;

-- ---------------------------------------------------------------------------------
-- 3. DETALLE POR EMPENO -- revisar este cuadro antes de decidir
-- ---------------------------------------------------------------------------------
PRINT '=== 3. DETALLE POR EMPENO ===';
SELECT
    b.EmpenoId                                          AS Empeno,
    e.Estado,
    COUNT(*)                                            AS Cuotas,
    CONVERT(varchar(10), MIN(b.FechaVencimiento), 103)  AS VenciaDesde,
    CONVERT(varchar(10), MAX(b.FechaVencimiento), 103)  AS VenciaHasta,
    CONVERT(varchar(10), MIN(b.UltimoPagado), 103)      AS UltimoInteresPagado,
    CONVERT(varchar(10), MIN(b.FechaCreacion), 103)     AS SeCrearonEl,
    SUM(b.MontoTotal)                                   AS Monto
FROM #basura b
JOIN Empenoes e ON e.EmpenoId = b.EmpenoId
GROUP BY b.EmpenoId, e.Estado
ORDER BY COUNT(*) DESC;

-- ---------------------------------------------------------------------------------
-- 4. BORRADO
-- ---------------------------------------------------------------------------------
PRINT '=== 4. BORRADO ===';

DECLARE @pagosAntes int = (SELECT COUNT(*) FROM Pagoes);

BEGIN TRANSACTION;

    DELETE i
    FROM Intereses i
    JOIN #basura b ON b.InteresesId = i.InteresesId;

    PRINT 'Cuotas eliminadas: ' + CAST(@@ROWCOUNT AS varchar(20));

    -- Comprobacion final: la tabla de pagos tiene que haber quedado EXACTAMENTE igual.
    IF (SELECT COUNT(*) FROM Pagoes) <> @pagosAntes
    BEGIN
        PRINT '*** SE TOCARON PAGOS -- SE DESHACE TODO ***';
        ROLLBACK TRANSACTION;
        RETURN;
    END
    PRINT 'Pagos intactos: ' + CAST(@pagosAntes AS varchar(20));

    -- Marca para que la aplicacion no vuelva a intentar la reparacion.
    INSERT INTO Bitacoras (Fecha, Error, Mensaje, Valor)
    SELECT GETDATE(), 0, 'REPARACION-INTERESES-RETROACTIVOS-V2',
           '{"Modulo":"Intereses","Accion":"Reparacion de intereses retroactivos","Valor":"Ejecutada por script SQL"}';

/* >>> CAMBIAR AQUI <<<
   Tal como esta, deshace todo y no cambia nada: sirve para ver los numeros.
   Para aplicarlo de verdad, cambiar ROLLBACK por COMMIT.                        */
ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

PRINT '=== FIN ===';
DROP TABLE #basura;
