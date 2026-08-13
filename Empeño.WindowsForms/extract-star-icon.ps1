# Extrae el ícono de estrella dorada de imageres.dll y lo guarda como .ico.
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing

$src = 'C:\Windows\System32\imageres.dll'
$outDir = 'C:\Temp\empeno-icons'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

Add-Type @"
using System;
using System.Drawing;
using System.Runtime.InteropServices;
public class IE {
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern uint PrivateExtractIcons(string szFileName, int nIconIndex, int cxIcon, int cyIcon, IntPtr[] phicon, uint[] piconid, uint nIcons, uint flags);
    [DllImport("user32.dll", SetLastError=true)]
    public static extern bool DestroyIcon(IntPtr hIcon);
    public static Icon Extract(string file, int index, int size) {
        IntPtr[] h = new IntPtr[1];
        uint[] id = new uint[1];
        uint n = PrivateExtractIcons(file, index, size, size, h, id, 1, 0);
        if (n > 0 && h[0] != IntPtr.Zero) {
            Icon icon = (Icon)Icon.FromHandle(h[0]).Clone();
            DestroyIcon(h[0]);
            return icon;
        }
        return null;
    }
}
"@ -ReferencedAssemblies System.Drawing

$candidatos = @(108, 181, 6, 51, 82, 97, 197, 3, 4, 5, 7, 12, 76, 96)
foreach ($idx in $candidatos) {
  try {
    $icon = [IE]::Extract($src, $idx, 256)
    if ($icon -ne $null) {
      $png = Join-Path $outDir "star-$idx.png"
      $bmp = $icon.ToBitmap()
      $bmp.Save($png, [System.Drawing.Imaging.ImageFormat]::Png)
      $bmp.Dispose()
      $icon.Dispose()
      Write-Output "  idx=$idx  OK"
    }
  } catch {
    Write-Output "  idx=$idx  ERROR"
  }
}
Write-Output "Salida: $outDir"
Get-ChildItem $outDir | Select-Object Name, Length | Format-Table -AutoSize | Out-String
