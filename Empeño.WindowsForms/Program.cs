using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Reports;
using Empeño.WindowsForms.SeedDb;
using Empeño.WindowsForms.Views;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms
{
    static class Program
    {
        // Entorno WebView2 ÚNICO para todo el proceso. Crear varios entornos sobre la misma carpeta
        // de datos (EmpenoWebView2) hace fallar la inicialización (HRESULT 0x8007139F) al alternar
        // entre la versión clásica y la nueva. Se crea una vez y se REUSA en frmLoginWeb y frmShell.
        private static CoreWebView2Environment _webViewEnv;
        public static async Task<CoreWebView2Environment> WebViewEnv()
        {
            if (_webViewEnv == null)
            {
                string udf = Path.Combine(Path.GetTempPath(), "EmpenoWebView2");
                _webViewEnv = await CoreWebView2Environment.CreateAsync(null, udf);
            }
            return _webViewEnv;
        }
        public static Form Cargando;
        public static User Usuario;
        public static User ChangeUserPassword;
        public static Cliente Cliente;
        public static string PIN;
        public static bool Acceso;
        public static string Modulo;
        public static int EmpleadoId;
        public static int PerfilId;
        public static int EmpeñoId;
        public static bool Proroga;
        public static string Meses(int numero_mes)
        {
            string mes = string.Empty;
            switch (numero_mes)
            {
                case 1:
                    mes = "Enero";
                    break;
                case 2:
                    mes = "Febrero";
                    break;
                case 3:
                    mes = "Marzo";
                    break;
                case 4:
                    mes = "Abril";
                    break;
                case 5:
                    mes = "Mayo";
                    break;
                case 6:
                    mes = "Junio";
                    break;
                case 7:
                    mes = "Julio";
                    break;
                case 8:
                    mes = "Agosto";
                    break;
                case 9:
                    mes = "Setiembre";
                    break;
                case 10:
                    mes = "Octubre";
                    break;
                case 11:
                    mes = "Noviembre";
                    break;
                case 12:
                    mes = "Diciembre";
                    break;

            }
            return mes;
        }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Acceso = false;
            Application.EnableVisualStyles();
            PerfilSeedDb.CheckPerfiles();
            SuperUsuarioSeedDb.CheckSuperUsuario();
            //TODO:Datos de Prueba            
            //ClienteSeedDb.CheckClientes();
            //END TODO
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmCargando(new Size(800,600), new Point(50,50)));
            // Ingreso NUEVO (WebView2, mismo diseño del dashboard). El login clásico (frmLogin)
            // queda intacto y se usa solo como respaldo si WebView2 no está disponible.
            Application.Run(new frmLoginWeb());
        }

        public static void GetCargando(Size size, Point location) 
        {
            Cargando = new frmCargando(size, location);
            Cargando.Show();
        }

        public static void CargandoClose()
        {
            Cargando.Close();
            Cargando = null;
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
    }
}
