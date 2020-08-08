using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Funciones;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        bool switchPago = false; 
        int empeñoId = 0;
        Configuracion configuracion = new Configuracion();

        public frmEmpeno()
        {
            InitializeComponent();
            clientesInicial.Add(new Cliente());
            empenosInicial.Add(new Empeno());
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
            configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            mesesVencimiento = configuracion.Meses;
            funciones.ResetForm(panelFormulario);
            switchPago = false;
            chbEsOro.Checked = true;
            Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblVence.Text = DateTime.Today.AddMonths(mesesVencimiento).ToString("dd/MM/yyyy");
            if (!_context.Empenos.Any())
            {
                lblNumeroEmpeño.Text = "01";
            }
            else
            {
                lblNumeroEmpeño.Text = (_context.Empenos.Max(l => l.EmpenoId) + 1).ToString();
            }

            if (Program.Usuario.PerfilId==4)
            {
                cbInteres.Enabled = false;
                lblVence.Enabled = false;
                Fecha.Enabled = false;
            }
            else 
            {
                cbInteres.Enabled = true;
                lblVence.Enabled = true;
                Fecha.Enabled = true;
                Fecha.ForeColor = Color.Black;
            }
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
                dgvClientes.DataSource = await _context.Clientes.Where(w =>!w.IsDelete && (w.Nombre.Contains(txtBuscar.Text) || w.Identificacion.Contains(txtBuscar.Text))).Select(x => new
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
                    await funciones.ReviewDuplicateEmpeños(number);
                    var listEmpeños = await _context.Empenos.Where(w => w.EmpenoId == number && !w.IsDelete && !w.RetiradoAdministrador).Select(x => new
                    {
                        Id = x.EmpenoId,
                        x.Descripcion,
                        Empleado = x.Empleado.Nombre,
                        Es_Oro = x.EsOro,
                        x.Estado,
                        Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        Interes = x.Interes.Porcentaje + "%",
                        Monto = x.Monto,
                        Pendiente = x.MontoPendiente
                    }).ToListAsync();

                    dgvEmpeños.DataSource =  listEmpeños.AsEnumerable().Select(x => new
                    {
                        x.Id,
                        x.Descripcion,
                        x.Empleado,
                        x.Es_Oro,
                        x.Estado,
                        x.Fecha,
                        x.Vence,
                        x.Interes,
                        Monto = x.Monto.ToString("N2"),
                        Pendiente = x.Pendiente.ToString("N2")
                    }).ToList();

                    var empeño = await _context.Empenos.FindAsync(number);
                    if (empeño != null)
                    {
                        dgvClientes.DataSource = await _context.Clientes.Where(w => !w.IsDelete && w.ClienteId == empeño.ClienteId).Select(x => new
                        {
                            Id = x.ClienteId,
                            x.Identificacion,
                            x.Nombre,
                            x.Correo,
                            x.Telefono,
                        }).ToListAsync();


                        int cantidadMeses = ((int)(DateTime.Today - empeño.Fecha).TotalDays / 30);
                        double sobrante = (DateTime.Today - empeño.Fecha).TotalDays % 30;
                        cantidadMeses += sobrante > 1 ? 1 : 0;
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
                                if (_context.Intereses.Where(x=>x.EmpenoId==empeño.EmpenoId && x.FechaVencimiento==intereses.FechaVencimiento).Count()==0)
                                {
                                    _context.Intereses.Add(intereses);
                                }                                
                            }
                            await _context.SaveChangesAsync();
                        }

                        var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId).OrderByDescending(o => o.InteresesId)
                  .FirstOrDefaultAsync();

                        if (ultimoInteres!=null)
                        {
                            if (ultimoInteres.FechaVencimiento < DateTime.Today)
                            {
                                empeño.Estado = Estado.Pendiente;
                                _context.Entry(empeño).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
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

                        await BuscarEmpeño(number);
                        if (switchPago)
                        {
                            await LoadPays();
                        }
                        else
                        {
                            CargarPagos();
                        }
                        
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
            try
            {
               
                if (empeñoId > 0)
                {
                    if (!funciones.ValidatePIN("Editar Empeño"))
                        return;

                    var empeño = await _context.Empenos.FindAsync(empeñoId);
                    if (empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
                    {
                        MessageBox.Show("El registro no puede ser modificado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    empeño.Descripcion = txtDescripcion.Text;
                    empeño.EditorId = Program.EmpleadoId;
                    empeño.EsOro = chbEsOro.Checked;
                    empeño.Comentario = txtComentario.Text != lblComentario.Text ? txtComentario.Text : string.Empty;
                    var fecha = DateTime.ParseExact(Fecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (empeño.Fecha != fecha  && Program.PerfilId!=4)
                    {
                        empeño.Fecha = fecha;
                        if(empeño.Pagos.Count()==0)
                            empeño.FechaVencimiento = DateTime.Today.AddMonths(mesesVencimiento);
                    }

                    if (empeño.Monto != double.Parse(txtMonto.Text))
                    {
                        empeño.Monto = double.Parse(txtMonto.Text);

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
                                _context.Entry(interes).State = EntityState.Modified;
                            }
                        }
                    }

                    if (empeño.InteresId != await funciones.GetInteresIdByNombre(cbInteres.Text)  && Program.PerfilId != 4)
                    {
                        empeño.InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text);

                        if (empeño.Intereses.Count() == 1)
                        {
                            var interes = empeño.Intereses.FirstOrDefault();
                            if (interes.Pagado == 0)
                            {
                                interes.Monto = empeño.Monto * ((double)empeño.Interes.Porcentaje / (double)100);
                                _context.Entry(interes).State = EntityState.Modified;
                            }
                        }
                    }

                    var vence = DateTime.ParseExact(lblVence.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (empeño.FechaVencimiento != vence && Program.PerfilId != 4)
                    {
                        empeño.FechaVencimiento = vence;
                    }
                    _context.Entry(empeño).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Datos guardados correctamente");

                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Valor = JsonConvert.SerializeObject(empeño),
                        Modulo = "Empeños",
                        Accion = "Editar"
                    });

                    lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                    ChangeState(lblEstado, Estado.Activo, empeño);

                    dgvPagos.DataSource = _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).Select(x => new
                    {
                        Id = x.InteresesId,
                        Interes = x.Empeno.Interes.Porcentaje + "%",
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        x.Monto,
                        x.Pagado,
                        Vencimiento = x.Monto == x.Pagado ? 0 : DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                    }).OrderByDescending(i => i.Id)
                        .AsEnumerable()
                        .Select(x => new
                        {
                            x.Id,
                            x.Interes,
                            x.Vence,
                            Monto = x.Monto.ToString("N2"),
                            Pagado = x.Pagado.ToString("N2"),
                            Faltan = x.Vencimiento
                        }).ToList();
                }
                else
                {
                    if (!funciones.ValidatePIN("Empeño"))
                        return;

                    var empleadoId = Program.EmpleadoId;
                    var empleado = await _context.Empleados.FindAsync(empleadoId);
                    var strFecha = Fecha.Text + " " + DateTime.Now.ToString("HH:mm");
                    var fecha = DateTime.ParseExact(strFecha, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    var fechaVencimiento = DateTime.ParseExact(lblVence.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var vence = fecha.Date.AddMonths(mesesVencimiento);

                    if (fecha.Date!=DateTime.Today)
                    {
                        if (Program.PerfilId==4)
                        {
                            MessageBox.Show("Solo un supervisor puede realizar modificaciones en la fecha.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        var resp = MessageBox.Show("La fecha en la que desea crear el Empeño es diferente a la fecha actual." + Environment.NewLine
                            + "¿Esta seguro que desea guardar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resp==DialogResult.No)
                        {
                            MessageBox.Show("Realice los cambios necesarios", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    if (fechaVencimiento!=vence)
                    {
                        if (Program.PerfilId == 4)
                        {
                            MessageBox.Show("Solo un supervisor puede realizar modificaciones en la fecha", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        var resp = MessageBox.Show("La fecha de vencimiento del Empeño es diferente a la calculada por el sistema." + Environment.NewLine
                            + "¿Esta seguro que desea guardar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resp == DialogResult.No)
                        {
                            MessageBox.Show("Realice los cambios necesarios.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    var empeño = new Empeno
                    {
                        ClienteId = clienteId,
                        Descripcion = txtDescripcion.Text,
                        EmpleadoId = empleadoId == 0 ? 1 : empleadoId,
                        EditorId = empleadoId == 0 ? 1 : empleadoId,
                        EsOro = chbEsOro.Checked,
                        Estado = Estado.Activo,
                        Fecha = fecha,
                        FechaVencimiento = fechaVencimiento,
                        InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text),
                        Monto = double.Parse(txtMonto.Text),
                        MontoPendiente = double.Parse(txtMonto.Text),
                        Comentario = txtComentario.Text != lblComentario.Text ? txtComentario.Text : string.Empty
                    };                    

                    _context.Empenos.Add(empeño);
                    await _context.SaveChangesAsync();
                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Valor = JsonConvert.SerializeObject(empeño),
                        Modulo = "Empeños",
                        Accion = "Crear"
                    });

                    var interes = await _context.Interes.FindAsync(empeño.InteresId);
                    
                    lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                    ChangeState(lblEstado, Estado.Activo, empeño);
                    empeñoId = empeño.EmpenoId;
                    var intereses = new Intereses
                    {
                        EmpenoId = empeño.EmpenoId,
                        FechaCreacion = DateTime.Now,
                        FechaVencimiento = fecha.AddMonths(1),
                        Monto = (double)empeño.MontoPendiente * ((double)interes.Porcentaje / (double)100)
                    };

                    _context.Intereses.Add(intereses);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Datos guardados correctamente");
                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Valor = JsonConvert.SerializeObject(empeño),
                        Modulo = "Intereses",
                        Accion = "Crear"
                    });

                    dgvPagos.DataSource = _context.Intereses.Where(i => i.EmpenoId == intereses.EmpenoId).Select(x => new
                    {
                        Id = x.InteresesId,
                        Interes = x.Empeno.Interes.Porcentaje + "%",
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        x.Monto,
                        x.Pagado,
                        Vencimiento = x.Monto == x.Pagado ? 0 : DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                    }).OrderByDescending(i => i.Id)
                        .AsEnumerable()
                        .Select(x => new
                        {
                            x.Id,
                            x.Interes,
                            x.Vence,
                            Monto = x.Monto.ToString("N2"),
                            Pagado = x.Pagado.ToString("N2"),
                            Faltan = x.Vencimiento
                        }).ToList();

                    await Print(empeño);
                    var cliente = _context.Clientes.Find(empeño.ClienteId);

                    if (!string.IsNullOrEmpty(cliente.Correo))
                        await emailFuncion.SendMail(empeño.Cliente.Correo, "Creación de Empeño " + configuracion.Compañia + " #" + empeño.EmpenoId, empeño);
                }

                await BuscarEmpeño(empeñoId);

                lblCantidad.Text = dgvClientes.Rows.Count.ToString();
                lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups! algo salio mal" + Environment.NewLine + "Error: " + ex.Message.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                    txtComentario.Text = empeño.Comentario;
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString("N2");
                    Realizado.Text = empeño.Empleado.Usuario;
                }
            }
        }

        private async void btnEmpeñar_Click_2(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                funciones.ResetForm(panelFormulario);
                Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
                if (Program.Usuario.PerfilId == 4)
                {
                    if (!funciones.ValidatePIN("Editar Empeño"))
                        return;
                }

                var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                var empleado = await _context.Empleados.FindAsync(empeño.EmpleadoId);
                if (empeño != null)
                {
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = empeño.Comentario;
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString("N2");
                    Realizado.Text = empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    if (switchPago)
                    {
                        await LoadPays();
                    }
                    else
                    {
                        CargarPagos();
                    }
                    if (empeño.Estado == Estado.Retirada)
                    {
                        funciones.BlockTextBox(panelFormulario, false);
                    }
                    else
                    {
                        funciones.BlockTextBox(panelFormulario, true);
                    }

                }
            }
        }


        public async Task LoadEmpeños(int index=0)
        {
            try
            {
                IQueryable<Empeno> empeños;

                empeños = _context.Empenos;

                switch (index)
                {
                    case 0:
                        empeños = empeños.Where(w => !w.IsDelete && !w.RetiradoAdministrador && (w.Estado == Estado.Activo || w.Estado == Estado.Pendiente || w.Estado == Estado.Vencido));
                        break;
                    case 1:
                        empeños = empeños.Where(w => w.IsDelete && !w.RetiradoAdministrador);
                        break;
                    case 2:
                        empeños = empeños.Where(w => !w.IsDelete && !w.RetiradoAdministrador && (w.Estado == Estado.Retirada || w.Retirado || w.FechaRetiro != null));
                        break;
                    case 3:
                        empeños = empeños.Where(w => !w.IsDelete && !w.RetiradoAdministrador && (w.RetiradoAdministrador || w.FechaRetiroAdministrador != null));
                        break;
                    default:
                        break;
                }


                int number;                
                if (int.TryParse(txtBuscar.Text, out number))
                {

                    if (dgvClientes.SelectedRows.Count > 0)
                    {
                        var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                        empeños = empeños.Where(w => w.ClienteId == cliente.ClienteId);
                    }
                    else if (number != 0)
                    {
                        empeños = empeños.Where(w => w.EmpenoId == number);
                    }


                    var listEmpeños = await empeños.Select(x => new
                    {
                        Id = x.EmpenoId,
                        x.Descripcion,
                        Empleado = x.Empleado.Nombre,
                        Es_Oro = x.EsOro,
                        x.Estado,
                        Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                        Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                        Interes = x.Interes.Porcentaje + "%",
                        Monto = x.Monto,
                        Pendiente = x.MontoPendiente
                    }).ToListAsync();

                    dgvEmpeños.DataSource = listEmpeños.AsEnumerable().Select(x => new
                    {
                        x.Id,
                        x.Descripcion,
                        x.Empleado,
                        x.Es_Oro,
                        x.Estado,
                        x.Fecha,
                        x.Vence,
                        x.Interes,
                        Monto = x.Monto.ToString("N2"),
                        Pendiente = x.Pendiente.ToString("N2")
                    }).ToList();

                    DataGridViewColumn column = dgvEmpeños.Columns[0];
                    column.Width = 40;
                    lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString(); 
                }
            }
            catch (Exception)
            {

            }
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
                    if (switchPago)
                    {
                        await LoadPays();
                    }
                    else
                    {
                        CargarPagos();
                    }
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
                    if (switchPago)
                    {
                        await LoadPays();
                    }
                    else
                    {
                        CargarPagos();
                    }
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
                    if (switchPago)
                    {
                        await LoadPays();
                    }
                    else
                    {
                        CargarPagos();
                    }
                }
                if (empeñoId>0)
                {
                    var empeño = await _context.Empenos.FindAsync(empeñoId);
                    if (empeño.Estado==Estado.Retirada)
                    {
                        funciones.BlockTextBox(panelFormulario, false);
                    }
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



        private async void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIdentificacion, lblIdentificacion, PlaceHolderType.Leave, "Identificación");
            if (txtIdentificacion.Text!=lblIdentificacion.Text && lblIdentificacion.Visible)
            {
                await FindIdentification();
                if (Program.Usuario.PerfilId == 4)
                {
                    cbInteres.Enabled = false;
                    lblVence.Enabled = false;
                    Fecha.Enabled = false;
                }
                else
                {
                    cbInteres.Enabled = true;
                    lblVence.Enabled = true;
                    Fecha.Enabled = true;
                    Fecha.ForeColor = Color.Black;
                }
            }            
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
            funciones.FormatNumber(sender);
            if (txtMonto.Text=="0.00")
            {
                txtMonto.Text = string.Empty;
                funciones.PlaceHolder(txtMonto, lblMonto, PlaceHolderType.Leave, "Monto");
            }            
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
            try
            {
                var _context = new DataContext();
                if (dgvEmpeños.SelectedRows.Count > 0)
                {
                    int empeñoId = int.Parse(dgvEmpeños.SelectedRows[0].Cells[0].Value.ToString());
                    var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                    await funciones.ReviewDuplicateEmpeños(empeño.EmpenoId);
                    if (empeño != null)
                    {
                        int cantidadMeses = ((int)(DateTime.Today - empeño.Fecha).TotalDays / 30);
                        double sobrante = (DateTime.Today - empeño.Fecha).TotalDays % 30;
                        cantidadMeses += sobrante > 1 ? 1 : 0;
                        int cantidadIntereses = empeño.Intereses.Count();

                        if (cantidadMeses > cantidadIntereses)
                        {
                            var numeroIntereses = cantidadMeses - cantidadIntereses;
                            for (int i = 0; i < numeroIntereses; i++)
                            {
                                var intereses = new Intereses
                                {
                                    EmpenoId = empeñoId,
                                    FechaCreacion = DateTime.Now,
                                    Monto = (double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100)
                                };

                                if (_context.Intereses.Where(x => x.EmpenoId == empeñoId).Count() > 0)
                                {
                                    intereses.FechaVencimiento = _context.Intereses
                                        .Where(x => x.EmpenoId == empeñoId)
                                        .OrderByDescending(x => x.InteresesId)
                                        .FirstOrDefault()
                                        .FechaVencimiento.AddMonths(1);
                                }
                                else
                                {
                                    intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                                }
                                using (DataContext temp = new DataContext())
                                {
                                    var interesesFind = await temp.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId && x.FechaVencimiento == intereses.FechaVencimiento).ToListAsync();
                                    if (interesesFind.Count() == 0)
                                    {
                                        _context.Intereses.Add(intereses);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                            await funciones.SaveBitacora(new ValorBitacora
                            {
                                Valor = JsonConvert.SerializeObject(_context.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId).ToList()),
                                Modulo = "Intereses",
                                Accion = "Crear"
                            });
                        }
                        if (_context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).Count() > 0)
                        {
                            var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId)
                                        .OrderByDescending(o => o.InteresesId)
                                        .FirstOrDefaultAsync();
                            if (ultimoInteres != null)
                            {
                                if (ultimoInteres.FechaVencimiento < DateTime.Today && ultimoInteres.Monto > ultimoInteres.Pagado)
                                {
                                    empeño.Estado = Estado.Pendiente;
                                    _context.Entry(empeño).State = EntityState.Modified;
                                    await _context.SaveChangesAsync();
                                    await funciones.SaveBitacora(new ValorBitacora
                                    {
                                        Valor = JsonConvert.SerializeObject(empeño),
                                        Modulo = "Empeños",
                                        Accion = "Editar"
                                    });
                                }
                            }

                        }
                        else if (_context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId && i.Monto > i.Pagado && i.FechaVencimiento < DateTime.Today).Count() > 0)
                        {
                            empeño.Estado = Estado.Pendiente;
                            _context.Entry(empeño).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            await funciones.SaveBitacora(new ValorBitacora
                            {
                                Valor = JsonConvert.SerializeObject(empeño),
                                Modulo = "Empeños",
                                Accion = "Editar"
                            });
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
                            await funciones.SaveBitacora(new ValorBitacora
                            {
                                Valor = JsonConvert.SerializeObject(empeño),
                                Modulo = "Empeños",
                                Accion = "Editar"
                            });
                        }
                        empeñoId = empeño.EmpenoId;
                        lblNumeroEmpeño.Text = empeñoId.ToString();
                        txtIdentificacion.Text = empeño.Cliente.Identificacion;
                        txtNombre.Text = empeño.Cliente.Nombre;
                        txtDescripcion.Text = empeño.Descripcion;
                        txtComentario.Text = empeño.Comentario;
                        cbInteres.DataSource = await _context.Interes.ToListAsync();
                        cbInteres.Text = empeño.Interes.Nombre;
                        txtMonto.Text = empeño.Monto.ToString("N2");
                        Realizado.Text = empeño.Empleado.Usuario;
                        chbEsOro.Checked = empeño.EsOro;
                        Realizado.Text = empeño.Empleado.Usuario;
                        lblVence.Text = empeño.FechaVencimiento.ToString("dd/MM/yyyy");
                        ChangeState(lblEstado, empeño.Estado, empeño);
                        Fecha.Text = empeño.Fecha.ToString("dd/MM/yyyy");

                        funciones.ShowLabels(panelFormulario);
                        if (Program.PerfilId==4 || empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
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
            catch (Exception)
            {

            }
        }

        public void ChangeState(System.Windows.Forms.Label label, Estado estado)
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

        public void ChangeState(System.Windows.Forms.Label label, Estado estado, Empeno empeño)
        {
            label.Visible = false;
            if (empeño.IsDelete)
            {
                label.Text = "Eliminado";
                label.BackColor = Color.DarkRed;
                label.ForeColor = Color.White;
                label.Visible = true;
                return;
            }
            if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador!=null)
            {
                label.Text = "Perdido";
                label.BackColor = Color.Purple;
                label.ForeColor = Color.White;
                label.Visible = true;
                return;
            }
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
                await funciones.ReviewDuplicateEmpeños(id);
                int cantidadMeses = ((int)(DateTime.Today - empeño.Fecha).TotalDays / 30);
                double sobrante = (DateTime.Today - empeño.Fecha).TotalDays % 30;
                cantidadMeses += sobrante > 1 ? 1 : 0;
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
                            intereses.FechaVencimiento = empeño.Intereses.OrderByDescending(x => x.InteresesId).FirstOrDefault().FechaVencimiento.AddMonths(1);
                        }
                        else
                        {
                            intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                        }
                        if (_context.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId && x.FechaVencimiento == intereses.FechaVencimiento).Count() == 0)
                        {
                            _context.Intereses.Add(intereses);
                        }
                    }
                    await _context.SaveChangesAsync();
                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Valor = JsonConvert.SerializeObject(empeño),
                        Modulo = "Empeños",
                        Accion = "Editar"
                    });
                }


                var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId).OrderByDescending(o => o.InteresesId)
                  .FirstOrDefaultAsync();
                if (ultimoInteres!=null)
                {
                    if (ultimoInteres.FechaVencimiento < DateTime.Today && ultimoInteres.Monto > ultimoInteres.Pagado)
                    {
                        empeño.Estado = Estado.Pendiente;
                        _context.Entry(empeño).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        await funciones.SaveBitacora(new ValorBitacora
                        {
                            Valor = JsonConvert.SerializeObject(empeño),
                            Modulo = "Empeños",
                            Accion = "Editar"
                        });
                    }
                    else if (_context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId && i.Monto > i.Pagado && i.FechaVencimiento < DateTime.Today).Count() > 0)
                    {
                        empeño.Estado = Estado.Pendiente;
                        _context.Entry(empeño).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        await funciones.SaveBitacora(new ValorBitacora
                        {
                            Valor = JsonConvert.SerializeObject(empeño),
                            Modulo = "Empeños",
                            Accion = "Editar"
                        });
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
                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Valor = JsonConvert.SerializeObject(empeño),
                        Modulo = "Empeños",
                        Accion = "Editar"
                    });
                }


                var empleado = await _context.Empleados.FindAsync(empeño.EmpleadoId);
                if (empeño != null)
                {
                    empeñoId = empeño.EmpenoId;
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = empeño.Comentario;
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString("N2");
                    Realizado.Text = empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    Realizado.Text = empeño.Empleado.Usuario;
                    Fecha.Text = empeño.Fecha.ToString("dd/MM/yyyy");
                    lblVence.Text = empeño.FechaVencimiento.ToString("dd/MM/yyyy");
                    lblNumeroEmpeño.Text = empeño.EmpenoId.ToString();
                    ChangeState(lblEstado, empeño.Estado, empeño);
                    funciones.ShowLabels(panelFormulario);
                    if (Program.Usuario.PerfilId == 4)
                    {
                        funciones.BlockTextBox(panelFormulario, false);
                    }
                    else
                    {
                        funciones.BlockTextBox(panelFormulario, true);
                    }
                    if (Program.PerfilId==4 || empeño.FechaRetiro != null || empeño.Retirado || empeño.FechaRetiroAdministrador != null || empeño.RetiradoAdministrador || empeño.Estado == Estado.Retirada)
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

        public void CargarPagos()
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                empeñoId = int.Parse(dgvEmpeños.SelectedRows[0].Cells[0].Value.ToString());
                using (DataContext _contextTemp = new DataContext())
                {

                    var list = _contextTemp.Intereses.Where(p => p.EmpenoId == empeñoId)
                        .Select(x => new
                        {
                            Id = x.InteresesId,
                            Interes = x.Empeno.Interes.Porcentaje + "%",
                            Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                            x.Monto,
                            x.Pagado,
                            Vencimiento = x.Monto == x.Pagado ? 0 : DbFunctions.DiffDays(DateTime.Today, x.FechaVencimiento),
                        }).OrderByDescending(i => i.Id)
                        .AsEnumerable()
                        .Select(x => new
                        {
                            x.Id,
                            x.Interes,
                            x.Vence,
                            Monto = x.Monto.ToString("N2"),
                            Pagado = x.Pagado.ToString("N2"),
                            Faltan = x.Vencimiento
                        }).ToList();
                    dgvPagos.DataSource = list; 
                }
            }
        }

        private async void dgvEmpeños_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Program.Usuario.PerfilId == 4)
                {
                    funciones.BlockTextBox(panelFormulario, false);
                }
                else
                {
                    funciones.BlockTextBox(panelFormulario, true);
                }
                await BuscarEmpeño();              
                if (switchPago)
                {
                    await LoadPays();
                }
                else
                {
                    CargarPagos();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private async void txtIdentificacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await FindIdentification();
                if (Program.Usuario.PerfilId == 4)
                {
                    cbInteres.Enabled = false;
                    lblVence.Enabled = false;
                    Fecha.Enabled = false;
                }
                else
                {
                    cbInteres.Enabled = true;
                    lblVence.Enabled = true;
                    Fecha.Enabled = true;
                    Fecha.ForeColor = Color.Black;
                }
            }
        }


        public async Task FindIdentification()
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                var cliente = await _context.Clientes.SingleOrDefaultAsync(c => !c.IsDelete && c.Identificacion == txtIdentificacion.Text);
                if (cliente != null)
                {
                    //AQUI
                    CleanForm();
                    lblEstado.Visible = false;
                    Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    clienteId = cliente.ClienteId;
                    lblIdentificacion.Visible = true;
                    txtIdentificacion.Text = cliente.Identificacion;
                    txtIdentificacion.ForeColor = Color.Black;
                    txtNombre.Text = cliente.Nombre;
                    txtNombre.ForeColor = Color.Black;
                    lblNombre.Visible = true;
                    txtDescripcion.Focus();
                    txtDescripcion.Text = string.Empty;
                    txtDescripcion.ForeColor = Color.Black;
                    lblDescripcion.Visible = true;
                    chbEsOro.Checked = true;
                    dgvPagos.DataSource = null;
                    dgvPagos.Rows.Clear();
                }
                else
                {
                    var resp = MessageBox.Show("El cliente no existe, desea crearlo como uno nuevo?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {
                        CleanForm();
                        Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                        frmOscuro oscuro = new frmOscuro();
                        oscuro.Show();
                        frmClientes frmClientes = new frmClientes();
                        frmClientes.ShowDialog();
                        oscuro.Close();
                        if (Program.Cliente != null)
                        {
                            var clienteTem = Program.Cliente;
                            if (clienteTem != null)
                            {
                                clienteId = clienteTem.ClienteId;
                                lblIdentificacion.Visible = true;
                                txtIdentificacion.Text = clienteTem.Identificacion;
                                txtIdentificacion.ForeColor = Color.Black;
                                txtNombre.Text = clienteTem.Nombre;
                                txtNombre.ForeColor = Color.Black;
                                lblNombre.Visible = true;
                                txtDescripcion.Focus();
                                funciones.IntelligHolders(panelFormulario);
                                dgvPagos.DataSource = null;
                                dgvPagos.Rows.Clear();
                            }
                        }
                        else
                        {
                            funciones.ResetForm(panelFormulario);
                            Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                            dgvPagos.DataSource = null;
                            dgvPagos.Rows.Clear();
                        }
                    }
                }

                if (Program.Usuario.PerfilId == 4)
                {
                    cbInteres.Enabled = false;
                    lblVence.Enabled = false;
                    Fecha.Enabled = false;
                }
                else
                {
                    cbInteres.Enabled = true;
                    lblVence.Enabled = true;
                    Fecha.Enabled = true;
                    Fecha.ForeColor = Color.Black;
                }
            }
        }


        private async void btnIdentificacion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                var cliente = await _context.Clientes.SingleOrDefaultAsync(c => !c.IsDelete && c.Identificacion == txtIdentificacion.Text);
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
            var empeños = _context.Empenos.ToList();
            funciones.ResetForm(panelFormulario);
            cbInteres.Text = "Porcentaje";
            clienteId = 0;
            empeñoId = 0;
            lblNumeroEmpeño.Text = "";
            dgvPagos.DataSource = null;
            dgvPagos.Refresh();
            lblEstado.Visible = false;
            Fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblNumeroEmpeño.Text = empeños.Count()>0? (_context.Empenos.Max(m => m.EmpenoId)+1).ToString():"1";
            lblCantidad.Text = dgvClientes.Rows.Count.ToString();
            lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
            chbEsOro.Checked = true;
            lblEstado.Visible = false;
            lblVence.Text = DateTime.Today.AddMonths(configuracion.Meses).ToString("dd/MM/yyyy");
            if (Program.Usuario.PerfilId == 4)
            {
                cbInteres.Enabled = false;
                lblVence.Enabled = false;
                Fecha.Enabled = false;
            }
            else
            {
                cbInteres.Enabled = true;
                lblVence.Enabled = true;
                Fecha.Enabled = true;
                Fecha.ForeColor = Color.Black;
            }
        }

        private async void iconButton10_Click(object sender, EventArgs e)
        {
            if (empeñoId>0)
            {
                var empeño = await _context.Empenos.FindAsync(empeñoId);
                if (empeño!=null)
                {
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
            }            
        }

        private async void txtMonto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMonto.Text) && txtMonto.Text != "Monto")
                {
                    double monto;
                    double.TryParse(txtMonto.Text, out monto);
                    var intereses = await _context.Interes.OrderBy(i => i.Mayor).ToListAsync();
                    var interes = new Interes();
                    foreach (var item in intereses)
                    {
                        if (item.Mayor <= monto)
                        {
                            interes = item;
                        }
                    }
                    if (interes != null)
                    {
                        cbInteres.Text = interes.Nombre;
                        cbInteres.ForeColor = Color.Black;
                        lblInteres.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

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
            try
            {
                CleanForm();

                var listEmpeños = await _context.Empenos.Where(x => ((x.Intereses.Where(i => i.Monto > i.Pagado && i.FechaVencimiento == DateTime.Today).Count() > 0)
                || (x.MontoPendiente > 0 && x.FechaVencimiento == DateTime.Today)
                )
                && (!x.IsDelete && !x.RetiradoAdministrador && x.Retirado == false && x.FechaRetiro == null
                && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
                && (x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)
                 ).Select(x => new
                 {
                     Id = x.EmpenoId,
                     x.Descripcion,
                     Empleado = x.Empleado.Nombre,
                     Es_Oro = x.EsOro,
                     x.Estado,
                     Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                     Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                     Interes = x.Interes.Porcentaje + "%",
                     Monto = x.Monto,
                     Pendiente = x.MontoPendiente
                 }).ToListAsync();

                dgvEmpeños.DataSource = listEmpeños.AsEnumerable().Select(x => new
                {
                    x.Id,
                    x.Descripcion,
                    x.Empleado,
                    x.Es_Oro,
                    x.Estado,
                    x.Fecha,
                    x.Vence,
                    x.Interes,
                    Monto = x.Monto.ToString("N2"),
                    Pendiente = x.Pendiente.ToString("N2")
                }).ToList();

                lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
            }
            catch (Exception)
            {

            }
        }

        private async void btnPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                CleanForm();
                var listEmpeños = await _context.Empenos.Where(x => ((x.Intereses.Where(i => i.Monto > i.Pagado).Count() > 0) || x.MontoPendiente > 0)
                && (!x.IsDelete && !x.RetiradoAdministrador && x.Retirado == false && x.FechaRetiro == null
                 && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
                 && (x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)
                 ).Select(x => new
                 {
                     Id = x.EmpenoId,
                     x.Descripcion,
                     Empleado = x.Empleado.Nombre,
                     Es_Oro = x.EsOro,
                     x.Estado,
                     Fecha = SqlFunctions.DateName("day", x.Fecha) + "/" + SqlFunctions.DateName("month", x.Fecha) + "/" + SqlFunctions.DateName("year", x.Fecha),
                     Vence = SqlFunctions.DateName("day", x.FechaVencimiento) + "/" + SqlFunctions.DateName("month", x.FechaVencimiento) + "/" + SqlFunctions.DateName("year", x.FechaVencimiento),
                     Interes = x.Interes.Porcentaje + "%",
                     Monto = x.Monto,
                     Pendiente = x.MontoPendiente
                 }).ToListAsync();

                dgvEmpeños.DataSource = listEmpeños.AsEnumerable().Select(x => new
                {
                    x.Id,
                    x.Descripcion,
                    x.Empleado,
                    x.Es_Oro,
                    x.Estado,
                    x.Fecha,
                    x.Vence,
                    x.Interes,
                    Monto = x.Monto.ToString("N2"),
                    Pendiente = x.Pendiente.ToString("N2")
                }).ToList();

                dgvEmpeños.ClearSelection();
                lblCatidadEmpeños.Text = dgvEmpeños.Rows.Count.ToString();
            }
            catch (Exception)
            {

            }
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
        public async Task Print(Empeno empeno)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch =  Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteEmpeño.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);
            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;
            var empleadoId =Program.EmpleadoId;
            var empleado =await _context.Empleados.FindAsync(empleadoId);
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;
            var cliente = await _context.Clientes.FindAsync(empeno.ClienteId);
            cexcel.Cells[14, 2].value = cliente.Identificacion;
            cexcel.Cells[15, 1].value = cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();            

            if (chbEsOro.Checked)
            {
                cexcel.Cells[19, 1].value ="ORO : " + txtDescripcion.Text;
            }
            else
            {
                cexcel.Cells[19, 1].value = txtDescripcion.Text;
            }
            cexcel.Cells[22, 3].value = empeno.Monto.ToString("N2");
            cexcel.Cells[23, 3].value = empeno.Monto.ToString("N2");
            cexcel.Cells[24, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
            string FechaVence = lblVence.Text;           
            cexcel.Cells[26, 3].value = ((double)empeno.Monto *((double)empeno.Interes.Porcentaje/(double)100)).ToString("N2");
            cexcel.Cells[28, 3].value = "Pendiente";
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }

        public async Task PrintAnulacion(Empeno empeno)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();

            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteAnulacion.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);
            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;
            var empleadoId = Program.EmpleadoId;
            var empleado = await _context.Empleados.FindAsync(empleadoId);
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (chbEsOro.Checked)
            {
                cexcel.Cells[19, 1].value = "ORO : " + txtDescripcion.Text;
            }
            else
            {
                cexcel.Cells[19, 1].value = txtDescripcion.Text;
            }
            cexcel.Cells[22, 3].value = empeno.Monto.ToString("N2");
            cexcel.Cells[23, 3].value = empeno.Monto.ToString("N2");
            cexcel.Cells[24, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
            string FechaVence = lblVence.Text;
            cexcel.Cells[26, 3].value = ((double)empeno.Monto * ((double)empeno.Interes.Porcentaje / (double)100)).ToString("N2");
            cexcel.Cells[28, 3].value = "Anulada";
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

                await Print(empeño);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var resp = MessageBox.Show("Esta seguro que desea eliminar los datos", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    if (!funciones.ValidatePIN("Borrar Empeño"))
                        return;

                    var empeñoId = dgvEmpeños.SelectedRows[0].Cells[0].Value;
                    var empeño = await _context.Empenos.FindAsync(empeñoId);
                    empeño.EditorId = Program.EmpleadoId;
                    empeño.IsDelete = true;
                    _context.Entry(empeño).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Elemento eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadEmpeños(empeño.EmpenoId);
                    await PrintAnulacion(empeño);
                }
            }
            else
            {
                MessageBox.Show("Debe haber un registro seleccionado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Editar Pago"))
                return;

            frmOscuro oscuro = new frmOscuro();
            oscuro.Show();
            int interesId = int.Parse(dgvPagos.SelectedRows[0].Cells[0].Value.ToString());
            frmEmpeñoInteres frmEmpeñoInteres = new frmEmpeñoInteres(interesId);
            frmEmpeñoInteres.ShowDialog();
            try
            {
                if (switchPago)
                {
                    await LoadPays();
                }
                else
                {
                    CargarPagos();
                }
            }
            catch (Exception)
            {

            }
            oscuro.Close();
        }

        private void cbInteres_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lblInteres.Visible = true;
        }       

        private void txtComentario_TextChanged(object sender, EventArgs e)
        {

        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Borrar Pago"))
                return;
          
            var resp=MessageBox.Show("Esta seguro que desea elminar los datos", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp==DialogResult.Yes)
            {
                int interesId = int.Parse(dgvPagos.SelectedRows[0].Cells[0].Value.ToString());
                if (switchPago)
                {
                    var pago = await _context.Pago.FindAsync(interesId);
                    var monto = pago.Monto;
                    if (pago.TipoPago==TipoPago.Interes)
                    {
                        var list = await _context.Intereses.Where(i => i.PagoId == pago.PagoId).OrderByDescending(i => i.InteresesId).ToListAsync();
                        foreach (var item in list)
                        {
                            if (monto > item.Pagado)
                            {
                                monto -= item.Pagado;
                                item.Pagado = 0;
                                item.PagoId = null;
                                _context.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                monto = 0;
                                item.Pagado -= monto;
                                item.PagoId = null;
                                _context.Entry(item).State = EntityState.Modified;
                            }
                            if (monto == 0)
                                break;

                        }
                     
                    }
                    else
                    {
                        var empeño = await _context.Empenos.FindAsync(pago.EmpenoId);
                        empeño.MontoPendiente += monto;
                        _context.Entry(empeño).State = EntityState.Modified;
                    }
                    _context.Pago.Remove(pago);

                }
                else
                {
                    var intereses = await _context.Intereses.FindAsync(interesId);
                    _context.Intereses.Remove(intereses);
                }

                await _context.SaveChangesAsync();
                MessageBox.Show("Elemento eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (switchPago)
            {
                await LoadPays();
            }
            else
            {
                CargarPagos();
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMonto_KeyUp(object sender, KeyEventArgs e)
        {
            funciones.KeyNumber(sender);
        }

        private async void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvEmpeños.SelectedRows.Count > 0)
            {
                var empeño = await _context.Empenos.FindAsync(dgvEmpeños.SelectedRows[0].Cells[0].Value);
                var empleado = await _context.Empleados.FindAsync(empeño.EmpleadoId);
                if (empeño != null)
                {
                    txtIdentificacion.Text = empeño.Cliente.Identificacion;
                    txtNombre.Text = empeño.Cliente.Nombre;
                    txtDescripcion.Text = empeño.Descripcion;
                    txtComentario.Text = empeño.Comentario;
                    cbInteres.DataSource = await _context.Interes.ToListAsync();
                    cbInteres.Text = empeño.Interes.Nombre;
                    txtMonto.Text = empeño.Monto.ToString("N2");
                    Realizado.Text = empleado.Usuario;
                    chbEsOro.Checked = empeño.EsOro;
                    funciones.BlockTextBox(panelFormulario, false);
                    if (switchPago)
                    {
                        await LoadPays();
                    }
                    else
                    {
                        CargarPagos();
                    }
                }
            }
        }

        private void panelFormulario_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnPagos_Click(object sender, EventArgs e)
        {
            switchPago = true;
            btnPagos.BackColor=Color.FromArgb(66, 133, 244);
            btnIntereses.BackColor = Color.DimGray;
            btnEditarPago.Enabled = false;
            btnEditarPago.BackColor = Color.DimGray;
            btnVerPago.Enabled = false;
            btnVerPago.BackColor = Color.DimGray;
            btnReimprimirPago.Enabled = true;
            btnReimprimirPago.BackColor = Color.FromArgb(17, 2, 115);

            await LoadPays();
        }

        public async Task LoadPays()
        {
            if (empeñoId>0)
            {
                var list = await _context.Pago.Where(p => p.EmpenoId == empeñoId).ToListAsync();
                dgvPagos.DataSource = list.AsEnumerable().Select(x => new
                {
                    Id = x.PagoId,
                    x.Fecha,
                    x.TipoPago,
                    Monto=x.Monto.ToString("N2")
                }).ToList();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun Empeño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnIntereses_Click(object sender, EventArgs e)
        {
            switchPago = false;
            btnPagos.BackColor = Color.DimGray;
            btnIntereses.BackColor = Color.FromArgb(66, 133, 244);
            btnEditarPago.Enabled = true;
            btnEditarPago.BackColor = Color.FromArgb(17, 2, 115);
            btnVerPago.Enabled = true;
            btnVerPago.BackColor = Color.FromArgb(17, 2, 115);
            btnReimprimirPago.Enabled = false;
            btnReimprimirPago.BackColor = Color.DimGray;
            CargarPagos();
        }



        #region Funciones
        public async Task PrintAbono(Empeno empeno, Pago pago)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteAbono.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            var usuario =await funciones.GetEmpleadoByUser(Program.Usuario.Usuario);

            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;

            cexcel.Cells[8, 2].value = pago.PagoId;
            cexcel.Cells[9, 2].value = usuario.Nombre;
            cexcel.Cells[10, 2].value = usuario.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (empeno.EsOro)
            {
                cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
            }
            else
            {
                cexcel.Cells[19, 1].value = empeno.Descripcion;
            }
            cexcel.Cells[23, 3].value = (empeno.MontoPendiente + pago.Monto).ToString("N");
            cexcel.Cells[24, 3].value = pago.Monto.ToString("N2");
            cexcel.Cells[25, 3].value = empeno.MontoPendiente;
            cexcel.Cells[27, 3].value = pago.Monto.ToString("N2");
            cexcel.Cells[29, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
            cexcel.Cells[31, 3].value = empeno.Estado.ToString();
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }

        public async Task PrintRetiro(Empeno empeno, Pago pago)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteCancelacion.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            var usuario = await funciones.GetEmpleadoByUser(Program.Usuario.Usuario);

            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;
            cexcel.Cells[7, 2].value = pago.PagoId;
            cexcel.Cells[9, 2].value = usuario.Nombre;
            cexcel.Cells[10, 2].value = Program.Usuario.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (empeno.EsOro)
            {
                cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
            }
            else
            {
                cexcel.Cells[19, 1].value = empeno.Descripcion;
            }

            cexcel.Cells[24, 3].value = _context.Intereses.Where(i => i.EmpenoId == empeno.EmpenoId).Sum(i => i.Monto).ToString("N2");
            cexcel.Cells[25, 3].value = empeno.MontoPendiente;
            cexcel.Cells[26, 3].value = pago.Monto;
            cexcel.Cells[28, 3].value = "Retirado";
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }

        public async Task PrintInteres(Empeno empeno, List<Intereses> intereses, Pago pago)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteInteres.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;

            var empleado = await funciones.GetEmpleadoByUser(Program.Usuario.Usuario);
            cexcel.Cells[8, 2].value = pago.PagoId;
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha;
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (empeno.EsOro)
            {
                cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
            }
            else
            {
                cexcel.Cells[19, 1].value = empeno.Descripcion;
            }
            cexcel.Cells[22, 4].value = empeno.MontoPendiente;
            var index = 0;
            foreach (var item in intereses)
            {
                cexcel.Cells[26+index, 1].value = item.FechaVencimiento.ToString("dd/MM/yyyy");
                cexcel.Cells[26+index, 3].value = item.Pagado.ToString("N2");

                Microsoft.Office.Interop.Excel.Worksheet ws = cexcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                Range line = (Range)cexcel.Rows[27+ index];
                line.Insert();
                ++index;
                ws.get_Range("A" + (26 + index), "B" + (26 + index)).Merge();
                ws.get_Range("C" + (26 + index), "D" + (26 + index)).Merge();
                
            }

            cexcel.Cells[28 + index, 3].value = intereses.Sum(i => i.Pagado);
            cexcel.Cells[30 + index, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
            cexcel.Cells[32 + index, 3].value = empeno.Estado.ToString();
           
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
        #endregion

        private async void btnReimprimirPago_Click(object sender, EventArgs e)
        {
            if (dgvPagos.SelectedRows.Count>0)
            {
                var pago = await _context.Pago.FindAsync(dgvPagos.SelectedRows[0].Cells[0].Value);
                if (pago!=null)
                {
                    var empeño = pago.Empeno;
                    var intereses = empeño.Intereses.Where(p => p.PagoId == pago.PagoId).ToList();
                    if (pago.TipoPago==TipoPago.Interes)
                    {
                        await PrintInteres(empeño, intereses, pago);
                    }
                    if (empeño.Estado==Estado.Retirada)
                    {
                        await PrintRetiro(empeño, pago);
                    }
                    else
                    {
                        await PrintAbono(empeño, pago);
                    }
                }
            }
        }               
    }
}
