using Empeño.CommonEF.Entities;
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
    public partial class frmConfiguracionGeneral : Form
    {
        private DataContext _context = new DataContext();
        int configuracionId = 0;

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
            }

            txtCompania.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                frmOscuro oscuro = new frmOscuro();
                oscuro.Show();
                frmPIN pin = new frmPIN("Empeño");
                pin.ShowDialog();
                if (!Program.Acceso)
                {
                    oscuro.Close();
                    MessageBox.Show("No tiene acceso a este módulo");
                    return;
                }
                oscuro.Close();


                if (configuracionId == 0)
                {
                    var configuracion = new Configuracion
                    {
                        ConfiguracionId = configuracionId,
                        Identificacion = txtIdentificacion.Text,
                        Meses = int.Parse(txtMeses.Text),
                        Compañia = txtCompania.Text,
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Puerto = int.Parse(txtPuerto.Text),
                        SMTP = txtSMTP.Text,
                        SSL = chkSSL.Checked,
                        Direccion = txtDirección.Text,
                        EmailNotification = txtEmailAdmin.Text
                    };

                    _context.Configuraciones.Add(configuracion);
                }
                else
                {
                    var configuracion = _context.Configuraciones.Find(configuracionId);

                    configuracion.Identificacion = txtIdentificacion.Text;
                    configuracion.Meses = int.Parse(txtMeses.Text);
                    configuracion.Nombre = txtNombre.Text;
                    configuracion.Compañia = txtCompania.Text;
                    configuracion.Telefono = txtTelefono.Text;            
                    configuracion.Email = txtEmail.Text;
                    configuracion.Password = txtPassword.Text;
                    configuracion.Puerto = int.Parse(txtPuerto.Text);
                    configuracion.SMTP = txtSMTP.Text;
                    configuracion.SSL = chkSSL.Checked;
                    configuracion.Direccion = txtDirección.Text;
                    configuracion.EmailNotification = txtEmailAdmin.Text;

                    _context.Entry(configuracion).State = EntityState.Modified;
                }
                _context.SaveChanges();
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
