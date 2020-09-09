using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Funciones;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmVencidos : System.Windows.Forms.Form
    {
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        List<Empeno> empeños = new List<Empeno>();
        int index;
        Configuracion configuracion = new Configuracion();
        EmailFuncion emailFuncion = new EmailFuncion();

        public frmVencidos()
        {
            InitializeComponent();
        }

        private async void frmArqueo_Load(object sender, EventArgs e)
        {
            try
            {
                await funciones.ReviewEmpeños();

                empeños = await _context.Empenos.Where(x => !x.IsDelete && x.Estado == Estado.Vencido
                 && !x.Retirado && x.FechaRetiro == null
                 && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
              .Include(x => x.Intereses).ToListAsync();
                configuracion = _context.Configuraciones.FirstOrDefault();
                LoadDetalle();
                dtDesde.Value = DateTime.Today;
                dtHasta.Value = DateTime.Today;
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
        }

        private async void btnGuardarCierreCaja_Click(object sender, EventArgs e)
        {
            if (configuracion.EmailNotification != string.Empty) 
            {
                var empleado = await _context.Empleados.FindAsync(Program.EmpleadoId);
                var str = "<p>" +
                    "<table><tr><td></td><td>Cantidad</td><td>Valor</td></tr>" +
                    "<tr><td>Total Vencidos</td><td>" + lblTotalVencidos.Text + "</td><td>"+ txtTotalVencido.Text +"</td></tr>" +
                    "<tr><td>Total Vencidos Retirados</td><td>" + lblTotalRetirados.Text + "</td><td>" + txtTotalRetirados.Text + "</td></tr>" +
                    "<tr><td>Total Vencidos Prorroga</td><td>" + lblTotalProrroga.Text + "</td><td>" + txtTotalProrroga.Text + "</td></tr>" +
                    "</table></p>";

                await emailFuncion.SendMailVencidos(configuracion.EmailNotification,
                    "Notificacion del Proceso de Vencidos " + configuracion.Compañia,
                    "<p>Se ha realizado departe del Local " + configuracion.Compañia + " en " 
                    + configuracion.Direccion + ", un procesos de sacar vencidos por " + empleado.Nombre + "</p>",
                    dgvDetalles, str) ;
            }
            //Print();
        }

        #region Funciones
        public async void Print()
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteVencidos.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            cexcel.Cells[3, 1].value = configuracion.Compañia;
            cexcel.Cells[4, 1].value = configuracion.Direccion;
            cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
            cexcel.Cells[6, 1].value = configuracion.Nombre;
            cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;


            var empleadoId = Program.EmpleadoId;
            var empleado = _context.Empleados.Find(empleadoId);
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;
            cexcel.Cells[11, 2].value = txtFecha.Text;


            var index = 0;
            foreach (DataGridViewRow item in dgvDetalles.Rows)
            {
                cexcel.Cells[14 + index, 1].value = item.Cells[0].Value;
                cexcel.Cells[14 + index, 2].value = item.Cells[3].Value.ToString();
                cexcel.Cells[14 + index, 3].value = item.Cells[4].Value.ToString();
                cexcel.Cells[14 + index, 4].value = item.Cells[11].Value.ToString();

                Range range = (Range)cexcel.Rows[15 + index];
                Range line = range;
                line.Insert();
                ++index;
            }
            double saldoVencido = double.Parse(txtTotalVencido.Text);
            double saldoRetirado = double.Parse(txtTotalRetirados.Text);
            double saldoProroga = double.Parse(txtTotalProrroga.Text);

            //cexcel.Cells[17 + index, 3].value = saldoPrincipal.ToString("N2");
            //cexcel.Cells[18 + index, 3].value = saldoIntereses.ToString("N2");
            //cexcel.Cells[19 + index, 3].value = saldoGeneral.ToString("N2");
            //cexcel.Cells[20 + index, 3].value = saldoAlDia.ToString("N2");
            cexcel.Cells[21 + index, 3].value = saldoVencido.ToString("N2");
            cexcel.Cells[22 + index, 3].value = saldoRetirado.ToString("N2");
            cexcel.Cells[23 + index, 3].value = saldoProroga.ToString("N2");

            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }


        public async Task PrintVencido(Empeno objempeño, Vencimientos vencimientos)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteVencimiento.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);
         
            var empleado = await _context.Empleados.FindAsync(Program.EmpleadoId);

            cexcel.Visible = false;
            cexcel.Cells[7, 2].value = vencimientos.Consecutivo;
            cexcel.Cells[8, 2].value = empleado.Nombre;
            cexcel.Cells[9, 2].value = empleado.Usuario;
            cexcel.Cells[13, 2].value = objempeño.Cliente.Identificacion;
            cexcel.Cells[14, 1].value = objempeño.Cliente.Nombre;

            cexcel.Cells[15, 2].value = vencimientos.Fecha.ToString("dd/MM/yyyy");
            cexcel.Cells[16, 2].value = objempeño.EmpenoId;

            cexcel.Cells[18, 1].value = objempeño.Descripcion;


            cexcel.Cells[23, 3].value = objempeño.MontoPendiente;

            cexcel.Cells[25, 3].value = "Vencido";
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalles.SelectedRows.Count > 0)
                {
                    MessageBox.Show(dgvDetalles.SelectedRows[0].Cells[2].Value.ToString());
                }
            }
            catch (Exception)
            {

            }

        }

        private async void dgvDetalles_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12 || e.ColumnIndex == 10)
            {

                DialogResult result = MessageBox.Show("Seguro que desea retirar este empeño?", "Salir", MessageBoxButtons.YesNoCancel);
                int conse = 0;
                if (result == DialogResult.Yes)
                {
                    int empeñoId = int.Parse(dgvDetalles.SelectedRows[0].Cells[0].Value.ToString());
                    var temporal = empeños.Where(x => x.EmpenoId == empeñoId).SingleOrDefault();
                    var empeño = await _context.Empenos.Where(x => x.EmpenoId == empeñoId).SingleOrDefaultAsync();
                    temporal.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                    empeño.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                    temporal.FechaRetiroAdministrador = DateTime.Now;
                    empeño.FechaRetiroAdministrador = DateTime.Now;
                    temporal.RetiradoAdministrador = true;
                    empeño.RetiradoAdministrador = true;
                    empeño.Estado = Estado.Retirado;
                    _context.Entry(empeño).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var vencimiento = new Vencimientos();
                    if (_context.Vencimientos.Any())
                    {
                        vencimiento = new Vencimientos
                        {
                            Consecutivo = _context.Vencimientos.Max(v => v.Consecutivo) + 1,
                            EmpenoId = empeñoId,
                            EmpleadoId = empeño.EditorId.Value,
                            Fecha = DateTime.Today,
                        };
                    }
                    else
                    {
                        vencimiento = new Vencimientos
                        {
                            Consecutivo = 1,
                            EmpenoId = empeñoId,
                            EmpleadoId = empeño.EditorId.Value,
                            Fecha = DateTime.Today,
                        };
                    }                    

                    _context.Vencimientos.Add(vencimiento) ;
                    await _context.SaveChangesAsync();
                    await PrintVencido(empeño, vencimiento);
                }
                else if (result == DialogResult.No)
                {

                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
               
            }
            else if (e.ColumnIndex == 13 || e.ColumnIndex == 9)
            {
                int empeñoId = int.Parse(dgvDetalles.SelectedRows[0].Cells[0].Value.ToString());
                var temporal = empeños.Where(x => x.EmpenoId == empeñoId).SingleOrDefault();
                var empeño = empeños.Where(x => x.EmpenoId == empeñoId).SingleOrDefault();
                temporal.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                empeño.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                frmOscuro oscuro = new frmOscuro();
                oscuro.Show();
                frmProroga frmProroga = new frmProroga(empeño.EmpenoId);
                frmProroga.ShowDialog();
                oscuro.Close();

                if (Program.Proroga)
                {
                    var proroga = _context.Prorrogas.Where(p => p.EmpenoId == empeñoId).FirstOrDefault();
                    temporal.Prorroga = true;
                    empeño.Prorroga = true;
                    if (!string.IsNullOrEmpty(empeño.Cliente.Correo))
                    {
                        EmailFuncion emailFuncion = new EmailFuncion();
                        var str = "Se le a otorgado una <b>prórroga de " + proroga.DiasProrroga + " días</b>, para que pueda retirar su Empeño #" + empeño.EmpenoId +
                            " <b>vencido el " + empeño.FechaVencimiento.ToString("dd/MM/yyyy") + "</b> por : <br /><i>" + empeño.Descripcion + "</i><br /><br />";
                        await emailFuncion.SendMail(empeño.Cliente.Correo, "Proróga de Empeño #" + empeñoId + " en " + configuracion.Compañia, str, empeño);
                    }
                }

                _context.Entry(empeño).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            index = dgvDetalles.SelectedRows[0].Index;
            LoadDetalle();
            dgvDetalles.Rows[0].Selected = false;
            dgvDetalles.Rows[index].Selected = true;
            dgvDetalles.FirstDisplayedScrollingRowIndex = dgvDetalles.SelectedRows[0].Index;
        }

        #region Funciones
        public async void LoadDetalle()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.Rows.Clear();
            dgvDetalles.Columns.Clear();
            dgvDetalles.Refresh();

            var totalPrincipal = empeños.Sum(l => l.Monto);
            var totalIntereses = empeños.SelectMany(l => l.Intereses).Sum(l => l.Monto);
            var totalGeneral = totalPrincipal + totalIntereses;

            var totalProrroga = empeños.Where(m => m.Prorrogas.Count() > 0).Sum(m => m.Monto + m.Intereses.Sum(i => i.Monto));
            lblTotalProrroga.Text = empeños.Where(m => m.Prorrogas.Count() > 0).Count().ToString();
            var totalActivos = empeños.Where(m => m.Estado == Estado.Vigente || m.Estado == Estado.Pendiente).Sum(m => m.Monto + m.Intereses.Sum(i => i.Monto));

            var list = empeños.Select(x => new
            {
                Id = x.EmpenoId,
                Empeño = x.Descripcion,
                x.Cliente.Identificacion,
                Cliente = x.Cliente.Nombre,
                Estado = x.Estado,
                Vencimiento = x.FechaVencimiento,
                Empleado = x.Empleado.Nombre,
                x.Prorroga,
                x.Monto,
                MontoPendiente = x.MontoPendiente,
                x.Intereses,
                x.Prorrogas,
                x.RetiradoAdministrador,
                x.FechaRetiroAdministrador,
            }).ToList();

            var list2 = list.Select(x => new
            {
                x.Id,
                x.Empeño,
                x.Identificacion,
                x.Cliente,
                x.Estado,
                Vencimiento = x.Vencimiento.ToString("dd/MM/yyyy"),
                Vencido = (x.Vencimiento - DateTime.Today).TotalDays + " días",
                x.Empleado,
                x.Prorroga,
                x.RetiradoAdministrador,
                x.FechaRetiroAdministrador,
                x.Prorrogas,
                x.Monto,
                MontoPendiente = (x.MontoPendiente + (x.Intereses != null ? x.Intereses.Sum(i => i.Monto - i.Pagado) : 0)).ToString("N2"),
                x.Intereses
            }).ToList();

            dgvDetalles.DataSource = list2.Select(x => new
            {
                x.Id,
                x.Empeño,
                x.Identificacion,
                x.Cliente,
                x.Estado,
                x.Vencimiento,
                x.Vencido,
                x.Empleado,
                x.Prorroga,
                x.RetiradoAdministrador,
                x.Monto,
                x.MontoPendiente,
            }).ToList();

            var totalVencido = empeños.Where(l => l.Estado == Estado.Vencido && l.Prorrogas.Count() == 0 && !l.RetiradoAdministrador).Sum(l => l.Monto + l.Intereses.Sum(i => i.Monto));
            lblTotalVencidos.Text = empeños.Where(l => l.Estado == Estado.Vencido && l.Prorrogas.Count() == 0 && !l.RetiradoAdministrador).Count().ToString();
            var totalRetirados = list2.Where(l => l.RetiradoAdministrador || l.FechaRetiroAdministrador != null).Sum(l => l.Monto + l.Intereses.Sum(i => i.Monto));
            lblTotalRetirados.Text = list2.Where(l => l.RetiradoAdministrador || l.FechaRetiroAdministrador != null).Count().ToString();

            ////ADD BOTONES COLUMNS
            var buttonDataGridView = new DataGridViewButtonColumn();
            buttonDataGridView.FlatStyle = FlatStyle.Flat;
            buttonDataGridView.HeaderText = "";
            buttonDataGridView.Text = "Eliminar";
            buttonDataGridView.UseColumnTextForButtonValue = true;
            dgvDetalles.Columns.AddRange(buttonDataGridView);
            buttonDataGridView = new DataGridViewButtonColumn();
            buttonDataGridView.FlatStyle = FlatStyle.Flat;
            buttonDataGridView.HeaderText = "";
            buttonDataGridView.Text = "Prórroga";
            buttonDataGridView.UseColumnTextForButtonValue = true;
            dgvDetalles.Columns.AddRange(buttonDataGridView);
            dgvDetalles.Refresh();

            txtTotalVencido.Text = totalVencido.ToString("N2");
            txtTotalRetirados.Text = totalRetirados.ToString("N2");
            txtTotalProrroga.Text = totalProrroga.ToString("N2");
            txtFecha.Text = DateTime.Now.ToString();
            txtFecha.Enabled = false;
            var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            empleadoId = empleadoId == null ? 1 : empleadoId;
            var empleado = await _context.Empleados.FindAsync(empleadoId);
            txtEmpleado.Text = empleado.Nombre;
            txtEmpleado.Enabled = false;
        }
        #endregion


        private void panelArqueo_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {                
                var hasta = dtHasta.Value.AddHours(23).AddHours(59);               

                empeños = await _context.Empenos.Where(x =>x.FechaVencimiento>=dtDesde.Value && x.FechaVencimiento<=hasta
                && !x.IsDelete && x.Estado == Estado.Vencido
                 && !x.Retirado && x.FechaRetiro == null
                 && !x.RetiradoAdministrador && x.FechaRetiroAdministrador == null)
              .Include(x => x.Intereses).ToListAsync();
                configuracion = _context.Configuraciones.FirstOrDefault();
                LoadDetalle();              
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
        }
    }
}
