using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Funciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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
        EmailFuncion emailFuncion = new EmailFuncion();
        int mesesVencimiento;

        int empeñoId = 0;
        Configuracion configuracion = new Configuracion();

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
            configuracion = _context.Configuraciones.FirstOrDefault();
            mesesVencimiento = configuracion.Meses;
            funciones.ResetForm(panelFormulario);

            Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblVence.Text = DateTime.Today.AddMonths(mesesVencimiento).ToString("dd/MM/yyyy");

            await funciones.ReviewEmpeños();
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
                dgvClientes.DataSource = await _context.Clientes.Where(w => w.Nombre.Contains(txtBuscar.Text) || w.Identificacion.Contains(txtBuscar.Text)).Select(x => new
                {
                    Id = x.ClienteId,
                    x.Identificacion,
                    x.Nombre,
                    x.Correo,
                    x.Telefono,
                }).ToListAsync();

                int number;
                int.TryParse(txtBuscar.Text, out number);
                if (number != 0)
                {
                    dgvEmpeños.DataSource = await _context.Empenos.Where(w => w.EmpenoId == number && !w.IsDelete).Select(x => new
                    {
                        Id = x.EmpenoId,
                        x.Descripcion,
                        Empleado = x.Empleado.Nombre,
                        Es_Oro = x.EsOro,
                        x.Estado,
                        Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        Interes = x.Interes.Porcentaje + "%",
                        x.Monto,
                        Pendiente = x.MontoPendiente
                    }).ToListAsync();

                    var empeño = await _context.Empenos.FindAsync(number);
                    if (empeño != null)
                    {
                        dgvClientes.DataSource = await _context.Clientes.Where(w => w.ClienteId == empeño.ClienteId).Select(x => new
                        {
                            Id = x.ClienteId,
                            x.Identificacion,
                            x.Nombre,
                            x.Correo,
                            x.Telefono,
                        }).ToListAsync();


                        int cantidadMeses = ((int)(DateTime.Today - empeño.Fecha).TotalDays / 30);
                        double sobrante = (DateTime.Today - empeño.Fecha).TotalDays % 30;
                        cantidadMeses += sobrante > 0 ? 1 : 0;
                        int cantidadIntereses = empeño.Intereses.Count();

                        if (cantidadMeses > cantidadIntereses)
                        {
                            var numeroIntereses = cantidadMeses - cantidadIntereses;
                            for (int i = 0; i < numeroIntereses; i++)
                            {
                                var intereses = new Intereses
                                {
                                    EmpenoId = empeño.EmpenoId,
                                    FechaCreacion = DateTime.Now,
                                    Monto = (double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100)
                                };

                                if (empeño.Intereses.Count() > 0)
                                {
                                    intereses.FechaVencimiento = empeño.Intereses.LastOrDefault().FechaVencimiento.AddMonths(1);
                                }
                                else
                                {
                                    intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                                }

                                _context.Intereses.Add(intereses);
                            }
                            await _context.SaveChangesAsync();
                        }

                        var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId).OrderByDescending(o => o.InteresesId)
                  .FirstOrDefaultAsync();

                        if (ultimoInteres.FechaVencimiento < DateTime.Today)
                        {
                            empeño.Estado = Estado.Pendiente;
                            _context.Entry(empeño).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                        if (empeño.FechaVencimiento < DateTime.Today)
                        {
                            if (empeño.Retirado || empeño.FechaRetiro != null)
                            {
                                empeño.Estado = Estado.Retirada;
                            }
                            else if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador != null)
                            {
                                empeño.Estado = Estado.Perdido;
                            }
                            else
                            {
                                empeño.Estado = Estado.Vencido;
                            }

                            await _context.SaveChangesAsync();
                        }

                        await BuscarEmpeño(number);

                        await CargarPagos();
                    }
                }
            }
            lblCatidadEmpeños.Text = dgvEmpeños.RowCount.ToString();
            lblCantidad.Text = dgvClientes.RowCount.ToString();

            if (dgvEmpeños.ColumnCount > 0)
            {
                DataGridViewColumn column = dgvEmpeños.Columns[0];
                column.Width = 40;
            }
            if (dgvClientes.ColumnCount > 0)
            {
                DataGridViewColumn column = dgvClientes.Columns[0];
                column.Width = 40;
            }


        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text != " Buscar" && txtBuscar.Text != "" && txtBuscar.TextLength >= 3)
                {
                    await Buscar();
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                txtBuscar.Text = " Buscar";
                txtBuscar.ForeColor = Color.LightGray;
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
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (cliente != null)
                {
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;
                    clienteId = cliente.ClienteId;
                }
            }
        }

        public Empeno ChangeStatusEmpeño(Empeno empeno)
        {
            if (empeno.FechaVencimiento < DateTime.Today && (empeno.FechaRetiroAdministrador != null || empeno.RetiradoAdministrador)
                && (empeno.FechaRetiro == null || !empeno.Retirado))
            {
                empeno.Estado = Estado.Perdido;
                return empeno;
            }
            if (empeno.FechaVencimiento < DateTime.Today && (empeno.FechaRetiroAdministrador == null || !empeno.RetiradoAdministrador)
                && (empeno.FechaRetiro != null || empeno.Retirado))
            {
                empeno.Estado = Estado.Retirada;
                return empeno;
            }
            if (empeno.FechaVencimiento < DateTime.Today && empeno.FechaRetiroAdministrador == null && !empeno.RetiradoAdministrador
               && empeno.FechaRetiro == null && !empeno.Retirado)
            {
                empeno.Estado = Estado.Vencido;
                return empeno;
            }
            if (empeno.Intereses.Where(i => i.FechaVencimiento < DateTime.Today && i.Monto > i.Pagado).Count() > 0)
            {
                empeno.Estado = Estado.Pendiente;
                return empeno;
            }
            empeno.Estado = Estado.Activo;
            return empeno;
        }

        private async void btnGuardarEmpeño_Click_1(object sender, EventArgs e)
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

            if (empeñoId > 0)
            {
                var empeño = await _context.Empenos.FindAsync(empeñoId);
                if (empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
                {
                    MessageBox.Show("El registro no puede ser modificado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                empeño.Descripcion = txtDescripcion.Text;
                empeño.EditorId = Program.EmpleadoId;
                empeño.EsOro = chbEsOro.Checked;
                var fecha = DateTime.ParseExact(Fecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (empeño.Fecha != fecha)
                {
                    empeño.Fecha = fecha;
                    empeño.FechaVencimiento = DateTime.Today.AddMonths(mesesVencimiento);
                }
                if (empeño.Monto != double.Parse(txtMonto.Text))
                {
                    if (empeño.Monto < double.Parse(txtMonto.Text))
                    {
                        empeño.MontoPendiente += double.Parse(txtMonto.Text) - empeño.Monto;
                    }
                    else if (empeño.Monto > double.Parse(txtMonto.Text))
                    {
                        empeño.MontoPendiente -= empeño.Monto - double.Parse(txtMonto.Text);
                    }

                    if (empeño.Intereses.Count() == 1)
                    {
                        var interes = empeño.Intereses.FirstOrDefault();
                        if (interes.Pagado == 0)
                        {
                            interes.Monto = empeño.Monto * ((double)empeño.Interes.Porcentaje / (double)100);
                        }
                    }
                }
                if (empeño.InteresId != await funciones.GetInteresIdByNombre(cbInteres.Text))
                {
                    empeño.InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text);

                    if (empeño.Intereses.Count() == 1)
                    {
                        var interes = empeño.Intereses.FirstOrDefault();
                        if (interes.Pagado == 0)
                        {
                            interes.InteresesId = empeño.InteresId;
                            interes.Monto = empeño.Monto * ((double)empeño.Interes.Porcentaje / (double)100);
                        }
                    }
                }

                _context.Entry(empeño).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                ChangeState(lblEstado, Estado.Activo);

                dgvPagos.DataSource = _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).Select(x => new
                {
                    Id = x.InteresesId,
                    Interes = x.Empeno.Interes.Porcentaje + "%",
                    Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                    x.Monto,
                    x.Pagado,
                    Faltan = DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                }).ToList();
            }
            else
            {
                var empleadoId = Program.EmpleadoId;

                var empeño = new Empeno
                {
                    ClienteId = clienteId,
                    Descripcion = txtDescripcion.Text,
                    EmpleadoId = empleadoId == 0 ? 1 : empleadoId,
                    EditorId = empleadoId == 0 ? 1 : empleadoId,
                    EsOro = chbEsOro.Checked,
                    Estado = Estado.Activo,
                    Fecha = DateTime.Now,
                    FechaVencimiento = DateTime.Today.AddMonths(mesesVencimiento),
                    InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text),
                    Monto = double.Parse(txtMonto.Text),
                    MontoPendiente = double.Parse(txtMonto.Text),
                    Comentario = txtComentario.Text
                };

                var interes = await _context.Interes.FindAsync(empeño.InteresId);

                _context.Empenos.Add(empeño);
                await _context.SaveChangesAsync();
                lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                ChangeState(lblEstado, Estado.Activo);
                empeñoId = empeño.EmpenoId;
                var intereses = new Intereses
                {
                    EmpenoId = empeño.EmpenoId,
                    FechaCreacion = DateTime.Now,
                    FechaVencimiento = DateTime.Today.AddMonths(1),
                    Monto = (double)empeño.MontoPendiente * ((double)interes.Porcentaje / (double)100)
                };

                _context.Intereses.Add(intereses);
                await _context.SaveChangesAsync();

                dgvPagos.DataSource = _context.Intereses.Where(i => i.EmpenoId == intereses.EmpenoId).Select(x => new
                {
                    Id = x.InteresesId,
                    Interes = x.Empeno.Interes.Porcentaje + "%",
                    Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                    x.Monto,
                    x.Pagado,
                    Faltan = DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                }).ToList();

                Print(empeño);
               
                await emailFuncion.SendMail(empeño.Cliente.Correo, "Empeño", "Se a generado un empeño en ");
            }

            await _context.SaveChangesAsync();

            dgvEmpeños.DataSource = await _context.Empenos.Where(x => x.EmpenoId == empeñoId && !x.IsDelete).Select(x => new
            {
                Id = x.EmpenoId,
                x.Descripcion,
                Empleado = x.Empleado.Nombre,
                Es_Oro = x.EsOro,
                x.Estado,
                Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                Interes = x.Interes.Porcentaje + "%",
                x.Monto,
                Pendiente = x.MontoPendiente
            }).ToListAsync();

            dgvClientes.DataSource = _context.Clientes.Where(w => w.ClienteId == clienteId).Select(x => new
            {
                Id = x.ClienteId,
                x.Identificacion,
                x.Nombre,
                x.Correo,
                x.Telefono,
            }).ToList();

            MessageBox.Show("Datos guardados correctamente");
        }

        private async void btnEditarEmpeño_Click(object sender, EventArgs e)
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
                }
            }
        }

        private async void btnEmpeñar_Click_2(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                funciones.ResetForm(panelFormulario);
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (cliente != null)
                {
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;
                }
            }

            funciones.IntelligHolders(panelFormulario);
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


        public async Task LoadEmpeños(int index=0)
        {
            IQueryable<Empeno> empeños;

            empeños = _context.Empenos;

            switch (index)
            {
                case 0:
                    empeños = empeños.Where(w => !w.IsDelete && (w.Estado == Estado.Activo || w.Estado == Estado.Pendiente || w.Estado == Estado.Vencido));
                    break;
                case 1:
                    empeños = empeños.Where(w => w.IsDelete);
                    break;
                case 2:
                    empeños = empeños.Where(w => !w.IsDelete && (w.Estado == Estado.Retirada || w.Retirado || w.FechaRetiro != null));
                    break;
                case 3:
                    empeños = empeños.Where(w => !w.IsDelete && (w.RetiradoAdministrador || w.FechaRetiroAdministrador != null));
                    break;
                default:
                    break;
            }


            int number;
            int.TryParse(txtBuscar.Text, out number);        

            if (dgvClientes.SelectedRows.Count > 0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                empeños = empeños.Where(w => w.ClienteId == cliente.ClienteId);
            }
            else if (number != 0)
            {
                empeños = empeños.Where(w => w.EmpenoId == number);
            }

            dgvEmpeños.DataSource = empeños.Select(x => new
            {
                Id = x.EmpenoId,
                x.Descripcion,
                Empleado = x.Empleado.Nombre,
                Es_Oro = x.EsOro,
                x.Estado,
                Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                Interes = x.Interes.Porcentaje + "%",
                x.Monto,
                Pendiente = x.MontoPendiente
            }).ToList();

            DataGridViewColumn column = dgvEmpeños.Columns[0];
            column.Width = 40;
            lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
            funciones.BlockTextBox(panelFormulario, false);
        }

        private async void btnVerEmpleado_Click(object sender, EventArgs e)
        {
            await LoadEmpeños();
        }

        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    await Buscar();
                    await CargarPagos();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int i;
                    int.TryParse(txtBuscar.Text, out i);
                    if (i > 0)
                    {
                        i -= 1;
                        txtBuscar.Text = i.ToString();
                    }
                    await Buscar();
                    await CargarPagos();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    int i;
                    int.TryParse(txtBuscar.Text, out i);
                    if (i > 0)
                    {
                        i += 1;
                        txtBuscar.Text = i.ToString();
                    }
                    await Buscar();
                    await CargarPagos();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<int> GetInteresId(string nombre)
        {
            var interes = await _context.Interes.SingleOrDefaultAsync(x => x.Nombre == nombre);
            if (interes != null)
            {
                return interes.InteresId;
            }
            return 0;
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
            var _context = new DataContext();
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);

                if (empeño != null)
                {
                    if (_context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).Count() > 0)
                    {
                        var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId)
                                    .OrderByDescending(o => o.InteresesId)
                                    .FirstOrDefaultAsync();
                        if (ultimoInteres != null)
                        {
                            if (ultimoInteres.FechaVencimiento < DateTime.Today)
                            {
                                empeño.Estado = Estado.Pendiente;
                                _context.Entry(empeño).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                        }

                    }
                    if (empeño.FechaVencimiento < DateTime.Today)
                    {
                        if (empeño.Retirado || empeño.FechaRetiro != null)
                        {
                            empeño.Estado = Estado.Retirada;
                        }
                        else if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador != null)
                        {
                            empeño.Estado = Estado.Perdido;
                        }
                        else
                        {
                            empeño.Estado = Estado.Vencido;
                        }

                        await _context.SaveChangesAsync();
                    }
                    empeñoId = empeño.EmpenoId;
                    lblNumeroEmpeño.Text = empeñoId.ToString();
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
                    ChangeState(lblEstado, empeño.Estado);
                    Fecha.Text = empeño.Fecha.ToString("dd/MM/yyyy");

                    funciones.ShowLabels(panelFormulario);
                    if (empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
                    {
                        funciones.BlockTextBox(panelFormulario, false);
                    }
                    else
                    {
                        funciones.BlockTextBox(panelFormulario, true);
                        funciones.EditTextColor(panelFormulario);
                    }
                }
            }
        }

        public void ChangeState(Label label, Estado estado)
        {
            label.Visible = false;
            switch (estado)
            {
                case Estado.Activo:
                    label.Text = "Activo";
                    label.BackColor = Color.Blue;
                    label.ForeColor = Color.White;
                    break;
                case Estado.Pendiente:
                    label.Text = "Pendiente";
                    label.BackColor = Color.OrangeRed;
                    label.ForeColor = Color.White;
                    break;
                case Estado.Vencido:
                    label.Text = "Vencido";
                    label.BackColor = Color.Red;
                    label.ForeColor = Color.White;
                    break;
                case Estado.Cancelada:
                    label.Text = "Pendiente";
                    label.BackColor = Color.Yellow;
                    label.ForeColor = Color.Black;
                    break;
                case Estado.Retirada:
                    label.Text = "Retirada";
                    label.BackColor = Color.ForestGreen;
                    label.ForeColor = Color.White;
                    break;
                default:
                    label.Text = "Activo";
                    label.BackColor = Color.Blue;
                    label.ForeColor = Color.White;
                    break;
            }
            label.Visible = true;
        }

        public async Task BuscarEmpeño(int id)
        {
            var _context = new DataContext();
            if (id > 0)
            {
                var empeño = await _context.Empenos.FindAsync(id);
                var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId).OrderByDescending(o => o.InteresesId)
                  .FirstOrDefaultAsync();
                if (ultimoInteres.FechaVencimiento < DateTime.Today)
                {
                    empeño.Estado = Estado.Pendiente;
                    _context.Entry(empeño).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                if (empeño.FechaVencimiento < DateTime.Today)
                {
                    if (empeño.Retirado || empeño.FechaRetiro != null)
                    {
                        empeño.Estado = Estado.Retirada;
                    }
                    else if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador != null)
                    {
                        empeño.Estado = Estado.Perdido;
                    }
                    else
                    {
                        empeño.Estado = Estado.Vencido;
                    }

                    await _context.SaveChangesAsync();
                }


                var empleado = await _context.Empleados.FindAsync(empeño.EmpleadoId);
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
                    Realizado.Text = empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    Realizado.Text = empeño.Empleado.Usuario;
                    Fecha.Text = empeño.Fecha.ToString("dd/MM/yyyy");
                    lblVence.Text = empeño.FechaVencimiento.ToString("dd/MM/yyyy");
                    lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                    ChangeState(lblEstado, empeño.Estado);
                    funciones.ShowLabels(panelFormulario);
                    if (empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
                    {
                        funciones.BlockTextBox(panelFormulario, false);
                    }
                    else
                    {
                        funciones.BlockTextBox(panelFormulario, true);
                        funciones.EditTextColor(panelFormulario);
                    }
                }
            }
        }

        public async Task CargarPagos()
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                empeñoId = int.Parse(dgvEmpeños.SelectedRows[0].Cells[0].Value.ToString());
                dgvPagos.DataSource = await _context.Intereses.Where(p => p.EmpenoId == empeñoId)
                    .Select(x => new
                    {
                        Id = x.InteresesId,
                        Interes = x.Empeno.Interes.Porcentaje + "%",
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        x.Monto,
                        x.Pagado,
                        Vencimiento = DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                    }).OrderByDescending(i => i.Id).ToListAsync();
            }
        }

        private async void dgvEmpeños_DoubleClick(object sender, EventArgs e)
        {
            await BuscarEmpeño();
            await CargarPagos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CleanForm();
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
                        clienteId = cliente.ClienteId;
                        txtIdentificacion.Text = cliente.Identificacion;
                        txtNombre.Text = cliente.Nombre;
                        txtNombre.ForeColor = Color.Black;
                        lblNombre.Visible = true;
                        txtDescripcion.Focus();
                    }
                }
            }
        }

        private async void btnIdentificacion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.Identificacion == txtIdentificacion.Text);
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

            if (dgvClientes.SelectedRows.Count > 0)
            {
                CleanForm();

                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                if (cliente != null)
                {
                    clienteId = cliente.ClienteId;
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtNombre.Text = cliente.Nombre;
                }
            }

            funciones.IntelligHolders(panelFormulario);
        }

        public void CleanForm()
        {
            funciones.ResetForm(panelFormulario);
            cbInteres.Text = "Porcentaje";
            clienteId = 0;
            empeñoId = 0;
            lblNumeroEmpeño.Text = "";
            dgvPagos.DataSource = null;
            dgvPagos.Refresh();
            lblEstado.Visible = false;
            Fecha.Text = DateTime.Today.ToString();
            lblNumeroEmpeño.Text = _context.Empenos.Max(m => m.EmpenoId).ToString();
            lblCantidad.Text = dgvClientes.Rows.Count.ToString();
            lblCatidadEmpeños.Text = dgvClientes.Rows.Count.ToString();
        }

        private async void iconButton10_Click(object sender, EventArgs e)
        {
            var empeño = await _context.Empenos.FindAsync(empeñoId);
            if (empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
            {
                MessageBox.Show("El registro no puede ser modificado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var oscuro = new frmOscuro();
            oscuro.Show();
            var frm = new frmPagar(empeñoId);
            frm.ShowDialog();
            oscuro.Close();
            await BuscarEmpeño(empeñoId);
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMonto.Text) && txtMonto.Text != "Monto")
            {
                var monto = double.Parse(txtMonto.Text);
                var interes = _context.Interes.Where(i => i.Mayor < monto).FirstOrDefault();
                if (interes != null)
                {
                    cbInteres.Text = interes.Nombre;
                }
            }
        }

        private void btnClienteNuevo_Click(object sender, EventArgs e)
        {
            var oscuro = new frmOscuro();
            oscuro.Show();
            var frm = new frmClientes();
            frm.ShowDialog();
            oscuro.Close();
            if (Program.Cliente != null)
            {
                Cliente cliente = Program.Cliente;
                clienteId = cliente.ClienteId;
                txtIdentificacion.Text = cliente.Identificacion;
                txtNombre.Text = cliente.Nombre;
                funciones.IntelligHolders(panelFormulario);
                chbEsOro.Checked = true;
                txtDescripcion.Focus();
            }
        }

        private void cbInteres_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(cbInteres, lblInteres, PlaceHolderType.Leave, "Porcentaje");
        }

        private void cbInteres_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(cbInteres, lblInteres, PlaceHolderType.Enter, "Porcentaje");
        }

        private async void btnHoy_Click(object sender, EventArgs e)
        {
            CleanForm();
            dgvEmpeños.DataSource = await _context.Empenos.Where(x => !x.IsDelete && (x.Retirado == false && x.FechaRetiro == null
              && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
              && (x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)
              && (x.Intereses.Where(i => i.FechaVencimiento == DateTime.Today).Count() > 0)).Select(x => new
              {
                  Id = x.EmpenoId,
                  x.Descripcion,
                  Empleado = x.Empleado.Nombre,
                  Es_Oro = x.EsOro,
                  x.Estado,
                  Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                  Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                  Interes = x.Interes.Porcentaje + "%",
                  x.Monto,
                  Pendiente = x.MontoPendiente
              }).ToListAsync();
            lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
        }

        private async void btnPendientes_Click(object sender, EventArgs e)
        {
            CleanForm();
            dgvEmpeños.DataSource = await _context.Empenos.Where(x => !x.IsDelete && (x.Retirado == false && x.FechaRetiro == null
             && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
             && (x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)).Select(x => new
             {
                 Id = x.EmpenoId,
                 x.Descripcion,
                 Empleado = x.Empleado.Nombre,
                 Es_Oro = x.EsOro,
                 x.Estado,
                 Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                 Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                 Interes = x.Interes.Porcentaje + "%",
                 x.Monto,
                 Pendiente = x.MontoPendiente
             }).ToListAsync();
            dgvEmpeños.ClearSelection();
            lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
        }

        private void dgvEmpeños_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewRow row in dgvEmpeños.Rows)
            {
                string rowtype = row.Cells["Estado"].Value.ToString();

                switch (rowtype)
                {
                    case "Vencido":
                        row.Cells[4].Style.BackColor = Color.Red;
                        row.Cells[4].Style.ForeColor = Color.White;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                    case "Pendiente":
                        row.Cells[4].Style.BackColor = Color.Orange;
                        row.Cells[4].Style.ForeColor = Color.White;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                    case "Activo":
                        row.Cells[4].Style.BackColor = Color.Blue;
                        row.Cells[4].Style.ForeColor = Color.White;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                    case "Cancelada":
                        row.Cells[4].Style.BackColor = Color.Yellow;
                        row.Cells[4].Style.ForeColor = Color.Black;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                    case "Retirada":
                        row.Cells[4].Style.BackColor = Color.ForestGreen;
                        row.Cells[4].Style.ForeColor = Color.White;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                    default:
                        row.Cells[4].Style.BackColor = Color.Blue;
                        row.Cells[4].Style.ForeColor = Color.White;
                        row.Cells[4].Style.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
                        break;
                }
            }
        }

        #region Funciones
        public async void Print(Empeno empeno)
        {
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();

            cexcel.Workbooks.Open("C:\\Empeños\\Comprobantes\\ComprobanteEmpeño.xlsx", true, true);

            cexcel.Visible = false;

            cexcel.Cells[9, 2].value = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            cexcel.Cells[10, 2].value = Program.Usuario.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (chbEsOro.Checked)
            {
                cexcel.Cells[19, 1].value = txtDescripcion.Text;
            }
            else
            {
                cexcel.Cells[19, 1].value = txtDescripcion.Text;
            }
            cexcel.Cells[22, 3].value = empeno.Monto.ToString().Replace(",", "");
            cexcel.Cells[23, 3].value = empeno.Monto.ToString().Replace(",", "");
            string FechaVence = lblVence.Text;
            //cexcel.Cells[24, 3].value = Convert.ToDateTime(FechaVence).Day.ToString() + "/" + Convert.ToDateTime(FechaVence).Month.ToString() + "/" + Convert.ToDateTime(FechaVence).Year.ToString();
            //cexcel.Cells[26, 3].value = Convert.ToInt32(objinteres.Monto_Interes).ToString();
            cexcel.Cells[28, 3].value = "Pendiente";
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
        #endregion

        private async void btnReimprimir_Click(object sender, EventArgs e)
        {
            if (dgvEmpeños.SelectedRows.Count>0)
            {
                var empeñoId = dgvEmpeños.SelectedRows[0].Cells[0].Value;

                var empeño = await _context.Empenos.FindAsync(empeñoId);

                Print(empeño);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
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

            var resp=MessageBox.Show("Esta seguro que desea eliminar los datos", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp== DialogResult.Yes)
            {
                if (dgvEmpeños.SelectedRows.Count>0)
                {
                    var empeñoId = dgvEmpeños.SelectedRows[0].Cells[0].Value;
                    var empeño = await _context.Empenos.FindAsync(empeñoId);
                    empeño.EditorId = Program.EmpleadoId;
                    empeño.IsDelete = true;
                    _context.Entry(empeño).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            var oscuro = new frmOscuro();
            oscuro.Show();
            var frm = new frmClientes();
            frm.ShowDialog();
            oscuro.Close();
            if (Program.Cliente != null)
            {
                Cliente cliente = Program.Cliente;
                clienteId = cliente.ClienteId;
                txtIdentificacion.Text = cliente.Identificacion;
                txtNombre.Text = cliente.Nombre;
                funciones.IntelligHolders(panelFormulario);
                chbEsOro.Checked = true;
                txtDescripcion.Focus();
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadEmpeños(comboBox1.SelectedIndex);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            frmOscuro oscuro = new frmOscuro();
            oscuro.Show();
            int interesId = int.Parse(dgvPagos.SelectedRows[0].Cells[0].Value.ToString());
            frmEmpeñoInteres frmEmpeñoInteres = new frmEmpeñoInteres(interesId);
            frmEmpeñoInteres.Show();
        }
    }
}
