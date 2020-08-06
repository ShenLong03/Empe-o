using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmPIN : Form
    {
        string Modulo;
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmPIN(string modulo)
        {
            InitializeComponent();
            Modulo = modulo;
            Program.Modulo = Modulo;
            Program.Acceso = false;
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            await Aceptar();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void txtPIN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Aceptar();
                this.Close();
            }
        }

        public async Task Aceptar() 
        {
            var perfil = await _context.User.Where(u => u.Codigo == txtPIN.Text).FirstOrDefaultAsync();
            if (perfil == null)
            {
                MessageBox.Show("No tiene acceso al módulo " + Modulo, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.Modulo = Modulo;
                Program.Acceso = false;
                return;
            }

            Program.PIN = txtPIN.Text;
            Program.Modulo = Modulo;
            Program.Acceso = false;
            Program.EmpleadoId = await funciones.GetEmpleadoIdByUser(perfil.Usuario);
            Program.PerfilId = perfil.PerfilId;

            if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "SuperUsuario")
                Program.Acceso = true;

            switch (Modulo)
            {
                case "Configuración":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Cierre Caja":
                    if (perfil.Perfil.Nombre == "Empleado" || perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Arqueo":
                    if (perfil.Perfil.Nombre == "Empleado" || perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Empeño":
                    if (perfil.Perfil.Nombre == "Empleado" || perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Editar Empeño":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Borrar Empeño":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Borrar Pago":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Editar Pago":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Pago":
                    if (perfil.Perfil.Nombre == "Empleado" || perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                case "Empleado":
                    if (perfil.Perfil.Nombre == "Administrador" || perfil.Perfil.Nombre == "Supervisor")
                        Program.Acceso = true;
                    break;
                default:
                    Program.Acceso = false;
                    break;
            }
            if (!Program.Acceso)
            {                
                MessageBox.Show("No tiene acceso al módulo " + Modulo, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.Modulo = Modulo;
                Program.Acceso = false;            
            }
            this.Close();
        }
    }
}
