using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmEmpleados : Form, IDisposable
    {
        DataContext _context = new DataContext();
        int empleadoId = 0;
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmEmpleados()
        {
            InitializeComponent();
        }

        private async void frmEmpleados_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        #region Funciones
        public async Task LoadData() 
        {
            dgvEmpleados.DataSource = await _context.Empleados.Select(x => new
            {
                Id = x.EmpleadoId,
                x.Nombre,
                x.Correo,
                x.Telefono
            }).ToListAsync();

            DataGridViewColumn column = dgvEmpleados.Columns[0];
            column.Width = 40;
        }

        private void Clear()
        {
            txtNombre.Text = string.Empty;
            funciones.PlaceHolder(txtNombre, PlaceHolderType.Leave, "Nombre");

            empleadoId = 0;
            txtPassword.Text = string.Empty;
            funciones.PlaceHolder(txtPassword, PlaceHolderType.Leave, "Password");
            txtCorreo.Text = string.Empty;
            funciones.PlaceHolder(txtCorreo, PlaceHolderType.Leave, "Correo");
            cbPerfil.Text = string.Empty;
            txtPIN.Text = string.Empty;
            funciones.PlaceHolder(txtNombre, PlaceHolderType.Leave, "PIN");
            txtTelefono.Text = string.Empty;
            funciones.PlaceHolder(txtTelefono, PlaceHolderType.Leave, "Teléfono");
            txtUsuario.Text = string.Empty;
            funciones.PlaceHolder(txtUsuario, PlaceHolderType.Leave, "Usuario");
            txtNombre.Focus();
            chbActivo.Checked = true;
            cbPerfil.Text = "Empleado";
        }
        private int GetPerfilId(string text)
        {
            return _context.Perfil.Single(p => p.Nombre == text).PerfilId;
        }
        #endregion

        private async void btnGuardar_Click(object sender, EventArgs e)
        {         
            try
            {
                if (empleadoId == 0)
                {
                    var empleado = new Empleado
                    {
                        EmpleadoId = empleadoId,
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Correo = txtCorreo.Text,
                        Activo = chbActivo.Checked,
                        Usuario = txtUsuario.Text
                    };

                    var user = new User
                    {
                        Activo = chbActivo.Checked,
                        Usuario = txtUsuario.Text,
                        Codigo = txtPIN.Text,
                        Password = txtPassword.Text,
                        PerfilId = GetPerfilId(cbPerfil.Text)
                    };

                    _context.Empleados.Add(empleado);
                    _context.User.Add(user);

                }
                else
                {
                    var empleado = _context.Empleados.Find(empleadoId);
                    var user = _context.User.Single(u => u.Usuario == empleado.Usuario);

                    empleado.Correo = txtCorreo.Text;
                    empleado.Nombre = txtNombre.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Activo = chbActivo.Checked;
                    empleado.Usuario = txtUsuario.Text;

                    user.Activo = chbActivo.Checked;
                    user.Usuario = txtUsuario.Text;
                    user.Codigo = txtPIN.Text;
                    user.Password = txtPassword.Text;

                    _context.Entry(empleado).State = EntityState.Modified;
                    _context.Entry(user).State = EntityState.Modified;
                }
                _context.SaveChanges();
                await LoadData();
                Clear();
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Enter, "Nombre");
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Leave, "Correo");
        }

        private void txtCorreo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Enter, "Correo");
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Leave, "Telefono");
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Enter, "Telefono");
        }

        private void cbPerfil_Leave(object sender, EventArgs e)
        {
            if (cbPerfil.Text == "")
            {
                cbPerfil.Text = "Perfil";
                cbPerfil.ForeColor = Color.DimGray;
            }
        }

        private void cbPerfil_Enter(object sender, EventArgs e)
        {
            if (cbPerfil.Text == "Perfil")
            {
                cbPerfil.Text = "";
                cbPerfil.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtUsuario, lblUsuario, PlaceHolderType.Leave, "Usuario");
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtUsuario, lblUsuario, PlaceHolderType.Enter, "Usuario");
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPassword, lblPassword, PlaceHolderType.Leave, "Password");
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPassword, lblPassword, PlaceHolderType.Enter, "Password");
        }

        private void txtPIN_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPIN, lblPIN, PlaceHolderType.Leave, "PIN");
        }

        private void txtPIN_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPIN, lblPassword, PlaceHolderType.Enter, "PIN");
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                empleadoId = int.Parse(dgvEmpleados.SelectedRows[0].Cells[0].Value.ToString());
                var empleado = await _context.Empleados.FindAsync(dgvEmpleados.SelectedRows[0].Cells[0].Value);
                if (empleado == null)
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empleadoId = 0;
                    return;
                }
                Clear();
                txtNombre.Text = empleado.Nombre;
                txtTelefono.Text = empleado.Telefono;
                txtCorreo.Text = empleado.Correo;
                chbActivo.Checked = empleado.Activo;
                txtUsuario.Text = empleado.Usuario;

                var user = await _context.User.Include(u => u.Perfil).SingleOrDefaultAsync(u => u.Usuario == empleado.Usuario);
                if (user != null)
                {
                    cbPerfil.Text = user.Perfil.Nombre;
                    txtPassword.Text = user.Password;
                    txtPIN.Text = user.Codigo;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            funciones.ResetForm(panelFormulario);
        }

        private async void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                empleadoId = int.Parse(dgvEmpleados.SelectedRows[0].Cells[0].Value.ToString());
                var empleado = await _context.Empleados.FindAsync(dgvEmpleados.SelectedRows[0].Cells[0].Value);
                if (empleado == null)
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empleadoId = 0;
                    return;
                }
                Clear();
                txtNombre.Text = empleado.Nombre;
                txtTelefono.Text = empleado.Telefono;
                txtCorreo.Text = empleado.Correo;
                chbActivo.Checked = empleado.Activo;
                txtUsuario.Text = empleado.Usuario;

                var user = await _context.User.Include(u => u.Perfil).SingleOrDefaultAsync(u => u.Usuario == empleado.Usuario);
                if (user != null)
                {
                    cbPerfil.Text = user.Perfil.Nombre;
                    txtPassword.Text = user.Password;
                    txtPIN.Text = user.Codigo;
                }

                funciones.BlockTextBox(panelFormulario,false);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
           funciones.ShowLabelName((TextBox)sender, lblNombre);
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((TextBox)sender, lblCorreo);
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((TextBox)sender, lblTelefono);
        }

        private void cbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((ComboBox)sender, lblTelefono);
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((TextBox)sender, lblUsuario);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((TextBox)sender, lblPassword);
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            funciones.ShowLabelName((TextBox)sender, lblPIN);
        }
    }
}
