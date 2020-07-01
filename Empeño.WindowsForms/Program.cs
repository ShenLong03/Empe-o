using Empeño.WindowsForms.SeedDb;
using Empeño.WindowsForms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            PerfilSeedDb.CheckPerfiles();
            //TODO:Datos de Prueba            
            ClienteSeedDb.CheckClientes();
            //END TODO
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmInicio());
        }
    }
}
