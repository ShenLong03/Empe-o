using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Reports;
using Empeño.WindowsForms.SeedDb;
using Empeño.WindowsForms.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms
{
    static class Program
    {
        public static Form Cargando;
        public static User Usuario;
        public static User ChangeUserPassword;
        public static Cliente Cliente;
        public static string PIN;
        public static bool Acceso;
        public static string Modulo;
        public static int EmpleadoId;
        public static int EmpeñoId;
        public static bool Proroga;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Acceso = false;
            Application.EnableVisualStyles();
            PerfilSeedDb.CheckPerfiles();
            //TODO:Datos de Prueba            
            //ClienteSeedDb.CheckClientes();
            //END TODO
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmCargando(new Size(800,600), new Point(50,50)));
            Application.Run(new frmInicio());
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
