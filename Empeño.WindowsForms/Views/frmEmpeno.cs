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
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmEmpeno : Form
    {
        DataContext _context = new DataContext();
        List<Cliente> clientesInicial = new List<Cliente>();
        List<Empeno> empenosInicial = new List<Empeno>();
        public frmEmpeno()
        {
            InitializeComponent();
            clientesInicial.Add(new Cliente());
            empenosInicial.Add(new Empeno());
        }

        private void tableLayoutPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

   

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmEmpeno_Load(object sender, EventArgs e)
        {
            lblRealizado.Text = Program.Usuario.Usuario;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (txtBuscar.Text!=" Buscar" && txtBuscar.Text != "" && txtBuscar.TextLength >= 3)
            {
                dgvClientes.DataSource = _context.Clientes.Where(w=>w.Nombre.Contains(txtBuscar.Text) || w.Identificacion.Contains(txtBuscar.Text)).Select(x => new
                {
                    Id = x.ClienteId,
                    x.Nombre,
                    x.Correo,
                    x.Telefono,
                }).ToList();

                int number;
                int.TryParse(txtBuscar.Text, out number);
                if (number!=0)
                {
                    dgvEmpeños.DataSource = _context.Empenos.Where(w=>w.EmpenoId==number).Select(x => new
                    {
                        Id = x.EmpenoId,
                        x.Descripcion,
                        x.Interes,
                        x.Monto,
                    }).ToList();
                }
            }                            
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                txtBuscar.Text = " Buscar";
                txtBuscar.ForeColor = Color.DimGray;
            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == " Buscar")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se encuentra en desarrollo");
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se encuentra en desarrollo");
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se encuentra en desarrollo");
        }

        int clienteId = 0;

        private async void btnEmpeñar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count>0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (cliente!=null)
                {
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;
                    clienteId = cliente.ClienteId;
                }
            }
        }

        private async void btnGuardarEmpeño_Click(object sender, EventArgs e)
        {
            if (clienteId!=0)
            {
                var cliente = await _context.Clientes.FindAsync(clienteId);

                var empeño = new Empeno
                {
                    ClienteId = clienteId,
                    Descripcion = txtDescripcion.Text,
                    EmpleadoId = 1,
                    EsOro = chbEsOro.Checked,
                    Estado = Estado.Activo,
                    Fecha = DateTime.Now,
                    FechaVencimiento = DateTime.Today.AddMonths(3),
                    InteresId =1,
                    Monto= double.Parse(txtMonto.Text),
                    MontoPendiente=double.Parse(txtMonto.Text)
                };

                _context.Empenos.Add(empeño);
                await _context.SaveChangesAsync();
            }

            dgvEmpeños.DataSource = await _context.Empenos.Select(x => new
            {
                Id=x.EmpenoId,
                x.Descripcion,
                Interes=x.Interes.Porcentaje,
                x.Monto,
                x.MontoPendiente
            }).ToListAsync();

            MessageBox.Show("Datos guardados correctamente");
        }

        private async void btnEditarEmpeño_Click(object sender, EventArgs e)
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño= await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                if (empeño!=null)
                {
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = "";
                    txtInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    lblRealizado.Text = empeño.Empleado.Usuario;
                }
            }
        }

        private async void btnEmpeñar_Click_1(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (empeño != null)
                {
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = "";
                    txtInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    lblRealizado.Text = empeño.Empleado.Usuario;
                }
            }
        }

        private async void btnEmpeñar_Click_2(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (cliente != null)
                {
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;                    
                }
            }
        }
    }
}
