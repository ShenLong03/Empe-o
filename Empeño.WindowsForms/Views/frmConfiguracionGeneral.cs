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
            if (_context.Configuraciones.Any())
            {
                var configuracion = _context.Configuraciones.FirstOrDefault();
                txtIdentificacion.Text = configuracion.Identificacion;
                txtCompania.Text = configuracion.Nombre;
                txtMeses.Text = configuracion.Meses.ToString();
                txtTelefono.Text = configuracion.Telefono;
                configuracionId = configuracion.ConfiguracionId;
            }

            txtCompania.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (configuracionId == 0)
                {
                    var configuracion = new Configuracion
                    {
                        ConfiguracionId = configuracionId,
                        Identificacion = txtIdentificacion.Text,
                        Meses = int.Parse(txtMeses.Text),
                        Nombre = txtCompania.Text,
                        Telefono = txtTelefono.Text,
                    };

                    _context.Configuraciones.Add(configuracion);
                }
                else
                {
                    var configuracion = _context.Configuraciones.Find(configuracionId);

                    configuracion.Identificacion = txtIdentificacion.Text;
                    configuracion.Meses = int.Parse(txtMeses.Text);
                    configuracion.Nombre = txtCompania.Text;
                    configuracion.Telefono = txtTelefono.Text;

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
