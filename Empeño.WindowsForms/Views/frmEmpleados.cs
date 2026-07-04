using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Newtonsoft.Json;
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

        // ===== Reutilizable desde la versión nueva (frmShell), SIN mostrar el formulario =====
        // Misma lógica que btnGuardar_Click: valida, crea/edita Empleado + User (Usuario/Codigo/Password/Perfil)
        // y registra bitácora "Empleado". El PIN "Empleado" lo valida frmShell ANTES de llamar.
        public async Task<object> GuardarHeadless(Newtonsoft.Json.Linq.JObject d)
        {
            int id = d["id"] != null ? (int)d["id"] : 0;
            string nombre = ((string)d["nombre"] ?? "").Trim();
            string usuario = ((string)d["usuario"] ?? "").Trim();
            string password = (string)d["password"] ?? "";
            string pin = ((string)d["pin"] ?? "").Trim();
            string perfil = ((string)d["perfil"] ?? "").Trim();
            string telefono = ((string)d["telefono"] ?? "").Trim();
            string correo = ((string)d["correo"] ?? "").Trim();
            bool activo = d["activo"] != null && (bool)d["activo"];

            if (nombre.Length == 0) return new { ok = false, error = "El nombre es obligatorio." };
            if (perfil.Length == 0 || perfil == "Perfil") return new { ok = false, error = "Seleccione el perfil del empleado." };
            var perf = _context.Perfil.FirstOrDefault(p => p.Nombre == perfil);
            if (perf == null) return new { ok = false, error = "El perfil no es válido." };
            int perfilId = perf.PerfilId;

            string accion = id == 0 ? "Crear" : "Editar";
            string usuarioLog = usuario;

            if (id == 0)
            {
                if (usuario.Length == 0) return new { ok = false, error = "El usuario es obligatorio." };
                if (password.Length == 0) return new { ok = false, error = "La contraseña es obligatoria." };
                if (pin.Length == 0) return new { ok = false, error = "El PIN es obligatorio." };
                if (_context.User.Any(u => u.Codigo == pin || u.Usuario == usuario))
                    return new { ok = false, error = "Ya existe un usuario o PIN igual. Elegí un PIN/usuario diferente." };

                _context.Empleados.Add(new Empleado { Nombre = nombre, Telefono = telefono, Correo = correo, Activo = activo, Usuario = usuario });
                _context.User.Add(new User { Activo = activo, Usuario = usuario, Codigo = pin, Password = password, PerfilId = perfilId });
            }
            else
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null) return new { ok = false, error = "El empleado ya no existe." };
                usuarioLog = empleado.Usuario;   // el usuario NO se cambia en edición (igual que el clásico)
                var user = await _context.User.SingleOrDefaultAsync(u => u.Usuario == empleado.Usuario);

                empleado.Correo = correo;
                empleado.Nombre = nombre;
                empleado.Telefono = telefono;
                empleado.Activo = activo;

                if (pin.Length == 0) return new { ok = false, error = "El PIN es obligatorio." };
                if (user != null)
                {
                    user.Activo = activo;
                    // Unicidad de PIN EXCLUYENDO al propio usuario (el clásico se comparaba consigo mismo y bloqueaba el cambio).
                    if (user.Codigo != pin && _context.User.Any(u => u.UsuarioId != user.UsuarioId && u.Codigo == pin))
                        return new { ok = false, error = "Ese PIN ya está en uso por otro usuario." };
                    user.Codigo = pin;
                    if (!string.IsNullOrEmpty(password)) user.Password = password;   // en blanco = mantener la actual
                    user.PerfilId = perfilId;
                    _context.Entry(user).State = EntityState.Modified;
                }
                else
                {
                    _context.User.Add(new User { Activo = activo, Usuario = empleado.Usuario, Codigo = pin, Password = password, PerfilId = perfilId });
                }
                _context.Entry(empleado).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            await funciones.SaveBitacora(new ValorBitacora
            {
                Modulo = "Empleado",
                Accion = accion,
                Valor = JsonConvert.SerializeObject(new { Nombre = nombre, Usuario = usuarioLog, Correo = correo, Telefono = telefono, Perfil = perfil, Activo = activo })
            });
            return new { ok = true };
        }

        // Baja = soft-delete del empleado y sus usuarios (igual que btnEliminar_Click). PIN validado en frmShell.
        public async Task<object> EliminarHeadless(int id)
        {
            var dato = await _context.Empleados.FindAsync(id);
            if (dato == null) return new { ok = false, error = "El empleado ya no existe." };

            dato.Activo = false;
            _context.Entry(dato).State = EntityState.Modified;

            var usuarios = _context.User.Where(u => u.Usuario == dato.Usuario).ToList();
            foreach (var u in usuarios)
            {
                u.Activo = false;
                _context.Entry(u).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            await funciones.SaveBitacora(new ValorBitacora
            {
                Modulo = "Empleado",
                Accion = "Desactivar",
                Valor = JsonConvert.SerializeObject(new { dato.EmpleadoId, dato.Nombre, dato.Usuario })
            });
            return new { ok = true };
        }

        private async void frmEmpleados_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        #region Funciones
        public async Task LoadData() 
        {
            dgvEmpleados.DataSource = await _context.Empleados.Where(d=>d.Usuario!="Admin" && d.Activo).Select(x => new
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
            funciones.PlaceHolder(txtPIN, PlaceHolderType.Leave, "PIN");
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
                if (!funciones.ValidatePIN("Empleado"))
                    return;

                string accion = empleadoId == 0 ? "Crear" : "Editar";

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

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Empleado",
                    Accion = accion,
                    Valor = JsonConvert.SerializeObject(new
                    {
                        Nombre = txtNombre.Text,
                        Usuario = txtUsuario.Text,
                        Correo = txtCorreo.Text,
                        Telefono = txtTelefono.Text,
                        Perfil = cbPerfil.Text,
                        Activo = chbActivo.Checked
                    })
                });

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
                    txtPassword.UseSystemPasswordChar = true;
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
            if (!funciones.ValidatePIN("Empleado"))
                return;

            var result = MessageBox.Show("¿Está seguro que desea desactivar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            if (dgvEmpleados.SelectedRows.Count <= 0)
                return;

            try
            {
                var dato = await _context.Empleados.FindAsync(dgvEmpleados.SelectedRows[0].Cells[0].Value);
                if (dato == null)
                    return;

                // Soft-delete: se desactiva empleado y usuarios, sin borrar (conserva el historial y evita romper FK)
                dato.Activo = false;
                _context.Entry(dato).State = EntityState.Modified;

                var usuarios = _context.User.Where(u => u.Usuario == dato.Usuario).ToList();
                foreach (var u in usuarios)
                {
                    u.Activo = false;
                    _context.Entry(u).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Empleado",
                    Accion = "Desactivar",
                    Valor = JsonConvert.SerializeObject(new { dato.EmpleadoId, dato.Nombre, dato.Usuario })
                });

                funciones.ResetForm(panelFormulario);
                empleadoId = 0;
                await LoadData();
                MessageBox.Show("El empleado fue desactivado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
