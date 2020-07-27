using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Funciones;
using Empeño.WindowsForms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmArqueo : Form
    {
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        List<Empeno> empeños = new List<Empeno>();
        int index;

        public frmArqueo()
        {
            InitializeComponent();
        }

        private async void frmArqueo_Load(object sender, EventArgs e)
        {
            try
            {
                await funciones.ReviewEmpeños();

                empeños = await _context.Empenos.Where(x => (x.Estado == Estado.Activo
                 || x.Estado == Estado.Pendiente
                 || x.Estado == Estado.Vencido)
                 && (!x.Retirado || x.FechaRetiro == null)
                 && (!x.RetiradoAdministrador || x.FechaRetiroAdministrador == null))
              .Include(x => x.Intereses).ToListAsync();

                LoadDetalle();
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
        }

        private void btnGuardarCierreCaja_Click(object sender, EventArgs e)
        {
            Print();           
        }

        #region Funciones
        public async void Print()
        {
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();

            cexcel.Workbooks.Open("C:\\Empeños\\Arqueo\\Arqueo.xlsx", true, true);

            cexcel.Visible = false;

            var index =15;

            foreach (DataGridViewRow item in dgvDetalles.Rows)
            {
                cexcel.Cells[index, 1].value = item.Cells[0].Value;
                cexcel.Cells[index, 2].value = item.Cells[1].Value;
                cexcel.Cells[index, 3].value = item.Cells[2].Value;
                cexcel.Cells[index, 4].value = item.Cells[3].Value;
                cexcel.Cells[index, 5].value = item.Cells[4].Value;
                cexcel.Cells[index, 6].value = item.Cells[5].Value;
                ++index;
            }
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
                if (dgvDetalles.SelectedRows.Count>0)
                {
                    MessageBox.Show(dgvDetalles.SelectedRows[0].Cells[2].Value.ToString());
                }
            }
            catch (Exception)
            {

            }

        }

        private async void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==12 || e.ColumnIndex==10)
            {
                int empeñoId = int.Parse(dgvDetalles.SelectedRows[0].Cells[0].Value.ToString());
                var temporal=empeños.Where(x => x.EmpenoId == empeñoId).SingleOrDefault();
                var empeño = _context.Empenos.Where(x => x.EmpenoId == empeñoId).SingleOrDefault();
                temporal.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                empeño.EditorId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);               
                temporal.FechaRetiroAdministrador = DateTime.Now;
                empeño.FechaRetiroAdministrador = DateTime.Now;
                temporal.RetiradoAdministrador = true;
                empeño.RetiradoAdministrador = true;

                _context.Entry(empeño).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else if(e.ColumnIndex==13 || e.ColumnIndex==9)
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
                    EmailFuncion emailFuncion =new EmailFuncion();
                    await emailFuncion.SendMail(empeño.Cliente.Correo, "Proróga de Empeño #" + empeñoId, "Estimado " + empeño.Cliente.Nombre + ", '/b"
                        + "Se le a otorgado una proróga de " + proroga.DiasProrroga + "días, para que pueda retirar su empeño.");
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
            }).ToList(); ;

            var totalPrincipal = empeños.Sum(l => l.MontoPendiente);
            var totalIntereses = empeños.SelectMany(l => l.Intereses).Sum(l => l.Monto - l.Pagado);
            var totalGeneral = totalPrincipal + totalIntereses;

            var totalPendiente = list2.Where(l => l.Estado == Estado.Pendiente).Sum(l => double.Parse(l.MontoPendiente));
            var totalVencido = list2.Where(l => l.Estado == Estado.Vencido && l.Prorrogas.Count() == 0 && !l.RetiradoAdministrador).Sum(l => double.Parse(l.MontoPendiente));
            var totalRetirados = list2.Where(l => l.RetiradoAdministrador || l.FechaRetiroAdministrador != null).Sum(l => double.Parse(l.MontoPendiente));
            var totalProrroga = empeños.Where(m => m.Prorrogas.Count() > 0).Sum(m => m.MontoPendiente + m.Intereses.Sum(i => i.Monto - i.Pagado));
            var totalActivos = empeños.Where(m => m.Estado == Estado.Activo || m.Estado == Estado.Pendiente).Sum(m => m.MontoPendiente);


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
            buttonDataGridView.Text = "Proróga";
            buttonDataGridView.UseColumnTextForButtonValue = true;
            dgvDetalles.Columns.AddRange(buttonDataGridView);
            dgvDetalles.Refresh();

            txtTotalVencido.Text = totalVencido.ToString("N2");
            txtTotalRetirados.Text = totalRetirados.ToString("N2");
            txtTotalProrroga.Text = totalProrroga.ToString("N2");
            txtTotalPrincipal.Text = totalPrincipal.ToString("N2");
            txtTotalIntereses.Text = totalIntereses.ToString("N2");
            txtTotalGeneral.Text = totalGeneral.ToString("N2");
            txtTotalAlDia.Text = totalActivos.ToString("N2");
            txtFecha.Text = DateTime.Now.ToString();
            txtFecha.Enabled = false;
            var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            empleadoId = empleadoId == null ? 1 : empleadoId;
            var empleado = await _context.Empleados.FindAsync(empleadoId);
            txtEmpleado.Text = empleado.Nombre;
            txtEmpleado.Enabled = false;
        }
        #endregion

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dgvDetalles.Width, this.dgvDetalles.Height);
            dgvDetalles.DrawToBitmap(bm, new Rectangle(0, 0, this.dgvDetalles.Width, this.dgvDetalles.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
    }
}
