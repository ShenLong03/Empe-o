using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmTablero : Form
    {
        DataContext _context = new DataContext();
        WebView2 web;

        public frmTablero()
        {
            InitializeComponent();
        }

        private async void frmTablero_Load(object sender, EventArgs e)
        {
            try
            {
                web = new WebView2 { Dock = DockStyle.Fill };
                this.Controls.Add(web);
                web.BringToFront();

                // Carpeta de datos de usuario propia (evita escribir junto al ejecutable).
                string udf = Path.Combine(Path.GetTempPath(), "EmpenoWebView2");
                var env = await CoreWebView2Environment.CreateAsync(null, udf);
                await web.EnsureCoreWebView2Async(env);

                web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                web.CoreWebView2.Settings.IsStatusBarEnabled = false;
                web.CoreWebView2.WebMessageReceived += WebMessageReceived;

                // Los enlaces externos (wa.me) se abren en el navegador del sistema, no dentro del panel.
                web.CoreWebView2.NewWindowRequested += (s, a) =>
                {
                    a.Handled = true;
                    try { Process.Start(a.Uri); } catch { }
                };

                web.CoreWebView2.NavigationCompleted += async (s, a) =>
                {
                    if (!a.IsSuccess) return;
                    string json = JsonConvert.SerializeObject(BuildData());
                    await web.CoreWebView2.ExecuteScriptAsync("window.renderTablero(" + json + ")");
                };

                string html = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dashboard", "tablero.html");
                web.CoreWebView2.Navigate(new Uri(html).AbsoluteUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el tablero: " + ex.Message, "Tablero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Mensajes que el HTML envía a la app (cambio de rango, WhatsApp, contactado).
        private async void WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var msg = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(e.TryGetWebMessageAsString());
                string type = (string)msg["type"];
                if (type == "range")
                {
                    string serie = JsonConvert.SerializeObject(BuildSerie((int)msg["index"]));
                    await web.CoreWebView2.ExecuteScriptAsync("window.updateSerie(" + serie + ")");
                }
                else if (type == "whatsapp")
                {
                    AbrirWhatsApp((int)msg["id"]);
                }
                // "contactado": la persistencia (tabla SeguimientoContacto) se agrega en un paso posterior.
            }
            catch { }
        }

        #region Datos reales (EF)
        private object BuildData()
        {
            var hoy = DateTime.Today;
            var sig7 = hoy.AddDays(7);
            var m0 = new DateTime(hoy.Year, hoy.Month, 1);

            var activosQ = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido));
            double cartera = activosQ.Select(x => (double?)x.MontoPendiente).Sum() ?? 0;
            int activos = activosQ.Count();
            int nuevosMes = _context.Empenos.Count(x => !x.IsDelete && x.Fecha >= m0);

            var vencidosQ = _context.Empenos.Where(x => !x.IsDelete && x.Estado == Estado.Vencido && !x.RetiradoAdministrador);
            int vencidosCount = vencidosQ.Count();
            double montoRiesgo = vencidosQ.Select(x => (double?)x.MontoPendiente).Sum() ?? 0;

            int porVencerCount = _context.Empenos.Count(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente) && x.FechaVencimiento >= hoy && x.FechaVencimiento < sig7);
            double recaudoMes = _context.Pago.Where(p => p.Fecha >= m0 && p.TipoPago == TipoPago.Interes).Select(p => (double?)p.Monto).Sum() ?? 0;

            var estadoDefs = new[] { Estado.Vigente, Estado.Pendiente, Estado.Vencido, Estado.Cancelado, Estado.Retirado };
            var estados = estadoDefs.Select(s => new { n = s.ToString(), v = _context.Empenos.Count(x => !x.IsDelete && x.Estado == s) }).ToList();

            // Morosidad por antigüedad (sobre los vencidos).
            var vlist = vencidosQ.Select(x => new { x.FechaVencimiento, x.MontoPendiente }).ToList();
            var b1 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 1 && (hoy - v.FechaVencimiento.Date).Days <= 30).ToList();
            var b2 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 31 && (hoy - v.FechaVencimiento.Date).Days <= 60).ToList();
            var b3 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 61).ToList();
            var aging = new object[]
            {
                new { t = "1–30 días", v = b1.Sum(v => v.MontoPendiente), ct = b1.Count },
                new { t = "31–60 días", v = b2.Sum(v => v.MontoPendiente), ct = b2.Count },
                new { t = "60+ días", v = b3.Sum(v => v.MontoPendiente), ct = b3.Count },
            };

            var porVencer = _context.Empenos
                .Where(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente) && x.FechaVencimiento >= hoy && x.FechaVencimiento < sig7)
                .OrderBy(x => x.FechaVencimiento)
                .Select(x => new { x.EmpenoId, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, x.Descripcion, x.EsOro, x.MontoPendiente, pct = x.Interes.Porcentaje, x.FechaVencimiento })
                .Take(60).ToList()
                .Select(x => Seguimiento(x.EmpenoId, x.cli, x.tel, x.Descripcion, x.EsOro, x.MontoPendiente, x.pct, x.FechaVencimiento, hoy)).ToList();

            var vencidos = vencidosQ
                .OrderBy(x => x.FechaVencimiento)
                .Select(x => new { x.EmpenoId, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, x.Descripcion, x.EsOro, x.MontoPendiente, pct = x.Interes.Porcentaje, x.FechaVencimiento })
                .Take(60).ToList()
                .Select(x => Seguimiento(x.EmpenoId, x.cli, x.tel, x.Descripcion, x.EsOro, x.MontoPendiente, x.pct, x.FechaVencimiento, hoy)).ToList();

            string empleado = Program.Usuario != null ? Program.Usuario.Usuario : "Empleado";
            string fecha;
            try { fecha = hoy.ToString("dddd, d 'de' MMMM", new CultureInfo("es-ES")); }
            catch { fecha = hoy.ToString("dd/MM/yyyy"); }

            return new
            {
                fecha,
                empleado,
                kpis = new { cartera, empenosActivos = activos, nuevosMes, porVencer = porVencerCount, vencidos = vencidosCount, montoRiesgo, recaudoMes },
                serie = BuildSerie(1),
                estados,
                aging,
                porVencer,
                vencidos,
                contactados = new object[0]
            };
        }

        // Una fila de seguimiento (por vencer / vencido). monto = interés estimado del período.
        private object Seguimiento(int id, string cli, string tel, string prenda, bool oro, double monto, object pctRaw, DateTime venc, DateTime hoy)
        {
            double pct = Convert.ToDouble(pctRaw);
            return new
            {
                id,
                cli,
                tel,
                prenda,
                oro,
                monto = Math.Truncate(monto * pct / 100.0),
                dias = (venc.Date - hoy).Days
            };
        }

        // Serie ingresos/egresos por rango (0=8d,1=15d,2=1M,3=3M,4=6M,5=1A).
        private object BuildSerie(int idx)
        {
            var hoy = DateTime.Today;
            DateTime desde;
            string mode;
            switch (idx)
            {
                case 0: desde = hoy.AddDays(-7); mode = "d"; break;
                case 2: desde = hoy.AddDays(-29); mode = "d"; break;
                case 3: desde = hoy.AddDays(-84); mode = "w"; break;
                case 4: desde = hoy.AddMonths(-6); mode = "m"; break;
                case 5: desde = hoy.AddYears(-1); mode = "m"; break;
                default: desde = hoy.AddDays(-14); mode = "d"; break;
            }

            var ing = _context.Pago.Where(p => p.Fecha >= desde).Select(p => new { p.Fecha, p.Monto }).ToList();
            var egr = _context.Empenos.Where(x => !x.IsDelete && x.Fecha >= desde).Select(x => new { x.Fecha, x.Monto }).ToList();

            var labels = new List<string>();
            var ingresos = new List<double>();
            var egresos = new List<double>();

            Action<DateTime, DateTime, string> bucket = (d0, d1, lbl) =>
            {
                labels.Add(lbl);
                ingresos.Add(ing.Where(x => x.Fecha >= d0 && x.Fecha < d1).Sum(x => x.Monto));
                egresos.Add(egr.Where(x => x.Fecha >= d0 && x.Fecha < d1).Sum(x => x.Monto));
            };

            if (mode == "d")
                for (var d = desde.Date; d <= hoy; d = d.AddDays(1)) bucket(d, d.AddDays(1), d.ToString("dd/MM"));
            else if (mode == "w")
                for (var d = desde.Date; d <= hoy; d = d.AddDays(7)) bucket(d, d.AddDays(7), d.ToString("dd/MM"));
            else
                for (var d = new DateTime(desde.Year, desde.Month, 1); d <= hoy; d = d.AddMonths(1)) bucket(d, d.AddMonths(1), d.ToString("MMM"));

            return new { labels, ingresos, egresos };
        }

        private void AbrirWhatsApp(int empenoId)
        {
            var e = _context.Empenos
                .Where(x => x.EmpenoId == empenoId)
                .Select(x => new { x.Descripcion, x.MontoPendiente, x.FechaVencimiento, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, pct = x.Interes.Porcentaje })
                .FirstOrDefault();
            if (e == null) return;

            int dias = (e.FechaVencimiento.Date - DateTime.Today).Days;
            string estado = dias >= 0 ? ("vence en " + dias + " día(s)") : ("está vencido hace " + (-dias) + " día(s)");
            double interes = Math.Truncate(e.MontoPendiente * Convert.ToDouble(e.pct) / 100.0);

            string tel = new string((e.tel ?? "").Where(char.IsDigit).ToArray());
            if (tel.Length == 8) tel = "506" + tel;

            string mensaje = "Hola " + e.cli + ", su empeño #" + empenoId + " (" + e.Descripcion + ") " + estado
                + ". Puede pagar el interés de ₡" + interes.ToString("N0") + " para conservar su artículo. ¡Gracias!";
            string url = "https://wa.me/" + tel + "?text=" + Uri.EscapeDataString(mensaje);
            try { Process.Start(url); } catch { }
        }
        #endregion

        // Handlers cableados por el Designer (el tablero ahora se dibuja en el WebView2).
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
        private void panel13_Paint(object sender, PaintEventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
    }
}
