using Empeño.WindowsForms.Dashboard;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Reports;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    // VERSIÓN NUEVA (WebView2). Todo el shell (sidebar + contenido) vive en un WebView2.
    // No cambia lógica ni BD: solo dibuja y delega en el C# existente. La versión clásica
    // (frmInicio) queda intacta y se abre con "Volver a versión clásica".
    public class frmShell : Form
    {
        private readonly DataContext _context = new DataContext();
        private readonly Funciones.Funciones funciones = new Funciones.Funciones();
        private WebView2 web;

        public frmShell()
        {
            Text = "Empeños";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(19, 18, 48);
            MinimumSize = new Size(1000, 640);
            Load += frmShell_Load;
        }

        private async void frmShell_Load(object sender, EventArgs e)
        {
            try
            {
                // Instalación sin configurar: la versión clásica fuerza la pantalla de configuración.
                if (!_context.Configuraciones.Any())
                {
                    AbrirClasico();
                    return;
                }

                web = new WebView2 { Dock = DockStyle.Fill };
                Controls.Add(web);

                string udf = Path.Combine(Path.GetTempPath(), "EmpenoWebView2");
                var env = await CoreWebView2Environment.CreateAsync(null, udf);
                await web.EnsureCoreWebView2Async(env);

                web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                web.CoreWebView2.Settings.IsStatusBarEnabled = false;
                web.CoreWebView2.WebMessageReceived += OnMessage;
                web.CoreWebView2.NewWindowRequested += (s, a) => { a.Handled = true; try { Process.Start(a.Uri); } catch { } };
                web.CoreWebView2.NavigationCompleted += async (s, a) =>
                {
                    if (!a.IsSuccess) return;
                    string usuario = Program.Usuario != null ? Program.Usuario.Usuario : "Empleado";
                    string json = JsonConvert.SerializeObject(TableroData.Build(_context, usuario));
                    await web.CoreWebView2.ExecuteScriptAsync("window.renderTablero(" + json + ")");
                };

                string html = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dashboard", "shell.html");
                web.CoreWebView2.Navigate(new Uri(html).AbsoluteUri);
            }
            catch (Exception ex)
            {
                // Si WebView2 no está disponible (runtime faltante, etc.), no dejar al usuario atrapado:
                // se cae a la versión clásica, que siempre funciona.
                MessageBox.Show("No se pudo cargar la versión nueva, se abrirá la versión clásica.\n\n" + ex.Message,
                    "Empeños", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AbrirClasico();
            }
        }

        private async void OnMessage(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var m = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(e.TryGetWebMessageAsString());
                string type = (string)m["type"];
                switch (type)
                {
                    case "min": WindowState = FormWindowState.Minimized; break;
                    case "max": WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized; break;
                    case "close": Application.Exit(); break;
                    case "clasico": AbrirClasico(); break;
                    case "logout": Logout(); break;
                    case "nav": Navegar((string)m["module"]); break;
                    case "range":
                        string serie = JsonConvert.SerializeObject(TableroData.Serie(_context, (int)m["index"]));
                        await web.CoreWebView2.ExecuteScriptAsync("window.updateSerie(" + serie + ")");
                        break;
                    case "loadEmpenos":
                        {
                            string ej = JsonConvert.SerializeObject(EmpenosData.Lista(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderEmpenos(" + ej + ")");
                            break;
                        }
                    case "searchEmpenos":
                        {
                            string ej = JsonConvert.SerializeObject(EmpenosData.Buscar(_context, (string)m["q"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderEmpenos(" + ej + ")");
                            break;
                        }
                    case "reprintPago":
                        {
                            // Reusa la lógica de impresión probada del formulario clásico.
                            var f = new frmEmpeno();
                            await f.ReimprimirPagoPorId((int)m["id"]);
                            f.Dispose();
                            break;
                        }
                    case "empenoDet":
                        {
                            string dj = JsonConvert.SerializeObject(EmpenosData.Detalle(_context, (int)m["id"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderEmpenoDetalle(" + dj + ")");
                            break;
                        }
                    case "openEmpenos": AbrirEmpenoClasico(); break;
                    case "loadClientes":
                        {
                            string cj = JsonConvert.SerializeObject(ClientesData.Lista(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderClientes(" + cj + ")");
                            break;
                        }
                    case "clienteDet":
                        {
                            string cdj = JsonConvert.SerializeObject(ClientesData.Detalle(_context, (int)m["id"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderClienteDetalle(" + cdj + ")");
                            break;
                        }
                    case "openClientes": AbrirClientesClasico(); break;
                    case "loadCaja":
                        {
                            string kj = JsonConvert.SerializeObject(CajaData.ResumenHoy(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCaja(" + kj + ")");
                            break;
                        }
                    case "loadConfig":
                        {
                            string gj = JsonConvert.SerializeObject(ConfigData.Get(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderConfig(" + gj + ")");
                            break;
                        }
                    case "openForm": AbrirFormClasico((string)m["form"]); break;
                    case "whatsapp": AbrirWhatsApp((int)m["id"]); break;
                }
            }
            catch { }
        }

        // "tablero" ya vive en el shell. El resto de módulos AÚN NO migrados abren la versión
        // clásica (frmInicio), para no perder ninguna funcionalidad durante la migración.
        private void Navegar(string modulo)
        {
            if (modulo == "tablero") return;
            AbrirClasico();
        }

        private void AbrirClasico()
        {
            var inicio = new frmInicio();
            inicio.Show();
            Close();
        }

        // Las acciones de escritura de Empeños (nuevo / cobrar / editar / etc.) abren el formulario
        // CLÁSICO tal cual, con toda su lógica intacta. La versión nueva solo consulta.
        private void AbrirEmpenoClasico()
        {
            var f = new frmEmpeno();
            f.TopLevel = true;
            f.FormBorderStyle = FormBorderStyle.Sizable;
            f.WindowState = FormWindowState.Maximized;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void AbrirClientesClasico()
        {
            var f = new frmClientes();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        // Abre el formulario clásico correspondiente (Caja/Arqueo/Reportes/Config/etc.) como ventana propia.
        private void AbrirFormClasico(string name)
        {
            // Se respetan los MISMOS PIN que exige el menú clásico (frmInicio), para no crear un bypass.
            Form f = null;
            switch (name)
            {
                case "cierre": f = new frmCierreCaja(); break;
                case "arqueo": if (!funciones.ValidatePIN("Empeño")) return; f = new frmArqueo(); break;
                case "repIngresos": f = new frmReporteIngresos(); break;
                case "repEmpenos": f = new frmReporteEmpeños(); break;
                case "vencidos": if (!funciones.ValidatePIN("Empeño")) return; f = new frmVencidos(); break;
                case "config": if (!funciones.ValidatePIN("Configuración")) return; f = new frmConfiguracionGeneral(); break;
                case "empleados": if (!funciones.ValidatePIN("Empleado")) return; f = new frmEmpleados(); break;
                case "intereses": if (!funciones.ValidatePIN("Configuración")) return; f = new frmIntereses(); break;
            }
            if (f == null) return;
            f.TopLevel = true;
            f.FormBorderStyle = FormBorderStyle.Sizable;
            f.WindowState = FormWindowState.Maximized;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void Logout()
        {
            Program.Usuario = null;
            new frmLogin().Show();
            Close();
        }

        private void AbrirWhatsApp(int empenoId)
        {
            var e = _context.Empenos
                .Where(x => x.EmpenoId == empenoId)
                .Select(x => new { x.Descripcion, x.MontoPendiente, x.FechaVencimiento, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, pct = x.Interes.Porcentaje, bod = x.Interes.Bodegaje })
                .FirstOrDefault();
            if (e == null) return;

            int dias = (e.FechaVencimiento.Date - DateTime.Today).Days;
            string estado = dias >= 0 ? ("vence en " + dias + " día(s)") : ("está vencido hace " + (-dias) + " día(s)");
            // El pago mensual (interés + bodegaje) se cobra sobre el saldo PENDIENTE. El avalúo es cargo único, no recurre.
            double bod = e.bod != null ? Convert.ToDouble(e.bod) : 0;
            double interes = Math.Truncate(e.MontoPendiente * (Convert.ToDouble(e.pct) + bod) / 100.0);
            string tel = new string((e.tel ?? "").Where(char.IsDigit).ToArray());
            if (tel.Length == 8) tel = "506" + tel;

            string mensaje = "Hola " + e.cli + ", su empeño #" + empenoId + " (" + e.Descripcion + ") " + estado
                + ". Puede pagar ₡" + interes.ToString("N0") + " (interés + bodegaje) para conservar su artículo. ¡Gracias!";
            string url = "https://wa.me/" + tel + "?text=" + Uri.EscapeDataString(mensaje);
            try { Process.Start(url); } catch { }
        }
    }
}
