using Empeño.CommonEF.Entities;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmConfiguracionGeneral : Form
    {
        private DataContext _context = new DataContext();
        int configuracionId = 0;
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmConfiguracionGeneral()
        {
            InitializeComponent();
        }

        private void frmConfiguracionGeneral_Load(object sender, EventArgs e)
        {
            if (_context.Configuraciones.Count()>0)
            {
                var configuracion = _context.Configuraciones.FirstOrDefault();
                txtIdentificacion.Text = configuracion.Identificacion;
                txtCompania.Text = configuracion.Compañia;
                txtNombre.Text = configuracion.Nombre;
                txtMeses.Text = configuracion.Meses.ToString();
                txtTelefono.Text = configuracion.Telefono;
                configuracionId = configuracion.ConfiguracionId;
                txtEmail.Text = configuracion.Email;
                txtPassword.Text = configuracion.Password;
                txtSMTP.Text = configuracion.SMTP;
                txtPuerto.Text = configuracion.Puerto.ToString() ;
                chkSSL.Checked = configuracion.SSL;
                txtDirección.Text = configuracion.Direccion;
                txtEmailAdmin.Text = configuracion.EmailNotification;
                txtIVA.Text = configuracion.IVA.ToString();
            }
            else
            {
                txtPuerto.Text = "587";
            }
            
            txtCompania.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!funciones.ValidatePIN("Configuración"))
                    return;

                // Validación de datos (sin cambios de base de datos)
                int meses;
                if (!int.TryParse(txtMeses.Text, out meses) || meses <= 0)
                {
                    MessageBox.Show("El plazo (Meses) debe ser un número entero mayor a cero.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int puerto = 0;
                if (!string.IsNullOrEmpty(txtPuerto.Text) && !int.TryParse(txtPuerto.Text, out puerto))
                {
                    MessageBox.Show("El puerto debe ser un número entero.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double iva = 0;
                if (!string.IsNullOrEmpty(txtIVA.Text) && !double.TryParse(txtIVA.Text, out iva))
                {
                    MessageBox.Show("El IVA debe ser un número.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (iva < 0 || iva > 100)
                {
                    MessageBox.Show("El IVA debe estar entre 0 y 100.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object antes = null;
                string accion;

                if (configuracionId == 0)
                {
                    accion = "Crear";
                    var configuracion = new Configuracion
                    {
                        ConfiguracionId = configuracionId,
                        Identificacion = txtIdentificacion.Text,
                        Meses = meses,
                        Compañia = txtCompania.Text,
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Puerto = puerto,
                        SMTP = txtSMTP.Text,
                        SSL = chkSSL.Checked,
                        Direccion = txtDirección.Text,
                        EmailNotification = txtEmailAdmin.Text,
                        IVA = iva
                    };

                    _context.Configuraciones.Add(configuracion);
                }
                else
                {
                    accion = "Editar";
                    var configuracion = _context.Configuraciones.Find(configuracionId);

                    antes = new
                    {
                        configuracion.Identificacion,
                        configuracion.Compañia,
                        configuracion.Nombre,
                        configuracion.Telefono,
                        configuracion.Direccion,
                        configuracion.Meses,
                        configuracion.IVA,
                        configuracion.SMTP,
                        configuracion.Puerto,
                        configuracion.SSL,
                        configuracion.Email,
                        configuracion.EmailNotification
                    };

                    configuracion.Identificacion = txtIdentificacion.Text;
                    configuracion.Meses = meses;
                    configuracion.Nombre = txtNombre.Text;
                    configuracion.Compañia = txtCompania.Text;
                    configuracion.Telefono = txtTelefono.Text;
                    configuracion.Email = txtEmail.Text;
                    configuracion.Password = txtPassword.Text;
                    configuracion.Puerto = puerto;
                    configuracion.SMTP = txtSMTP.Text;
                    configuracion.SSL = chkSSL.Checked;
                    configuracion.Direccion = txtDirección.Text;
                    configuracion.EmailNotification = txtEmailAdmin.Text;
                    configuracion.IVA = iva;

                    _context.Entry(configuracion).State = EntityState.Modified;
                }
                _context.SaveChanges();

                // Auditoría del cambio (no se registra la contraseña SMTP)
                var despues = new
                {
                    Identificacion = txtIdentificacion.Text,
                    Compañia = txtCompania.Text,
                    Nombre = txtNombre.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDirección.Text,
                    Meses = meses,
                    IVA = iva,
                    SMTP = txtSMTP.Text,
                    Puerto = puerto,
                    SSL = chkSSL.Checked,
                    Email = txtEmail.Text,
                    EmailNotification = txtEmailAdmin.Text
                };

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Configuración",
                    Accion = accion,
                    Valor = JsonConvert.SerializeObject(new { antes, despues })
                });

                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
