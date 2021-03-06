﻿using Empeño.CommonEF.Entities;
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
            dgvEmpleados.DataSource = await _context.Empleados.Where(d=>d.Usuario!="Admin").Select(x => new
            {
                Id = x.EmpleadoId,
                x.Nombre,
                x.Correo,
                x.Telefono
            }).ToListAsync();

            lblCantidad.Text = dgvEmpleados.Rows.Count.ToString();

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
                if (!Validate(txtNombre, lblNombre))
                    return;
                if (!Validate(txtUsuario, lblUsuario))
                    return;
                if (!Validate(txtPassword, lblPassword))
                    return;
                if (!Validate(txtPIN, lblPIN))
                    return;

                if (cbPerfil.Text=="Perfil")
                {
                    MessageBox.Show("Seleccione el perfil del empleado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (empleadoId == 0)
                {
                    if (_context.User.Where(d=>d.Codigo==txtPIN.Text || d.Usuario==txtUsuario.Text).Count()>0)
                    {
                        MessageBox.Show("Debe seleccionar un PIN diferente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var empleado = new Empleado
                    {
                        EmpleadoId = empleadoId,
                        Nombre = GetValue(txtNombre,lblNombre),
                        Telefono =  GetValue(txtTelefono,lblTelefono),
                        Correo = GetValue(txtCorreo,lblCorreo),
                        Activo = chbActivo.Checked,
                        Usuario = GetValue(txtUsuario,lblUsuario)
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
                    var user = await _context.User.SingleOrDefaultAsync(u => u.Usuario == empleado.Usuario);

                    empleado.Correo = txtCorreo.Text;
                    empleado.Nombre = txtNombre.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Activo = chbActivo.Checked;
                    empleado.Usuario = txtUsuario.Text;

                    if (user!=null)
                    {
                        user.Activo = chbActivo.Checked;
                        user.Usuario = txtUsuario.Text;
                        if (user.Codigo!=txtPIN.Text)
                        {
                            if (_context.User.Where(d => d.Codigo == txtPIN.Text || d.Usuario == txtUsuario.Text).Count() > 0)
                            {
                                MessageBox.Show("Debe seleccionar un PIN diferente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtPIN.Text = user.Codigo;
                                return;
                            }
                        }
                        user.Codigo = txtPIN.Text;
                        user.Password = txtPassword.Text;
                        user.PerfilId = GetPerfilId(cbPerfil.Text);

                        _context.Entry(user).State = EntityState.Modified;
                    }
                    else
                    {
                        user = new User
                        {
                            Activo = chbActivo.Checked,
                            Usuario = txtUsuario.Text,
                            Codigo = txtPIN.Text,
                            Password = txtPassword.Text,
                            PerfilId = GetPerfilId(cbPerfil.Text)
                        };

                        _context.User.Add(user);
                    }
                    
                    _context.Entry(empleado).State = EntityState.Modified;                    
                }
                await _context.SaveChangesAsync();
                await LoadData();
                funciones.ResetForm(panelFormulario);
                empleadoId = 0;
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validate(TextBox txt, Label lbl)
        {
            if (txt.Text==lbl.Text)
            {
                MessageBox.Show("El campo " + lbl.Text + " es un campo requerido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private string GetValue(TextBox txt, Label lbl)
        {
            if (txt.Text == lbl.Text)
            {                
                return string.Empty;
            }
            return txt.Text;
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
            panelFormulario.BackColor = Color.White;
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Enter, "Nombre");

            panelFormulario.BackColor = Color.White;
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Leave, "Correo");
            panelFormulario.BackColor = Color.White;
        }

        private void txtCorreo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Enter, "Correo");

            panelFormulario.BackColor = Color.White;
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Leave, "Teléfono");
            panelFormulario.BackColor = Color.White;
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Enter, "Teléfono");

            panelFormulario.BackColor = Color.White;
        }

        private void cbPerfil_Leave(object sender, EventArgs e)
        {
            if (cbPerfil.Text == "")
            {
                cbPerfil.Text = "Perfil";
                cbPerfil.ForeColor = Color.DimGray;
            }

            panelFormulario.BackColor = Color.White;
        }

        private void cbPerfil_Enter(object sender, EventArgs e)
        {
            if (cbPerfil.Text == "Perfil")
            {
                cbPerfil.Text = "";
                cbPerfil.ForeColor = Color.Black;
            }

            panelFormulario.BackColor = Color.White;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtUsuario, lblUsuario, PlaceHolderType.Leave, "Usuario");

            panelFormulario.BackColor = Color.White;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtUsuario, lblUsuario, PlaceHolderType.Enter, "Usuario");

            panelFormulario.BackColor = Color.White;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPassword, lblPassword, PlaceHolderType.Leave, "Contraseña");
            
            if (txtPassword.Text == "Contraseña")
                txtPassword.UseSystemPasswordChar = false;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {            
            funciones.PlaceHolder(txtPassword, lblPassword, PlaceHolderType.Enter, "Contraseña");

            if (txtPassword.Text == "")
                txtPassword.UseSystemPasswordChar = true;
        }

        private void txtPIN_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPIN, lblPIN, PlaceHolderType.Leave, "PIN");
        }

        private void txtPIN_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtPIN, lblPIN, PlaceHolderType.Enter, "PIN");
        }

        public async Task Editar()
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {                
                var empleado = await _context.Empleados.FindAsync(dgvEmpleados.SelectedRows[0].Cells[0].Value);
                if (empleado == null)
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empleadoId = 0;
                    return;
                }
                funciones.ResetForm(panelFormulario);
                empleadoId = 0;
                empleadoId = int.Parse(dgvEmpleados.SelectedRows[0].Cells[0].Value.ToString());
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
                    txtPassword.UseSystemPasswordChar = true;
                    txtPIN.Text = user.Codigo;
                }
                
                funciones.BlockTextBox(panelFormulario, true);
                funciones.EditTextColor(panelFormulario);
                funciones.ShowLabels(panelFormulario);
                funciones.TextBoxColorBlank(panelFormulario);

                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            await Editar();

            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            funciones.ResetForm(panelFormulario);
            empleadoId = 0;
            cbPerfil.Text = "Perfil";
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
                funciones.ResetForm(panelFormulario);
                empleadoId = 0;
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
           //funciones.ShowLabelName((TextBox)sender, lblNombre);
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            //funciones.ShowLabelName((TextBox)sender, lblCorreo);
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            //funciones.ShowLabelName((TextBox)sender, lblTelefono);
        }

        private void cbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPerfil.Text!="Perfil")
            {
                funciones.ShowLabelName((ComboBox)sender, lblPerfil);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            //funciones.ShowLabelName((TextBox)sender, lblUsuario);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //funciones.ShowLabelName((TextBox)sender, lblPassword);
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            //funciones.ShowLabelName((TextBox)sender, lblPIN);
        }

        private async void dgvEmpleados_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                await Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
            }
            
        }

        private  async void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count>0)
            {
                var usuario = await _context.User.SingleOrDefaultAsync(u => u.Usuario == txtUsuario.Text);
                if (usuario!=null)
                {
                    Program.ChangeUserPassword = usuario;
                    var oscuro = new frmOscuro();
                    var frm = new frmCambioPassword();
                    oscuro.Show();
                    frm.ShowDialog();
                    oscuro.Close();
                    txtPassword.Text = Program.ChangeUserPassword.Password;
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {


                var result = MessageBox.Show("¿Esta séguro que desea eliminar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (dgvEmpleados.SelectedRows.Count > 0)
                    {
                        var dato = await _context.Empleados.FindAsync(dgvEmpleados.SelectedRows[0].Cells[0].Value);
                        _context.Empleados.Remove(dato);
                        _context.User.RemoveRange(_context.User.Where(u => u.Usuario == dato.Usuario));
                        await _context.SaveChangesAsync();
                        funciones.ResetForm(panelFormulario);
                        empleadoId = 0;
                        await LoadData();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("El dato no puede ser eliminado", "Error");
            }
           
        }  
    }
}
