using Empeño.Common.Entities;
using Empeño.Forms.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empeño.Forms.Views
{
    public partial class frmEmpleado : Form
    {
        public DataContext _context = new DataContext();

        int empleadoId = 0;

        DataGridView dt = new DataGridView();

        public frmEmpleado()
        {
            InitializeComponent();
        }

        private void frmEmpleado_Load(object sender, EventArgs e)
        {
            dt = new DataGridView()
            {
                Location = new Point { X = 538, Y = 88 },
                Size= new Size { Width= 483, Height= 328 },
                SelectionMode=DataGridViewSelectionMode.FullRowSelect
            };         
            Controls.Add(dt);
            LoadData();
            DataGridViewColumn column = dt.Columns[0];
            column.Width = 50;
            Clear();
        }

        private void Clear() 
        {
            txtNombre.Text = string.Empty;
            empleadoId = 0;
            txtContrasena.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtPerfil.Text = string.Empty;
            txtPIN.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtNombre.Focus();
            ckbActivo.Checked = true;
            cbPerfil.Text = "Empleado";
        }

        private void LoadData() 
        {
            var empleados = _context.Empleados.Select(x=> new {Id=x.EmpleadoId, x.Nombre, Teléfono= x.Telefono, x.Correo, x.Usuario, x.Activo }).ToList();
            dt.DataSource = empleados;
            dt.CellClick += new DataGridViewCellEventHandler(Edit);
            lblContador.Text = empleados.Count().ToString();
        }

        private void Edit(object sender, EventArgs e)
        {
            empleadoId =int.Parse(dt.SelectedRows[0].Cells[0].Value.ToString());
            var empleado = _context.Empleados.Find(empleadoId);
            if (empleado==null)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                empleadoId = 0;
                return;
            }
            Clear();
            txtNombre.Text = empleado.Nombre;
            txtTelefono.Text = empleado.Telefono;
            txtCorreo.Text = empleado.Correo;
            ckbActivo.Checked = empleado.Activo;
            txtUsuario.Text = empleado.Usuario;

            var user = _context.User.Include(u=>u.Perfil).Single(u => u.Usuario == empleado.Usuario);

            cbPerfil.Text = user.Perfil.Nombre;
            txtContrasena.Text = user.Password;
            txtPIN.Text = user.Codigo;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
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
                        Correo= txtCorreo.Text,
                        Activo=ckbActivo.Checked,
                        Usuario=txtUsuario.Text
                    };

                    var user = new User
                    {
                        Activo = ckbActivo.Checked,
                        Usuario = txtUsuario.Text,
                        Codigo = txtPIN.Text,
                        Password = txtContrasena.Text,
                        PerfilId = GetPerfilId(cbPerfil.Text)
                    };

                    _context.Empleados.Add(empleado);
                    _context.User.Add(user);

                }
                else
                {
                    var empleado = _context.Empleados.Find(empleadoId);
                    var user = _context.User.Single(u=>u.Usuario==empleado.Usuario);

                    empleado.Correo = txtCorreo.Text;
                    empleado.Nombre = txtNombre.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Activo = ckbActivo.Checked;
                    empleado.Usuario = txtUsuario.Text;

                    user.Activo = ckbActivo.Checked;
                    user.Usuario = txtUsuario.Text;
                    user.Codigo = txtPIN.Text;
                    user.Password = txtContrasena.Text;                    

                    _context.Entry(empleado).State = EntityState.Modified;
                    _context.Entry(user).State = EntityState.Modified;
                }
                _context.SaveChanges();
                LoadData();
                Clear();
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetPerfilId(string text)
        {
            return _context.Perfil.Single(p => p.Nombre == text).PerfilId;
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
