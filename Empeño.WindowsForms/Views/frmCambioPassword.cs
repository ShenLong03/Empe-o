using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmCambioPassword : Form
    {
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmCambioPassword()
        {
            InitializeComponent();
        }

        private void txtViejo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtViejo, lblViejo, PlaceHolderType.Enter, "Viejo Password",true);
        }

        private void txtViejo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtViejo, lblViejo, PlaceHolderType.Leave, "Viejo Password", true);
        }

        private void txtNuevo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNuevo, lblNuevo, PlaceHolderType.Leave, "Nuevo Password", true);
        }

        private void txtNuevo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNuevo, lblNuevo, PlaceHolderType.Enter, "Nuevo Password", true);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNuevo.Text))
            {
                var usuario = await _context.User.SingleOrDefaultAsync(u => u.Usuario == Program.ChangeUserPassword.Usuario);
                if (usuario!=null)
                {
                    if (usuario.Password==txtViejo.Text)
                    {
                        usuario.Password = txtNuevo.Text;
                        Program.ChangeUserPassword.Password = usuario.Password;
                        _context.Entry(usuario).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        MessageBox.Show("Dato guardado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña anterior no es correcta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCambioPassword_Load(object sender, EventArgs e)
        {
            lblNuevo.Visible = false;
            lblViejo.Visible = false;
        }
    }
}
