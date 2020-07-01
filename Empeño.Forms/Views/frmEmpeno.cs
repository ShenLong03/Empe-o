using Empeño.Common.Entities;
using Empeño.Common.Enum;
using Empeño.Forms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empeño.Forms.Views
{
    public partial class frmEmpeno : Form
    {
        public int clienteId=0;

        DataContext _context = new DataContext();

        public frmEmpeno()
        {
            InitializeComponent();
        }

        private void frmEmpeno_Load(object sender, EventArgs e)
        {
            txtVencimmiento.Text = DateTime.Today.AddMonths(3).ToString("dd/MM/yyyy");
            txtIdentificacion.Focus();
            cbInteres.Text = "10%";
        }

        private void btnBuscarIdentificacion_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = _context.Clientes.SingleOrDefault(c => c.Identificacion == txtIdentificacion.Text);
                if (cliente!=null)
                {
                    txtNombre.Text = cliente.Nombre;
                    txtCorreo.Text = cliente.Correo;
                    txtTelefono.Text = cliente.Telefono;
                    clienteId = cliente.ClienteId;
                }
                else
                {
                    clienteId = 0;
                    MessageBox.Show("No se encuentra, agregue los datos del cliente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var cliente = _context.Clientes.SingleOrDefault(c => c.Identificacion == txtIdentificacion.Text);
            if (cliente!=null)
            {
                cliente.Nombre = txtNombre.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Correo = txtCorreo.Text;
                cliente.Identificacion = txtIdentificacion.Text;
                _context.Entry(cliente).State = EntityState.Modified;
            }
            else
            {
                cliente = new Cliente();
                cliente.Nombre = txtNombre.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Correo = txtCorreo.Text;
                _context.Clientes.Add(cliente);
            }

            _context.SaveChanges();

            var empeno = new Empeno
            {
                ClienteId = cliente.ClienteId,
                Descripcion = txtDescripcion.Text,
                EmpleadoId = 1,
                EsOro=ckbEsOro.Checked,
                Estado=Estado.Activo,
                Fecha=DateTime.Now,
                FechaVencimiento=DateTime.ParseExact(txtVencimmiento.Text,"dd/MM/yyyy", CultureInfo.InvariantCulture),
                Monto= double.Parse(txtMonto.Text),
            };

            MessageBox.Show("Empeño realizado!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            var monto = double.Parse(txtMonto.Text);
            if (monto>0)
            {
                var resp = (monto * (double)((double)double.Parse(cbInteres.Text.Replace("%", "")) / (double)100)).ToString("N2");
                txtMensualidad.Text = resp;
            }
            else
            {
                txtMensualidad.Text = "0.00";
            }
        }
    }
}
