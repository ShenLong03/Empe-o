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
    public partial class frmLogin : Form
    {
        DataContext _context = new DataContext();
        public int xClick = 0, yClick = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text== "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.White;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnAcceder.Focus();
            lblError.Visible = false;
            pictureBox1.Visible = false;
        }

        private async void btnAcceder_Click(object sender, EventArgs e)
        {

            Acceder();
        }

        private async void Acceder() 
        {
            var usuario = await _context.User.SingleOrDefaultAsync(u => u.Usuario == txtUsuario.Text && u.Password == txtContrasena.Text);

            if (usuario != null)
            {
                this.Hide();
                Program.Usuario = usuario;
                //frmBienvenida bienvenida = new frmBienvenida();
                //bienvenida.ShowDialog();
                frmInicio inicio = new frmInicio();
                inicio.Show();
            }
            else
            {
                label1.Visible = true;
                lblError.Text = " Ingreso Incorrecto";
                lblError.ForeColor = Color.White;
                lblError.Visible = true;
                pictureBox1.Visible = true;
            }
        }

        private void txtContrasena_Enter(object sender, EventArgs e)
        {
            if (txtContrasena.Text == "CONTRASEÑA")
            {
                txtContrasena.Text = "";
                txtContrasena.ForeColor = Color.White;
                txtContrasena.UseSystemPasswordChar = true;
            }
        }

        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                Acceder();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtContrasena_Leave(object sender, EventArgs e)
        {
            if (txtContrasena.Text == "")
            {
                txtContrasena.Text = "CONTRASEÑA";
                txtContrasena.ForeColor = Color.DimGray;
                txtContrasena.UseSystemPasswordChar = false;
            }
        }
    }
}
