using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Design;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
        Funciones.Funciones funciones = new Funciones.Funciones();
        int empeñoId = 0;

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

        private async void frmEmpeno_Load(object sender, EventArgs e)
        {
            Realizado.Text = Program.Usuario.Usuario;
            cbInteres.DataSource = await _context.Interes.ToListAsync();
            cbInteres.DisplayMember = "Nombre";
            cbInteres.ValueMember = "InteresId";
            cbInteres.Text = "Porcentaje";
            funciones.ResetForm(panelFormulario);

            Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblVence.Text= DateTime.Today.AddMonths(3).ToString("dd/MM/yyyy");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }


        public async Task Buscar() 
        {
            if (txtBuscar.Text != " Buscar" && txtBuscar.Text != "")
            {
                dgvClientes.DataSource = _context.Clientes.Where(w => w.Nombre.Contains(txtBuscar.Text) || w.Identificacion.Contains(txtBuscar.Text)).Select(x => new
                {
                    Id = x.ClienteId,
                    x.Nombre,
                    x.Correo,
                    x.Telefono,
                }).ToList();

                int number;
                int.TryParse(txtBuscar.Text, out number);
                if (number != 0)
                {
                    dgvEmpeños.DataSource = _context.Empenos.Where(w => w.EmpenoId == number).Select(x => new
                    {
                        Id = x.EmpenoId,
                        x.Descripcion,
                        Empleado = x.Empleado.Nombre,
                        Es_Oro = x.EsOro,
                        x.Estado,
                         x.Fecha,
                        Fecha_Vencimiento = x.FechaVencimiento,
                        Interes = x.Interes.Porcentaje + "%",
                        x.Monto,
                        Monto_Pendiente = x.MontoPendiente
                    }).ToList();

                    await BuscarEmpeño(number);

                }
            }
            lblCatidadEmpeños.Text = dgvEmpeños.RowCount.ToString();
            lblCantidad.Text = dgvClientes.RowCount.ToString();

            if (dgvEmpeños.ColumnCount>0)
            {
                DataGridViewColumn column = dgvEmpeños.Columns[0];
                column.Width = 40;
            }
            if (dgvClientes.ColumnCount>0)
            {
                DataGridViewColumn column = dgvClientes.Columns[0];
                column.Width = 40;
            }

            
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != " Buscar" && txtBuscar.Text != "" && txtBuscar.TextLength >= 3)
            {
                await Buscar();
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
                if (empeñoId>0)
                {
                    var empeño = await _context.Empenos.FindAsync(empeñoId);

                    empeño.ClienteId = clienteId;
                    empeño.Descripcion = txtDescripcion.Text;
                    empeño.EmpleadoId = 1;
                    empeño.EsOro = chbEsOro.Checked;
                    empeño.Estado = Estado.Activo;
                    empeño.Fecha = DateTime.Now;
                    empeño.FechaVencimiento = DateTime.Today.AddMonths(3);
                    empeño.InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text);
                    empeño.Monto = double.Parse(txtMonto.Text);
                    empeño.MontoPendiente = double.Parse(txtMonto.Text);

                    _context.Entry(empeño).State=EntityState.Modified;
                }
                else
                {
                    var empeño = new Empeno
                    {
                        ClienteId = clienteId,
                        Descripcion = txtDescripcion.Text,
                        EmpleadoId = 1,
                        EsOro = chbEsOro.Checked,
                        Estado = Estado.Activo,
                        Fecha = DateTime.Now,
                        FechaVencimiento = DateTime.Today.AddMonths(3),
                        InteresId = 1,
                        Monto = double.Parse(txtMonto.Text),
                        MontoPendiente = double.Parse(txtMonto.Text)
                    };

                    _context.Empenos.Add(empeño);
                }
           
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
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    Realizado.Text = empeño.Empleado.Usuario;
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

        private async void btnEditarEmpeño_Click_1(object sender, EventArgs e)
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                if (empeño != null)
                {
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = "";
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    Realizado.Text = empeño.Empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                }
            }
        }


        public async Task LoadEmpeños() 
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                var lista= cliente.Empenos.Select(x => new
                {
                    Id = x.EmpenoId,
                    x.Descripcion,
                    Empleado = x.Empleado.Nombre,
                    Es_Oro = x.EsOro,
                    x.Estado,
                    Fecha = x.Fecha.ToString("dd/MM/yyyy"),
                    Fecha_Vencimiento = x.FechaVencimiento,
                    Dias = 0,
                    Interes = x.Interes.Porcentaje,
                    x.Monto,
                    Monto_Pendiente = x.MontoPendiente
                }).ToList();

                dgvEmpeños.DataSource = lista.Select(x =>  new
                {
                    x.Id,
                    x.Descripcion,
                    x.Empleado,
                    x.Es_Oro,
                    x.Estado,
                    x.Fecha,
                    x.Fecha_Vencimiento,
                    Dias = x.Dias,
                    //Dias = DbFunctions.DiffDays(x.Fecha_Vencimiento, DateTime.Today),
                    Interes = x.Interes + "%",
                    x.Monto,
                    x.Monto_Pendiente
                }).ToList();

                DataGridViewColumn column = dgvEmpeños.Columns[0];
                column.Width = 40;

                funciones.BlockTextBox(panelFormulario, false);
            }
        }

        private async void btnVerEmpleado_Click(object sender, EventArgs e)
        {
            await LoadEmpeños();
        }

        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Buscar();

            }
        }

        public async Task<int> GetInteresId(string nombre) 
        {
            var interes = await _context.Interes.SingleOrDefaultAsync(x => x.Nombre == nombre);
            if (interes!=null)
            {
                return interes.InteresId;
            }
            return 0;
        }

        private async void btnGuardarEmpeño_Click_1(object sender, EventArgs e)
        {
            if (empeñoId > 0)
            {
                var empeño = await _context.Empenos.FindAsync(empeñoId);

                empeño.ClienteId = clienteId>0?clienteId:empeño.ClienteId;
                empeño.Descripcion = txtDescripcion.Text;
                empeño.EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                empeño.EsOro = chbEsOro.Checked;
                empeño.Estado = Estado.Activo;
                empeño.Fecha = DateTime.Now;
                empeño.FechaVencimiento = DateTime.Today.AddMonths(3);
                empeño.InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text);
                var interes = await _context.Interes.FindAsync(empeño.InteresId);
                empeño.Monto = double.Parse(txtMonto.Text);
                empeño.MontoPendiente = double.Parse(txtMonto.Text);

                _context.Entry(empeño).State = EntityState.Modified;

                var intereses = new Intereses
                {
                    EmpenoId=empeño.EmpenoId,
                    FechaCreacion=DateTime.Now,
                    FechaVencimiento=DateTime.Today.AddMonths(3),
                    Monto=(double)empeño.Monto*((double)interes.Porcentaje/(double)100)
                };

                _context.Intereses.Add(intereses);
                await _context.SaveChangesAsync();

                dgvPagos.DataSource = await _context.Pago.ToListAsync();
            }
            else
            {
                var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);               

                var empeño = new Empeno
                {
                    ClienteId = clienteId,
                    Descripcion = txtDescripcion.Text,
                    EmpleadoId =empleadoId,
                    EsOro = chbEsOro.Checked,
                    Estado = Estado.Activo,
                    Fecha = DateTime.Now,
                    FechaVencimiento = DateTime.Today.AddMonths(3),
                    InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text),
                    Monto = double.Parse(txtMonto.Text),
                    MontoPendiente = double.Parse(txtMonto.Text)
                };
                
                var interes = await _context.Interes.FindAsync(empeño.InteresId);

                _context.Empenos.Add(empeño);
                await _context.SaveChangesAsync();

                var intereses = new Intereses
                {
                    EmpenoId = empeño.EmpenoId,
                    FechaCreacion = DateTime.Now,
                    FechaVencimiento = DateTime.Today.AddMonths(3),
                    Monto = (double)empeño.Monto * ((double)interes.Porcentaje / (double)100)
                };

                _context.Intereses.Add(intereses);
                await _context.SaveChangesAsync();

                dgvPagos.DataSource = await _context.Intereses.Select(x => new
                {
                    Id = x.InteresesId,
                    Interes = x.Empeno.Interes.Porcentaje,
                    Fecha_Vencimiento=x.FechaVencimiento,
                    x.Monto,
                    x.Pagado,
                    Vencimiento=DbFunctions.DiffDays(x.FechaVencimiento,DateTime.Today),
                   //Vencimiento = -(x.FechaVencimiento - DateTime.Now).TotalDays,
                }).ToListAsync();
            }

            await _context.SaveChangesAsync();

            dgvEmpeños.DataSource = await _context.Empenos.Select(x => new
            {
                Id = x.EmpenoId,
                x.Descripcion,
                Interes = x.Interes.Porcentaje,
                x.Monto,
                x.MontoPendiente
            }).ToListAsync();

            MessageBox.Show("Datos guardados correctamente");
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIdentificacion, lblIdentificacion, PlaceHolderType.Leave, "Identificación");
        }

        private void txtIdentificacion_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIdentificacion, lblIdentificacion, PlaceHolderType.Enter, "Identificación");
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Enter, "Nombre");
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtDescripcion, lblDescripcion, PlaceHolderType.Leave, "Descripción");
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtDescripcion, lblDescripcion, PlaceHolderType.Enter, "Descripción");
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtComentario, lblComentario, PlaceHolderType.Leave, "Comentarios");
        }

        private void txtComentario_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtComentario, lblComentario, PlaceHolderType.Enter, "Comentarios");
        }

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMonto, lblMonto, PlaceHolderType.Leave, "Monto");
        }

        private void txtMonto_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMonto, lblMonto, PlaceHolderType.Enter, "Monto");
        }

        private void cbInteres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInteres.Text != "Porcentaje")
            {
                funciones.ShowLabelName((ComboBox)sender, lblInteres);
            }
        }

        public async Task BuscarEmpeño()
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                if (empeño != null)
                {
                    empeñoId = empeño.EmpenoId;
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = "";
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    Realizado.Text = empeño.Empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    Realizado.Text = empeño.Empleado.Usuario;
                    lblVence.Text = empeño.FechaVencimiento.ToString("dd/MM/yyyy");

                    funciones.ShowLabels(panelFormulario);
                    funciones.BlockTextBox(panelFormulario, true);
                    funciones.EditTextColor(panelFormulario);
                }
            }
        }

        public async Task BuscarEmpeño(int id)
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(id);
                if (empeño != null)
                {
                    empeñoId = empeño.EmpenoId;
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = "";
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString();
                    Realizado.Text = empeño.Empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    Realizado.Text = empeño.Empleado.Usuario;
                    Fecha.Text = empeño.Fecha.ToString("dd/MM/yyyy");
                    lblVence.Text = empeño.FechaVencimiento.ToString("dd/MM/yyyy");

                    funciones.ShowLabels(panelFormulario);
                    funciones.BlockTextBox(panelFormulario, true);
                    funciones.EditTextColor(panelFormulario);
                }
            }
        }

        public async Task CargarPagos()
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                dgvPagos.DataSource = await  _context.Pago.Where(p => p.EmpenoId == empeñoId)
                    .Select(x=>new
                    {
                        ID=x.PagoId,
                        x.Fecha,
                        x.TipoPago,
                        x.Monto,
                    }).ToListAsync();
            }
        }

        private async void dgvEmpeños_DoubleClick(object sender, EventArgs e)
        {
            await BuscarEmpeño();
            await CargarPagos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            funciones.ResetForm(panelFormulario);
            cbInteres.Text = "Porcentaje";
            clienteId = 0;
            empeñoId = 0;
        }

        private async void txtIdentificacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.Identificacion == txtIdentificacion.Text);
                    if (cliente != null)
                    {
                        txtIdentificacion.Text = cliente.Identificacion;
                        txtNombre.Text = cliente.Nombre;
                    }
                }
                funciones.GetPlaceHolders(panelFormulario);
            }            
        }

        private async void btnIdentificacion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                var cliente = await _context.Clientes.SingleOrDefaultAsync(c=>c.Identificacion==txtIdentificacion.Text);
                if (cliente != null)
                {
                    clienteId = cliente.ClienteId;
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;

                    funciones.IntelligHolders(panelFormulario);
                }
            }
        }

        private async void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            await LoadEmpeños();            
         }
    }
}
