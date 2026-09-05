using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    // VERSIÓN NUEVA del login + splash (WebView2), con el MISMO sistema de diseño del dashboard.
    // No cambia la lógica de autenticación: reusa EXACTAMENTE la consulta del login clásico
    // (Usuario + Password en claro + Activo), su bitácora y ReviewEmpeños() del splash clásico.
    // Si WebView2 no está disponible, cae al login clásico (frmLogin), que siempre funciona.
    public class frmLoginWeb : Form
    {
        private readonly DataContext _context = new DataContext();
        private readonly Funciones.Funciones funciones = new Funciones.Funciones();
        private WebView2 web;

        public frmLoginWeb()
        {
            Text = "Empeños";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(19, 18, 48);   // = --bg del tema
            Size = new Size(920, 600);
            Load += frmLoginWeb_Load;
        }

        // Arrastrar la ventana sin borde desde la zona superior (post "startdrag" desde el HTML).
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private async void frmLoginWeb_Load(object sender, EventArgs e)
        {
            try
            {
                web = new WebView2 { Dock = DockStyle.Fill };
                Controls.Add(web);

                var env = await Program.WebViewEnv();   // entorno WebView2 compartido (evita 0x8007139F al alternar versiones)
                await web.EnsureCoreWebView2Async(env);

                web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                web.CoreWebView2.Settings.IsStatusBarEnabled = false;
                web.CoreWebView2.WebMessageReceived += OnMessage;
                // Aplicar el tema (dark/light) que el usuario dejó guardado en la caché de preferencias.
                web.CoreWebView2.NavigationCompleted += async (s, a) =>
                {
                    if (!a.IsSuccess) return;
                    try
                    {
                        string pf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Empeno", "prefs.txt");
                        if (File.Exists(pf))
                        {
                            var o = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(pf));
                            if ((string)o["theme"] == "light")
                                await web.CoreWebView2.ExecuteScriptAsync("document.documentElement.classList.add('light')");
                        }
                    }
                    catch { }
                };

                string html = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dashboard", "login.html");
                web.CoreWebView2.Navigate(new Uri(html).AbsoluteUri);
            }
            catch (Exception ex)
            {
                // Sin WebView2: no dejar al usuario atrapado, usar el login clásico.
                MessageBox.Show("No se pudo cargar el ingreso nuevo, se abrirá el clásico.\n\n" + ex.Message,
                    "Empeños", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new frmLogin().Show();
                Close();
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
                    case "close": Application.Exit(); break;
                    case "startdrag":
                        ReleaseCapture(); SendMessage(this.Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero);
                        break;
                    case "login":
                        await Autenticar((string)m["user"], (string)m["pass"]);
                        break;
                    case "enter":
                        // Como el clásico (frmBienvenida): la puesta al día de intereses corre AQUÍ y se
                        // ESPERA, detrás del splash que ya existe. A diferencia del clásico, el splash se
                        // cierra cuando el trabajo TERMINA, no cuando se acaba un temporizador.
                        var progreso = new Progress<Funciones.Funciones.ReviewProgreso>(p =>
                        {
                            try
                            {
                                var _p = web.CoreWebView2.ExecuteScriptAsync(
                                    "window.reviewProgress(" + JsonConvert.SerializeObject(p) + ")");
                            }
                            catch { }
                        });
                        var resumen = await funciones.ReviewEmpeños(progreso);
                        if (resumen == null || resumen.Fallo)
                        {
                            string aviso = "No se pudieron actualizar todos los intereses. "
                                         + "Avisá al administrador antes de cobrar: los montos pueden estar incompletos.";
                            await web.CoreWebView2.ExecuteScriptAsync("window.reviewError(" + JsonConvert.SerializeObject(aviso) + ")");
                            // El fallo NO se pasa por alto en silencio: quien entra tiene que enterarse,
                            // porque a partir de acá los intereses que vea pueden estar incompletos.
                            MessageBox.Show(aviso, "Revisión de intereses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            await web.CoreWebView2.ExecuteScriptAsync("window.reviewOk(" + JsonConvert.SerializeObject(
                                "Intereses al día: " + resumen.CuotasCreadas + " cuotas en " + resumen.EmpeñosRevisados + " empeños.") + ")");
                            // Pausa breve para que el resumen del barrido se pueda LEER antes de que el
                            // splash se vaya. Es el único lugar donde estos números quedan a la vista:
                            // la bitácora se escribe pero nadie la lee desde la aplicación.
                            await System.Threading.Tasks.Task.Delay(1800);
                        }
                        var shell = new frmShell();
                        shell.Show();
                        // OJO: esta es la ventana de Application.Run. Si la cerramos, la app entera se
                        // cierra y el dashboard moriría. Se OCULTA (igual que hacía frmLogin clásico).
                        Hide();
                        break;
                }
            }
            catch { }
        }

        // MISMA autenticación que el login clásico (frmLogin.Acceder): usuario + password (en claro) + Activo.
        private async System.Threading.Tasks.Task Autenticar(string user, string pass)
        {
            var usuario = await _context.User.SingleOrDefaultAsync(u => u.Usuario == user && u.Password == pass && u.Activo);

            if (usuario != null)
            {
                Program.Usuario = usuario;
                Program.PerfilId = usuario.PerfilId;

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Login",
                    Accion = "Ingreso",
                    Valor = JsonConvert.SerializeObject(new { Usuario = usuario.Usuario })
                });

                await web.CoreWebView2.ExecuteScriptAsync("window.loginOk(" + JsonConvert.SerializeObject(usuario.Usuario) + ")");
            }
            else
            {
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Login",
                    Accion = "IngresoFallido",
                    Valor = JsonConvert.SerializeObject(new { Usuario = user })
                }, 1, "Ingreso incorrecto");

                await web.CoreWebView2.ExecuteScriptAsync("window.loginFail(" + JsonConvert.SerializeObject("Ingreso incorrecto") + ")");
            }
        }
    }
}
