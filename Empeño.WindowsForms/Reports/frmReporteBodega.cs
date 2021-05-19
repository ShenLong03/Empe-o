using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.ViewReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Reports
{
    public partial class frmReporteBodega : Form
    {
        DataContext _context = new DataContext();

        public frmReporteBodega()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        public async Task Buscar()
        {
            chartEmpeños.Update();
            DateTime desde = dtDesde.Value;
            DateTime hasta = dtHasta.Value.AddHours(23).AddMinutes(59);

            var list = await _context.Intereses.Where(e => e.FechaPago >= desde && e.FechaPago <= hasta && e.MontoBodega>0)
                .Include(e=>e.Empeno)
                .Include(e=>e.Interes)
                .ToListAsync();

            dgvEmpeños.DataSource = null;
            dgvEmpeños.Rows.Clear();
            chartEmpeños.Series[0].Points.Clear();

            if (!ckbBorrados.Checked)
            {
                list = list.Where(l => !l.Empeno.IsDelete).ToList();
            }

            if (!chbTodo.Checked)
            {
                if (!chbActivos.Checked)
                    list = list.Where(l => l.Empeno.Estado != CommonEF.Enum.Estado.Vigente).ToList();

                if (!chbPerdidos.Checked)
                    list = list.Where(l => l.Empeno.Estado != CommonEF.Enum.Estado.Retirado).ToList();

                if (!chbPendientes.Checked)
                    list = list.Where(l => l.Empeno.Estado != CommonEF.Enum.Estado.Pendiente).ToList();

                if (!chbRetirados.Checked)
                    list = list.Where(l => l.Empeno.Estado != CommonEF.Enum.Estado.Anulado).ToList();

                if (!chbVencidos.Checked)
                    list = list.Where(l => l.Empeno.Estado != CommonEF.Enum.Estado.Vencido).ToList();
            }


            var listEmpeños = list.Select(x => new EmpeñoReporte
            {
                ClienteId = x.Empeno.ClienteId,
                Identificacion = x.Empeno.Cliente.Identificacion,
                Cliente = x.Empeno.Cliente.Nombre,
                Descripcion = x.Empeno.Descripcion,
                Es_Oro = x.Empeno.EsOro,
                EmpeñoId = x.EmpenoId,
                Empleado = x.Empeno.Empleado.Nombre,
                EmpleadoId = x.Empeno.EmpleadoId,
                Estado = x.Empeno.Estado == CommonEF.Enum.Estado.Vigente ? "Activo" :
               x.Empeno.Estado == CommonEF.Enum.Estado.Anulado ? "Cancelada" :
               x.Empeno.Estado == CommonEF.Enum.Estado.Pendiente ? "Pendiente" :
               x.Empeno.Estado == CommonEF.Enum.Estado.Anulado ? "Retirada" :
               x.Empeno.Estado == CommonEF.Enum.Estado.Vencido ? "Vencido" : "",
                Fecha = x.Empeno.Fecha,
                Interes = x.Interes.Porcentaje,
                Monto = x.Empeno.Monto,
                MontoInteres = x.MontoBodega,
                Pendiente = x.Empeno.MontoPendiente,
                Vencimiento = x.FechaVencimiento,
            });
            for (int i = 0; i < 5; i++)
            {
                var estado = GetEstado(i);
                chartEmpeños.Series[0].Points.AddXY(GetEstadoName(i), list.Select(e=>e.Empeno).Where(x => x.Estado == estado
                && x.Fecha >= desde && x.Fecha <= hasta).Count());
            }
            chartEmpeños.Update();
            dgvEmpeños.DataSource = listEmpeños.ToList();
            dgvEmpeños.Refresh();
            if (ckbBorrados.Checked)
            {
                var borrados = list.Where(l => l.Empeno.IsDelete).ToList();
                foreach (DataGridViewRow item in dgvEmpeños.Rows)
                {
                    int empeñoId = int.Parse(item.Cells[0].Value.ToString());
                    if (borrados.Where(b => b.EmpenoId == empeñoId).Any())
                    {
                        item.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            txtMonto.Text = listEmpeños.Sum(l => l.MontoInteres).ToString("N2");
            txtPendiente.Text = (listEmpeños.Sum(l => l.MontoInteres) * 0.13).ToString("N2");
        }

        private string GetEstadoName(int i)
        {
            switch (i)
            {
                case 0:
                    return "Activo";
                case 1:
                    return "Pendiente";
                case 2:
                    return "Vencido";
                case 3:
                    return "Cancelada";
                case 4:
                    return "Retirada";
                default:
                    return "";
            }
        }

        private Estado GetEstado(int i)
        {
            switch (i)
            {
                case 0:
                    return Estado.Vigente;
                case 1:
                    return Estado.Pendiente;
                case 2:
                    return Estado.Vencido;
                case 3:
                    return Estado.Anulado;
                case 4:
                    return Estado.Anulado;
                default:
                    return Estado.Vigente;
            }
        }

        private async void frmReporteBodega_Load(object sender, EventArgs e)
        {
            string drt = "01/" + DateTime.Today.ToString("MM/yyyy");
            dtDesde.Value = DateTime.Parse(drt);
            dtHasta.Value = DateTime.Today;
            chbTodo.Checked = true;
            chbActivos.Checked = false;
            chbPendientes.Checked = false;
            chbPerdidos.Checked = false;
            chbRetirados.Checked = false;
            await Buscar();           
        }

        private async void btnPrint_Click(object sender, EventArgs e)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\Bodega.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            cexcel.Cells[2, 1].value = $"Empeños y Venta       {configuracion.Compañia}";
            cexcel.Cells[3, 3].value = dtDesde.Value.ToShortDateString();
            cexcel.Cells[5, 3].value = dtHasta.Value.ToShortDateString();
            cexcel.Cells[6, 3].value = txtMonto.Text;
            cexcel.Cells[8, 3].value = txtPendiente.Text;           
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
    }
}
