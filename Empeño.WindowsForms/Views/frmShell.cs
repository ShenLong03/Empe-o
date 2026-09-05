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

        // Autorizaciones de la sesión validadas en C#, no solo en el JS del shell: el PIN se pide al entrar a la
        // sección y acá se recuerda. Los mensajes de carga que devuelven datos sensibles (loadBitacora: pagos y
        // montos serializados; loadConfig: clave SMTP) se rechazan sin PIN válido aunque alguien publique el
        // mensaje a mano (F12 / postMessage) saltándose el menú.
        private bool _bitacoraAutorizada;
        private bool _configAutorizada;

        public frmShell()
        {
            Text = "Empeños";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(19, 18, 48);
            MinimumSize = new Size(1000, 640);
            Size = new Size(1280, 800);                 // tamaño al restaurar (no maximizado)
            Padding = new Padding(6);                   // borde agarrable para redimensionar (WndProc)
            Load += frmShell_Load;
        }

        // Ventana sin borde: permitir REDIMENSIONAR (arrastrando el borde de 6px) cuando NO está maximizada.
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            if (m.Msg == WM_NCHITTEST && WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);
                if ((int)m.Result == 1) // HTCLIENT -> ver si estamos en el borde para devolver un código de resize
                {
                    int lp = m.LParam.ToInt32();
                    var p = PointToClient(new Point((short)(lp & 0xFFFF), (short)((lp >> 16) & 0xFFFF)));
                    int b = 6, w = ClientSize.Width, h = ClientSize.Height;
                    bool l = p.X <= b, r = p.X >= w - b, t = p.Y <= b, bo = p.Y >= h - b;
                    if (t && l) m.Result = (IntPtr)13;        // HTTOPLEFT
                    else if (t && r) m.Result = (IntPtr)14;   // HTTOPRIGHT
                    else if (bo && l) m.Result = (IntPtr)16;  // HTBOTTOMLEFT
                    else if (bo && r) m.Result = (IntPtr)17;  // HTBOTTOMRIGHT
                    else if (l) m.Result = (IntPtr)10;        // HTLEFT
                    else if (r) m.Result = (IntPtr)11;        // HTRIGHT
                    else if (t) m.Result = (IntPtr)12;        // HTTOP
                    else if (bo) m.Result = (IntPtr)15;       // HTBOTTOM
                }
                return;
            }
            base.WndProc(ref m);
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

                var env = await Program.WebViewEnv();   // entorno WebView2 compartido (evita 0x8007139F al alternar versiones)
                await web.EnsureCoreWebView2Async(env);

                web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                web.CoreWebView2.Settings.IsStatusBarEnabled = false;
                // Sin herramientas de desarrollador (F12) en el shell: desde ahí se puede publicar cualquier mensaje al
                // C# (postMessage) saltándose el menú. Nada del sistema depende de ellas; los errores JS ya llegan por
                // el mensaje "jsError" a shell-error-log.txt.
                web.CoreWebView2.Settings.AreDevToolsEnabled = false;
                web.CoreWebView2.WebMessageReceived += OnMessage;
                web.CoreWebView2.NewWindowRequested += (s, a) => { a.Handled = true; try { Process.Start(a.Uri); } catch { } };
                web.CoreWebView2.NavigationCompleted += async (s, a) =>
                {
                    if (!a.IsSuccess) return;
                    // Caché de preferencias (tema, fechas de reportes, etc.) guardada en archivo TXT.
                    await web.CoreWebView2.ExecuteScriptAsync("window.__prefsLoaded(" + JsonConvert.SerializeObject(LeerPrefs()) + ")");
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

        // Caché de preferencias del usuario en un archivo TXT (JSON) bajo %APPDATA%\Empeno\prefs.txt.
        // Guarda cosas como el tema (dark/light) y las fechas de reportes para que persistan entre sesiones.
        private static string PrefsPath()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Empeno");
            return Path.Combine(dir, "prefs.txt");
        }
        private static string LeerPrefs()
        {
            try { var p = PrefsPath(); return File.Exists(p) ? File.ReadAllText(p) : "{}"; }
            catch { return "{}"; }
        }
        private static void GuardarPrefs(string data)
        {
            try
            {
                var p = PrefsPath();
                Directory.CreateDirectory(Path.GetDirectoryName(p));
                File.WriteAllText(p, string.IsNullOrEmpty(data) ? "{}" : data);
            }
            catch { }
        }

        // Parsea la "fecha de corte" opcional (dd/MM/yyyy) que manda el front; null si no vino o es inválida.
        private static DateTime? ParseFecha(Newtonsoft.Json.Linq.JObject m)
        {
            DateTime d;
            if (m["fecha"] != null && DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
                return d;
            return null;
        }

        // Parsea una fecha dd/MM/yyyy de una clave arbitraria del mensaje (ej. "desde"/"hasta"); null si falta o es inválida.
        private static DateTime? ParseFecha(Newtonsoft.Json.Linq.JObject m, string clave)
        {
            DateTime d;
            if (m[clave] != null && DateTime.TryParseExact((string)m[clave], "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
                return d;
            return null;
        }

        // ¿El front marcó "Mostrar todos"? (ignora el rango de fechas y muestra toda la cartera del reporte).
        private static bool Todos(Newtonsoft.Json.Linq.JObject m)
        {
            return m["todos"] != null && (bool)m["todos"];
        }

        private async void OnMessage(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string type = null;
            try
            {
                var m = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(e.TryGetWebMessageAsString());
                type = (string)m["type"];

                // BLINDAJE anti-staleness (EF6): el _context del dashboard vive TODA la sesión y cachea en su
                // identity-map las entidades que lee. Los forms clásicos (frmPagar, frmEmpeno, frmVencidos...) ESCRIBEN
                // con su PROPIO contexto; sin esto el dashboard seguiría mostrando valores viejos —p.ej. un interés YA
                // pagado que aparece "pendiente"—. Antes de procesar CADA mensaje soltamos lo cacheado (solo lo
                // Unchanged, sin tocar cambios pendientes) para que cada lectura vuelva a leer FRESCO desde la base.
                try
                {
                    foreach (var _e in _context.ChangeTracker.Entries()
                                 .Where(x => x.State == System.Data.Entity.EntityState.Unchanged).ToList())
                        _e.State = System.Data.Entity.EntityState.Detached;
                }
                catch { }

                switch (type)
                {
                    case "min": WindowState = FormWindowState.Minimized; break;
                    case "max": WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized; break;
                    case "startdrag":
                        // Mover la ventana arrastrando la barra superior (solo si no está maximizada).
                        if (WindowState != FormWindowState.Maximized) { ReleaseCapture(); SendMessage(this.Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero); }
                        break;
                    case "close": Application.Exit(); break;
                    case "prefsSave": GuardarPrefs((string)m["data"]); break;
                    case "jsError":
                        // Error JS del WebView reportado por el front: se deja en shell-error-log.txt para diagnosticar
                        // en la máquina del local (WebView2 viejo, etc.) sin tener que adivinar por qué falla en prod.
                        try
                        {
                            var lpJs = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "shell-error-log.txt");
                            System.IO.File.AppendAllText(lpJs, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  [JS] " + (m["msg"] != null ? m["msg"].ToString() : "") + Environment.NewLine);
                        }
                        catch { }
                        break;
                    case "configPin":
                        {
                            // Entrar a Configuración: solo Administrador. La vista no se muestra si no pasa.
                            bool okCfg = funciones.ValidatePIN("Configuración Admin");
                            _configAutorizada = okCfg;
                            await web.CoreWebView2.ExecuteScriptAsync("window.__configPinResuelto(" + (okCfg ? "true" : "false") + ")");
                            break;
                        }
                    case "bitacoraPin":
                        {
                            // Entrar a Bitácora: mismo gate que Configuración (SOLO Administrador). Valor puede
                            // traer datos de negocio serializados (pagos, montos), por eso el mismo nivel de acceso.
                            bool okBit = funciones.ValidatePIN("Configuración Admin");
                            _bitacoraAutorizada = okBit;
                            await web.CoreWebView2.ExecuteScriptAsync("window.__bitacoraPinResuelto(" + (okBit ? "true" : "false") + ")");
                            break;
                        }
                    case "loadBitacora":
                        {
                            // SOLO LECTURA: BitacoraData nunca escribe. El PIN ya se validó al entrar a la sección
                            // (case "bitacoraPin"); acá no se repite para no pedirlo en cada filtro/búsqueda, pero SÍ se
                            // exige que esa validación haya ocurrido en esta sesión: sin ella no se devuelve nada.
                            if (!_bitacoraAutorizada)
                            {
                                await funciones.SaveBitacora(new Empeño.CommonEF.Models.ValorBitacora { Modulo = "Bitácora", Accion = "AccesoDenegado", Valor = "loadBitacora sin PIN validado" }, 1, "Acceso a Bitácora sin PIN de administrador");
                                break;
                            }
                            bool bitSoloErrores = m["soloErrores"] != null && (bool)m["soloErrores"];
                            string bitTexto = m["texto"] != null ? (string)m["texto"] : null;
                            string bj = JsonConvert.SerializeObject(BitacoraData.Lista(_context, ParseFecha(m, "desde"), ParseFecha(m, "hasta"), bitSoloErrores, bitTexto));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderBitacora(" + bj + ")");
                            break;
                        }
                    case "clasico": AbrirClasico(); break;
                    case "logout": Logout(); break;
                    case "backup":
                        {
                            // Respaldo de la BD: MISMA lógica del clásico (frmInicio.Create) → BACKUP DATABASE a C:\Backup\empeno.bak.
                            // Servidor/BD tomados de la conexión real de la app (anda en el local con "." y en dev con LocalDB).
                            string bpath = "C:\\Backup\\empeno.bak";
                            try { System.IO.Directory.CreateDirectory("C:\\Backup"); } catch { }
                            string srv = _context.Database.Connection.DataSource;
                            string dbn = _context.Database.Connection.Database;
                            bool okBk = await System.Threading.Tasks.Task.Run(() => frmInicio.Create(srv, dbn, bpath));
                            await web.CoreWebView2.ExecuteScriptAsync("window.backupResuelto(" + JsonConvert.SerializeObject(new { ok = okBk, path = bpath }) + ")");
                            break;
                        }
                    case "nav": Navegar((string)m["module"]); break;
                    case "range":
                        string serie = JsonConvert.SerializeObject(TableroData.Serie(_context, (int)m["index"]));
                        await web.CoreWebView2.ExecuteScriptAsync("window.updateSerie(" + serie + ")");
                        break;
                    case "loadEmpenos":
                        {
                            string filtro = m["filtro"] != null ? (string)m["filtro"] : "activos";
                            int skip = m["skip"] != null ? (int)m["skip"] : 0;
                            string ej = JsonConvert.SerializeObject(EmpenosData.Lista(_context, filtro, skip));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderEmpenos(" + ej + ")");
                            break;
                        }
                    case "searchEmpenos":
                        {
                            int skip = m["skip"] != null ? (int)m["skip"] : 0;
                            string filtro = m["filtro"] != null ? (string)m["filtro"] : "activos";
                            string ej = JsonConvert.SerializeObject(EmpenosData.Buscar(_context, (string)m["q"], filtro, skip));
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
                    case "reprintEmpeno":
                        {
                            // Reimprime el comprobante del empeño (o retiro si está cancelado) SIN abrir el form clásico.
                            var f = new frmEmpeno();
                            await f.ReimprimirEmpenoPorId((int)m["id"]);
                            f.Dispose();
                            break;
                        }
                    case "reprintContrato":
                        {
                            // Reimprime el CONTRATO del empeño seleccionado (chooser "Reimprimir" del dashboard).
                            var f = new frmEmpeno();
                            await f.ReimprimirContratoPorId((int)m["id"]);
                            f.Dispose();
                            break;
                        }
                    case "cambiarEstado":
                        {
                            // Cambiar estado del empeño: SOLO Administrador/Supervisor (mismo PIN que editar).
                            if (!funciones.ValidatePIN("Editar Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.estadoCambiado({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fce = new frmEmpeno();
                            var rce = await fce.CambiarEstadoHeadless((int)m["id"], (int)m["estado"]);
                            fce.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.estadoCambiado(" + JsonConvert.SerializeObject(rce) + ")");
                            break;
                        }
                    case "cobrarInfo":
                        {
                            // Interés al día ANTES de mostrar montos: PagosData.CobrarInfo es SOLO LECTURA,
                            // así que la puesta al día se hace acá, antes de la consulta.
                            await funciones.ReviewEmpeño((int)m["id"]);
                            string cj = JsonConvert.SerializeObject(PagosData.CobrarInfo(_context, (int)m["id"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCobrar(" + cj + ")");
                            break;
                        }
                    case "cobrar":
                        {
                            // Reusa la lógica EXACTA del clásico (frmPagar.CobrarHeadless): PIN, reparto, cancelación, impresión.
                            double pi = m["pagoIntereses"] != null ? (double)m["pagoIntereses"] : 0;
                            double pm = m["pagoMonto"] != null ? (double)m["pagoMonto"] : 0;
                            var fpg = new frmPagar((int)m["id"], pi);   // valorInteres = pi (tope del interés a pagar)
                            var rc = await fpg.CobrarHeadless(pi, pm, (string)m["comentario"]);
                            fpg.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.cobrarResuelto(" + JsonConvert.SerializeObject(rc) + ")");
                            break;
                        }
                    case "anularPago":
                        {
                            // Anular un pago: SOLO Administrador. Reusa el reverso EXACTO del clásico (frmEmpeno.AnularPagoHeadless).
                            if (!funciones.ValidatePIN("Borrar Pago Admin"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.pagoAnulado({ok:false,error:'Necesita PIN de Administrador para anular un pago.'})");
                                break;
                            }
                            var fap = new frmEmpeno();
                            var rap = await fap.AnularPagoHeadless((int)m["id"]);
                            fap.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.pagoAnulado(" + JsonConvert.SerializeObject(rap) + ")");
                            break;
                        }
                    case "empenoDet":
                        {
                            // Igual que el clásico (frmEmpeno.cs:991-996): antes de MOSTRAR un empeño se revisa
                            // (genera lo que falte) y se depuran duplicados. Ambas son idempotentes.
                            int idDet = (int)m["id"];
                            if (idDet > 0)
                            {
                                await funciones.ReviewEmpeño(idDet);
                                await funciones.ReviewDuplicateEmpeños(idDet);
                            }
                            string dj = JsonConvert.SerializeObject(EmpenosData.Detalle(_context, idDet));
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
                    case "searchClientes":
                        {
                            // Búsqueda server-side (gemelo de searchEmpenos): encuentra clientes fuera del precargado.
                            string cj = JsonConvert.SerializeObject(ClientesData.Buscar(_context, (string)m["q"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderClientesBuscar(" + cj + ")");
                            break;
                        }
                    case "clienteDet":
                        {
                            string cdj = JsonConvert.SerializeObject(ClientesData.Detalle(_context, (int)m["id"]));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderClienteDetalle(" + cdj + ")");
                            break;
                        }
                    case "openClientes": AbrirClientesClasico(); break;
                    case "crearCliente":
                        {
                            DateTime fcli;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fcli))
                                fcli = DateTime.Today;
                            string rj = JsonConvert.SerializeObject(ClientesData.Crear(_context,
                                (string)m["ced"], (string)m["nom"], (string)m["tel"], (string)m["cor"], (string)m["dir"], (string)m["com"], (bool)m["activo"], fcli));
                            if ((bool)Newtonsoft.Json.Linq.JObject.Parse(rj)["ok"])
                                await funciones.SaveBitacora(new Empeño.CommonEF.Models.ValorBitacora
                                {
                                    Modulo = "Cliente",
                                    Accion = "Crear",
                                    Valor = JsonConvert.SerializeObject(new { Identificacion = (string)m["ced"], Nombre = (string)m["nom"], Telefono = (string)m["tel"], Correo = (string)m["cor"], Activo = (bool)m["activo"] })
                                });
                            await web.CoreWebView2.ExecuteScriptAsync("window.clienteCreado(" + rj + ")");
                            break;
                        }
                    case "editarCliente":
                        {
                            DateTime fcli;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fcli))
                                fcli = DateTime.Today;
                            string rj = JsonConvert.SerializeObject(ClientesData.Editar(_context,
                                (int)m["id"], (string)m["ced"], (string)m["nom"], (string)m["tel"], (string)m["cor"], (string)m["dir"], (string)m["com"], (bool)m["activo"], fcli));
                            if ((bool)Newtonsoft.Json.Linq.JObject.Parse(rj)["ok"])
                                await funciones.SaveBitacora(new Empeño.CommonEF.Models.ValorBitacora
                                {
                                    Modulo = "Cliente",
                                    Accion = "Editar",
                                    Valor = JsonConvert.SerializeObject(new { Identificacion = (string)m["ced"], Nombre = (string)m["nom"], Telefono = (string)m["tel"], Correo = (string)m["cor"], Activo = (bool)m["activo"] })
                                });
                            await web.CoreWebView2.ExecuteScriptAsync("window.clienteEditado(" + rj + ")");
                            break;
                        }
                    case "loadPlanes":
                        {
                            string pj = JsonConvert.SerializeObject(EmpenosData.Planes(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderPlanes(" + pj + ")");
                            break;
                        }
                    case "editarPin":
                        {
                            // PIN al tocar "Editar" (antes de abrir el editor). SOLO Administrador.
                            bool okPin = funciones.ValidatePIN("Editar Empeño Admin");
                            await web.CoreWebView2.ExecuteScriptAsync("window.__editarPinResuelto(" + (okPin ? "true" : "false") + ")");
                            break;
                        }
                    case "clientePin":
                        {
                            // PIN al tocar "Editar" en un cliente (antes de abrir el editor). SOLO Administrador.
                            bool okCli = funciones.ValidatePIN("Cliente Admin");
                            await web.CoreWebView2.ExecuteScriptAsync("window.__clientePinResuelto(" + (okCli ? "true" : "false") + ")");
                            break;
                        }
                    case "editarEmpeno":
                        {
                            // El PIN (solo Administrador) YA se validó al tocar "Editar" (caso editarPin), antes de abrir
                            // el editor; el editor no se abre sin ese PIN, así que acá no se vuelve a pedir.
                            DateTime fe, fv;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fe)
                                || !DateTime.TryParseExact((string)m["vence"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fv))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.empenoEditado({ok:false,error:'Las fechas no son válidas (dd/MM/yyyy).'})");
                                break;
                            }
                            int meses = _context.Configuraciones.Select(c => c.Meses).FirstOrDefault();
                            var fe2 = new frmEmpeno();
                            string err = await fe2.EditarEmpeno((int)m["id"], (string)m["desc"], (bool)m["oro"], (string)m["com"], fe,
                                (string)m["plan"], (double)m["monto"], fv, (double)m["avaluo"], Program.EmpleadoId, Program.PerfilId, meses > 0 ? meses : 3);
                            fe2.Dispose();
                            string rj = JsonConvert.SerializeObject(new { ok = err == null, error = err });
                            await web.CoreWebView2.ExecuteScriptAsync("window.empenoEditado(" + rj + ")");
                            break;
                        }
                    case "loadPlanesFull":
                        {
                            string pj = JsonConvert.SerializeObject(EmpenosData.PlanesDetalle(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderPlanesFull(" + pj + ")");
                            break;
                        }
                    case "crearEmpeno":
                        {
                            // Reusa la lógica de alta del clásico (CrearEmpeno) + PIN "Empeño".
                            if (!funciones.ValidatePIN("Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.empenoCreado({ok:false,error:'Necesita PIN para crear empeños.'})");
                                break;
                            }
                            DateTime fc, fvc;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fc)
                                || !DateTime.TryParseExact((string)m["vence"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fvc))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.empenoCreado({ok:false,error:'Las fechas no son válidas (dd/MM/yyyy).'})");
                                break;
                            }
                            // igual al clásico: la fecha del empeño = día elegido + hora actual (HH:mm).
                            var ahora = DateTime.Now;
                            fc = fc.Date.Add(new TimeSpan(ahora.Hour, ahora.Minute, 0));
                            int mesesCfg = _context.Configuraciones.Select(c => c.Meses).FirstOrDefault();
                            bool impContrato = m["imprimirContrato"] != null && (bool)m["imprimirContrato"];
                            var fc2 = new frmEmpeno();
                            var resCrear = await fc2.CrearEmpeno((int)m["clienteId"], (string)m["desc"], (string)m["com"], (double)m["monto"], (double)m["avaluo"], (double)m["bodegaje"], (bool)m["oro"], (int)m["planId"], fc, fvc, Program.EmpleadoId, Program.PerfilId, mesesCfg > 0 ? mesesCfg : 3, impContrato);
                            fc2.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.empenoCreado(" + JsonConvert.SerializeObject(resCrear) + ")");
                            break;
                        }
                    case "loadCaja":
                        {
                            string kj = JsonConvert.SerializeObject(CajaData.ResumenHoy(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCaja(" + kj + ")");
                            string mj = JsonConvert.SerializeObject(CajaData.MovimientosHoy(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCajaMov(" + mj + ")");
                            break;
                        }
                    case "diaResumen":
                        {
                            // Contadores del día para la vista Empeños. Reusa el MISMO resumen de caja.
                            string rj = JsonConvert.SerializeObject(CajaData.ResumenHoy(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderDiaResumen(" + rj + ")");
                            break;
                        }
                    case "cierrePreview":
                        {
                            DateTime fp;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fp))
                                fp = DateTime.Today;
                            string pj = JsonConvert.SerializeObject(CajaData.CierrePreview(_context, fp));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCierrePreview(" + pj + ")");
                            break;
                        }
                    case "guardarCierre":
                        {
                            if (!funciones.ValidatePIN("Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.cierreGuardado({ok:false,error:'Necesita PIN para cerrar caja.'})");
                                break;
                            }
                            DateTime fcierre;
                            if (!DateTime.TryParseExact((string)m["fecha"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fcierre))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.cierreGuardado({ok:false,error:'Fecha inválida (dd/MM/yyyy).'})");
                                break;
                            }
                            var ahoraC = DateTime.Now;
                            fcierre = fcierre.Date.Add(new TimeSpan(ahoraC.Hour, ahoraC.Minute, 0));
                            double saldoIni = m["saldoInicial"] != null ? (double)m["saldoInicial"] : 0;
                            var manuales = new System.Collections.Generic.List<Empeño.CommonEF.Entities.DetalleCierreCaja>();
                            var jarr = m["manuales"] as Newtonsoft.Json.Linq.JArray;
                            if (jarr != null)
                                foreach (var it in jarr)
                                    manuales.Add(new Empeño.CommonEF.Entities.DetalleCierreCaja { Concepto = (string)it["concepto"], Valor = (double)it["valor"] });
                            var fcc = new frmCierreCaja();
                            var resC = await fcc.GuardarCierreHeadless(fcierre, saldoIni, manuales, Program.EmpleadoId);
                            fcc.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.cierreGuardado(" + JsonConvert.SerializeObject(resC) + ")");
                            break;
                        }
                    case "cajaHistorial":
                        {
                            string hj = JsonConvert.SerializeObject(CajaData.Historial(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderCierreHistorial(" + hj + ")");
                            break;
                        }
                    case "reprintCierre":
                        {
                            var fcc = new frmCierreCaja();
                            await fcc.ReimprimirCierrePorId((int)m["id"]);
                            fcc.Dispose();
                            break;
                        }
                    case "loadArqueo":
                        {
                            string aj = JsonConvert.SerializeObject(ArqueoData.Resumen(_context, ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m)));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderArqueo(" + aj + ")");
                            break;
                        }
                    case "imprimirArqueo":
                        {
                            if (!funciones.ValidatePIN("Editar Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.arqueoResult({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fa = new frmArqueo();
                            await fa.ImprimirArqueoHeadless((string)m["obs"], ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m));
                            fa.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.arqueoResult({ok:true})");
                            break;
                        }
                    case "enviarArqueo":
                        {
                            if (!funciones.ValidatePIN("Editar Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.arqueoResult({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fa = new frmArqueo();
                            var rEnv = await fa.EnviarArqueoHeadless((string)m["obs"], ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m));
                            fa.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.arqueoResult(" + JsonConvert.SerializeObject(rEnv) + ")");
                            break;
                        }
                    case "retirarAdmin":
                        {
                            var fa = new frmArqueo();
                            var rRet = await fa.RetirarAdminHeadless((int)m["id"]);
                            fa.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.arqueoAccion(" + JsonConvert.SerializeObject(rRet) + ")");
                            break;
                        }
                    case "aplicarProroga":
                        {
                            var fp = new frmProroga((int)m["id"]);
                            var rPro = await fp.AplicarProrogaHeadless((int)m["dias"], (string)m["comentario"]);
                            fp.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.prorogaResuelta(" + JsonConvert.SerializeObject(rPro) + ")");
                            break;
                        }
                    case "loadRepIngresos":
                        {
                            DateTime rid, rih;
                            if (!DateTime.TryParseExact((string)m["desde"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out rid)) rid = DateTime.Today.AddMonths(-1);
                            if (!DateTime.TryParseExact((string)m["hasta"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out rih)) rih = DateTime.Today;
                            string j = JsonConvert.SerializeObject(ReportesData.IngresosEgresos(_context, rid, rih));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderRepIngresos(" + j + ")");
                            break;
                        }
                    case "loadRepEmpenos":
                        {
                            DateTime red, reh;
                            if (!DateTime.TryParseExact((string)m["desde"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out red)) red = DateTime.Today.AddMonths(-1);
                            if (!DateTime.TryParseExact((string)m["hasta"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out reh)) reh = DateTime.Today;
                            bool borr = m["borrados"] != null && (bool)m["borrados"];
                            string j = JsonConvert.SerializeObject(ReportesData.Empenos(_context, red, reh, borr));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderRepEmpenos(" + j + ")");
                            break;
                        }
                    case "openVencidos":
                        {
                            // Mismo PIN que exigía el menú clásico para ABRIR Cartera vencida (solo al abrir).
                            if (!funciones.ValidatePIN("Empeño"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.renderVencidos({error:'Necesita PIN para ver la cartera vencida.'})");
                                break;
                            }
                            string vj0 = JsonConvert.SerializeObject(ReportesData.Vencidos(_context, ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m)));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderVencidos(" + vj0 + ")");
                            break;
                        }
                    case "loadVencidos":
                        {
                            // Recarga tras una acción; no re-pide PIN (ya se validó al abrir).
                            string vj = JsonConvert.SerializeObject(ReportesData.Vencidos(_context, ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m)));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderVencidos(" + vj + ")");
                            break;
                        }
                    case "imprimirVencidos":
                        {
                            var fv = new frmVencidos();
                            await fv.ImprimirVencidosHeadless(ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m));
                            fv.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.vencidosResult({ok:true})");
                            break;
                        }
                    case "enviarVencidos":
                        {
                            var fv = new frmVencidos();
                            var rv = await fv.EnviarVencidosHeadless(ParseFecha(m, "desde"), ParseFecha(m, "hasta"), Todos(m));
                            fv.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.vencidosResult(" + JsonConvert.SerializeObject(rv) + ")");
                            break;
                        }
                    case "sacarVencido":
                        {
                            var fv = new frmVencidos();
                            var rs = await fv.SacarVencidoHeadless((int)m["id"]);
                            fv.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.vencidosAccion(" + JsonConvert.SerializeObject(rs) + ")");
                            break;
                        }
                    case "loadConfig":
                        {
                            // Devuelve la clave SMTP: mismo gate en C# que Bitácora (el PIN se validó en "configPin").
                            if (!_configAutorizada)
                            {
                                await funciones.SaveBitacora(new Empeño.CommonEF.Models.ValorBitacora { Modulo = "Configuración", Accion = "AccesoDenegado", Valor = "loadConfig sin PIN validado" }, 1, "Acceso a Configuración sin PIN de administrador");
                                break;
                            }
                            string gj = JsonConvert.SerializeObject(ConfigData.Get(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderConfig(" + gj + ")");
                            break;
                        }
                    case "guardarConfig":
                        {
                            if (!funciones.ValidatePIN("Configuración"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.configGuardada({ok:false,error:'Necesita PIN para guardar la configuración.'})");
                                break;
                            }
                            var fcfg = new frmConfiguracionGeneral();
                            var rcfg = await fcfg.GuardarHeadless(m);
                            fcfg.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.configGuardada(" + JsonConvert.SerializeObject(rcfg) + ")");
                            break;
                        }
                    case "loadEmpleados":
                        {
                            string ej = JsonConvert.SerializeObject(EmpleadosData.Lista(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderEmpleados(" + ej + ")");
                            break;
                        }
                    case "guardarEmpleado":
                        {
                            // Mismo PIN que exigía el clásico para gestionar empleados/accesos.
                            if (!funciones.ValidatePIN("Empleado"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.empleadoGuardado({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fe = new frmEmpleados();
                            var re = await fe.GuardarHeadless(m);
                            fe.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.empleadoGuardado(" + JsonConvert.SerializeObject(re) + ")");
                            break;
                        }
                    case "eliminarEmpleado":
                        {
                            if (!funciones.ValidatePIN("Empleado"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.empleadoEliminado({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fe = new frmEmpleados();
                            var rd = await fe.EliminarHeadless((int)m["id"]);
                            fe.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.empleadoEliminado(" + JsonConvert.SerializeObject(rd) + ")");
                            break;
                        }
                    case "loadIntereses":
                        {
                            string ij = JsonConvert.SerializeObject(InteresesData.Lista(_context));
                            await web.CoreWebView2.ExecuteScriptAsync("window.renderIntereses(" + ij + ")");
                            break;
                        }
                    case "guardarInteres":
                        {
                            // Mismo PIN que el clásico exigía para editar/guardar planes de tasa.
                            if (!funciones.ValidatePIN("Configuración"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.interesGuardado({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fi = new frmIntereses();
                            var ri = await fi.GuardarHeadless(m);
                            fi.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.interesGuardado(" + JsonConvert.SerializeObject(ri) + ")");
                            break;
                        }
                    case "eliminarInteres":
                        {
                            if (!funciones.ValidatePIN("Configuración"))
                            {
                                await web.CoreWebView2.ExecuteScriptAsync("window.interesEliminado({ok:false,error:'Necesita PIN de Administrador o Supervisor.'})");
                                break;
                            }
                            var fi = new frmIntereses();
                            var re = await fi.EliminarHeadless((int)m["id"]);
                            fi.Dispose();
                            await web.CoreWebView2.ExecuteScriptAsync("window.interesEliminado(" + JsonConvert.SerializeObject(re) + ")");
                            break;
                        }
                    case "openForm": AbrirFormClasico((string)m["form"]); break;
                    case "whatsapp": AbrirWhatsApp((int)m["id"]); break;
                }
            }
            catch (Exception ex)
            {
                // NUNCA tragarse un error en silencio. Si una acción (crear empeño, cobrar, cierre…) falla, hay que
                // (1) dejar traza en disco para diagnosticar en la máquina del usuario y (2) AVISARLE al front, para
                // que el usuario vea un error claro en vez de creer que "se hizo". Antes este catch estaba VACÍO: una
                // falla de guardado/impresión/conexión desaparecía sin rastro ni mensaje → "empeño fantasma".
                try
                {
                    var lp = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "shell-error-log.txt");
                    System.IO.File.AppendAllText(lp, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  [" + (type ?? "?") + "]  " + ex.ToString() + Environment.NewLine + Environment.NewLine);
                }
                catch { }
                try
                {
                    string emsg = JsonConvert.SerializeObject("La acción '" + (type ?? "") + "' no se completó: " + ex.Message);
                    if (web != null && web.CoreWebView2 != null)
                        await web.CoreWebView2.ExecuteScriptAsync("window.__accionError && window.__accionError(" + emsg + ")");
                }
                catch { }
            }
            finally
            {
                // Apaga el loader global de la vista nueva al terminar CUALQUIER operación (éxito o error).
                try { if (web != null && web.CoreWebView2 != null) await web.CoreWebView2.ExecuteScriptAsync("window.__busyOff&&window.__busyOff()"); }
                catch { }
            }
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
            new frmLoginWeb().Show();
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
