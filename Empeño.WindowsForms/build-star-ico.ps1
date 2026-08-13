# Genera una estrella dorada de 5 puntas como .ico multi-resolución (16/32/48/64/128/256)
# con relleno degradado dorado y borde oscuro. Guarda el .ico en la carpeta del proyecto
# (path con ñ) via stream sobre C:\Temp\ primero para evitar el bug de GDI+ con caracteres non-ASCII.
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing

$outDir = 'C:\Temp\empeno-icons'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

function New-StarBitmap {
    param([int]$size)
    $bmp = New-Object System.Drawing.Bitmap($size, $size, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
    $g.Clear([System.Drawing.Color]::Transparent)

    # Estrella de 5 puntas
    $cx = $size / 2.0
    $cy = $size / 2.0 + $size * 0.02   # ligero offset hacia abajo para centrar visualmente
    $R  = $size * 0.46                  # radio externo
    $r  = $R * 0.42                     # radio interno (proporción clásica de estrella)
    $points = New-Object 'System.Drawing.PointF[]' 10
    $startDeg = -90.0
    for ($i = 0; $i -lt 10; $i++) {
        $ang = ($startDeg + ($i * 36.0)) * [Math]::PI / 180.0
        $radius = if (($i % 2) -eq 0) { $R } else { $r }
        $x = $cx + $radius * [Math]::Cos($ang)
        $y = $cy + $radius * [Math]::Sin($ang)
        $points[$i] = New-Object System.Drawing.PointF([Single]$x, [Single]$y)
    }

    # Relleno con degradado dorado (amarillo brillante arriba -> ámbar profundo abajo)
    $rect = New-Object System.Drawing.RectangleF(0, 0, [Single]$size, [Single]$size)
    $c1 = [System.Drawing.Color]::FromArgb(255, 255, 220, 60)    # dorado brillante
    $c2 = [System.Drawing.Color]::FromArgb(255, 210, 130, 0)     # ámbar oscuro
    $brush = New-Object System.Drawing.Drawing2D.LinearGradientBrush($rect, $c1, $c2, [Single]90.0)
    $g.FillPolygon($brush, $points)

    # Borde oscuro para definición (más grueso en tamaños grandes, más fino en chicos)
    $penWidth = [Math]::Max(1.0, $size / 48.0)
    $penColor = [System.Drawing.Color]::FromArgb(255, 100, 55, 0)
    $pen = New-Object System.Drawing.Pen($penColor, [Single]$penWidth)
    $pen.LineJoin = [System.Drawing.Drawing2D.LineJoin]::Round
    $g.DrawPolygon($pen, $points)

    # Letra "E" central (marca de Empeños). En tamaños chicos se omite (no cabe legible).
    if ($size -ge 24) {
        $g.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::AntiAliasGridFit
        # Font size proporcional; buscamos que la E ocupe aprox el diámetro interno (~2*r)
        $fontFamily = 'Arial Black'
        try {
            $font = New-Object System.Drawing.Font($fontFamily, [Single]($size * 0.48), [System.Drawing.FontStyle]::Bold, [System.Drawing.GraphicsUnit]::Pixel)
        } catch {
            $font = New-Object System.Drawing.Font('Arial', [Single]($size * 0.48), [System.Drawing.FontStyle]::Bold, [System.Drawing.GraphicsUnit]::Pixel)
        }
        $fmt = New-Object System.Drawing.StringFormat
        $fmt.Alignment = [System.Drawing.StringAlignment]::Center
        $fmt.LineAlignment = [System.Drawing.StringAlignment]::Center
        # Rectángulo centrado en el CENTROIDE visual de la estrella (mismo cx/cy que usé para dibujarla)
        $textRect = New-Object System.Drawing.RectangleF([Single]($cx - $size/2), [Single]($cy - $size/2), [Single]$size, [Single]$size)
        # Sombra sutil (offset 1-2 px) para dar profundidad
        $shadowOffset = [Math]::Max(1.0, $size / 128.0)
        $shadowBrush = New-Object System.Drawing.SolidBrush([System.Drawing.Color]::FromArgb(90, 60, 30, 0))
        $shadowRect = New-Object System.Drawing.RectangleF([Single]($cx - $size/2 + $shadowOffset), [Single]($cy - $size/2 + $shadowOffset), [Single]$size, [Single]$size)
        $g.DrawString('E', $font, $shadowBrush, $shadowRect, $fmt)
        # Letra principal, ámbar muy oscuro (mismo tono del borde para consistencia)
        $textBrush = New-Object System.Drawing.SolidBrush([System.Drawing.Color]::FromArgb(255, 80, 40, 0))
        $g.DrawString('E', $font, $textBrush, $textRect, $fmt)
        $shadowBrush.Dispose(); $textBrush.Dispose(); $font.Dispose(); $fmt.Dispose()
    }

    $brush.Dispose(); $pen.Dispose(); $g.Dispose()
    return $bmp
}

$sizes = @(16, 32, 48, 64, 128, 256)
$pngBytes = New-Object 'System.Collections.Generic.List[byte[]]'
foreach ($sz in $sizes) {
    $bmp = New-StarBitmap $sz
    $ms = New-Object System.IO.MemoryStream
    $bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
    [void]$pngBytes.Add($ms.ToArray())
    $bmp.Dispose(); $ms.Dispose()
}

# Preview grande para que el dueño lo apruebe antes de shippear
$preview = New-StarBitmap 512
$previewPath = Join-Path $outDir 'estrella-preview.png'
$preview.Save($previewPath, [System.Drawing.Imaging.ImageFormat]::Png)
$preview.Dispose()

# Construir .ico multi-resolución con PNGs embebidos (formato Vista+)
$icoTmp = Join-Path $outDir 'estrella.ico'
$fs = New-Object System.IO.FileStream($icoTmp, [System.IO.FileMode]::Create)
$bw = New-Object System.IO.BinaryWriter($fs)

# ICONDIR (6 bytes): reservado=0, tipo=1 (ICO), count
$bw.Write([UInt16]0)
$bw.Write([UInt16]1)
$bw.Write([UInt16]$sizes.Count)

# Directorio de entradas (16 bytes cada una)
$headerSize = 6
$entrySize = 16
$offset = $headerSize + ($entrySize * $sizes.Count)
for ($i = 0; $i -lt $sizes.Count; $i++) {
    $sz = $sizes[$i]
    $w = if ($sz -eq 256) { [Byte]0 } else { [Byte]$sz }
    $h = if ($sz -eq 256) { [Byte]0 } else { [Byte]$sz }
    $bw.Write($w)
    $bw.Write($h)
    $bw.Write([Byte]0)                          # ColorPalette
    $bw.Write([Byte]0)                          # Reserved
    $bw.Write([UInt16]1)                        # Planes
    $bw.Write([UInt16]32)                       # BitsPerPixel
    $bw.Write([UInt32]$pngBytes[$i].Length)     # Size
    $bw.Write([UInt32]$offset)                  # Offset
    $offset += $pngBytes[$i].Length
}
# PNGs
for ($i = 0; $i -lt $sizes.Count; $i++) {
    $bw.Write($pngBytes[$i])
}
$bw.Close(); $fs.Close()

Write-Output ("ICO generado: " + $icoTmp + "  (" + (Get-Item $icoTmp).Length + " bytes)")
Write-Output ("Preview: " + $previewPath)
