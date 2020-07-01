using Empeño.Forms.Data;
using Empeño.Forms.SeedDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PerfilSeedDb.CheckPerfiles();
            //TODO:Datos de Prueba            
            ClienteSeedDb.CheckClientes();
            //END TODO
            Application.Run(new frmPrincipal());
        }
    }
}
