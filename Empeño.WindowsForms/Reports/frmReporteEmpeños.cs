using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.ViewReports;
using Microsoft.Reporting.WinForms;
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

namespace Empeño.WindowsForms.Reports
{
    public partial class frmReporteEmpeños : Form
    {
        public int xClick = 0, yClick = 0;        
        int lx, ly;
        int sw, sh;

        DataContext _context = new DataContext();

        public frmReporteEmpeños()
        {
            InitializeComponent();
        }

        private void frmReporteEmpeños_Load(object sender, EventArgs e)
        {
            var listEmpeños = _context.Empenos.Where(l=>!l.IsDelete).Select(x => new EmpeñoReporte
            {
                ClienteId=x.ClienteId,
                Identificacion=x.Cliente.Identificacion,
                Cliente=x.Cliente.Nombre,
                Descripcion=x.Descripcion,
                Es_Oro=x.EsOro,
                EmpeñoId=x.EmpenoId,
                Empleado=x.Empleado.Nombre,
                EmpleadoId=x.EmpleadoId,
                Estado=x.Estado==CommonEF.Enum.Estado.Activo?"Activo":
                x.Estado==CommonEF.Enum.Estado.Cancelada?"Cancelada":
                x.Estado==CommonEF.Enum.Estado.Pendiente?"Pendiente":
                x.Estado==CommonEF.Enum.Estado.Retirada?"Retirada":
                x.Estado==CommonEF.Enum.Estado.Vencido?"Vencido":"",
                Fecha=x.Fecha,
                Interes=x.Interes.Porcentaje,
                Monto=x.Monto,
                MontoInteres=x.Monto*((double)x.Interes.Porcentaje/(double)100),
                Pendiente=x.MontoPendiente,
                UltimoPago=x.Pagos.Max(p=>p.Fecha),
                Vencimiento=x.FechaVencimiento,                
            });

            dgvEmpeños.DataSource = listEmpeños.ToList();
            dgvEmpeños.Refresh();
            
            chbTodo.Checked = true;
            chbActivos.Checked = false;
            chbPendientes.Checked = false;
            chbPerdidos.Checked = false;
            chbRetirados.Checked = false;
            dtDesde.Value = listEmpeños.Min(l=>l.Fecha).Value;
            dtHasta.Value = DateTime.Today;

            for (int i = 0; i < 5; i++)
            {
                var estado = GetEstadoName(i);
                chartEmpeños.Series[0].Points.AddXY(GetEstadoName(i), listEmpeños.Where(x => x.Estado == estado).Count());
            }
            txtMonto.Text = listEmpeños.Sum(l => l.Monto).ToString("N2");
            txtPendiente.Text = listEmpeños.Sum(l => l.Pendiente).ToString("N2");
            txtOro.Text = listEmpeños.Where(l=>l.Es_Oro).Sum(l => l.Monto).ToString("N2");
            txtArticulos.Text = listEmpeños.Where(l => !l.Es_Oro).Sum(l => l.Monto).ToString("N2");
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
                    return Estado.Activo;
                case 1:
                    return Estado.Pendiente;
                case 2:
                    return Estado.Vencido;
                case 3:
                    return Estado.Cancelada;
                case 4:
                    return Estado.Retirada;
                default:
                    return Estado.Activo;
            }
        }


        private void chbTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTodo.Checked)
            {
                chbRetirados.Checked = false;
                chbPendientes.Checked = false;
                chbPerdidos.Checked = false;
                chbActivos.Checked = false;

                chbTodo.Checked = true;
            }
        }

        private void chbActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbRetiradas_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbCancelada_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
           await Buscar();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }


        public async Task Buscar()
        {
            chartEmpeños.Update();
            DateTime desde = dtDesde.Value;
            DateTime hasta = dtHasta.Value;

            var list = await _context.Empenos.Where(e => e.Fecha >= desde && e.Fecha <= hasta && e.IsDelete==ckbBorrados.Checked).ToListAsync();

            dgvEmpeños.DataSource = null;
            dgvEmpeños.Rows.Clear();
            chartEmpeños.Series[0].Points.Clear();
            if (!chbTodo.Checked)
            {
                if (!chbActivos.Checked)
                    list = list.Where(l => l.Estado != CommonEF.Enum.Estado.Activo).ToList();

                if (!chbPerdidos.Checked)
                    list = list.Where(l => l.Estado != CommonEF.Enum.Estado.Cancelada).ToList();

                if (!chbPendientes.Checked)
                    list = list.Where(l => l.Estado != CommonEF.Enum.Estado.Pendiente).ToList();

                if (!chbRetirados.Checked)
                    list = list.Where(l => l.Estado != CommonEF.Enum.Estado.Retirada).ToList();
            }

            var listEmpeños = list.Select(x => new EmpeñoReporte
            {
                ClienteId = x.ClienteId,
                Identificacion = x.Cliente.Identificacion,
                Cliente = x.Cliente.Nombre,
                Descripcion = x.Descripcion,
                Es_Oro=x.EsOro,
                EmpeñoId = x.EmpenoId,
                Empleado = x.Empleado.Nombre,
                EmpleadoId = x.EmpleadoId,
                Estado = x.Estado == CommonEF.Enum.Estado.Activo ? "Activo" :
               x.Estado == CommonEF.Enum.Estado.Cancelada ? "Cancelada" :
               x.Estado == CommonEF.Enum.Estado.Pendiente ? "Pendiente" :
               x.Estado == CommonEF.Enum.Estado.Retirada ? "Retirada" :
               x.Estado == CommonEF.Enum.Estado.Vencido ? "Vencido" : "",
                Fecha = x.Fecha,
                Interes = x.Interes.Porcentaje,
                Monto = x.Monto,
                MontoInteres = x.Monto * ((double)x.Interes.Porcentaje / (double)100),
                Pendiente = x.MontoPendiente,
                Vencimiento = x.FechaVencimiento,
            });
            for (int i = 0; i < 5; i++)
            {
                var estado = GetEstado(i);
                chartEmpeños.Series[0].Points.AddXY(GetEstadoName(i), list.Where(x => x.Estado == estado 
                && x.Fecha>=desde && x.Fecha <= hasta).Count());
            }
            chartEmpeños.Update();
            dgvEmpeños.DataSource = listEmpeños.ToList();
            dgvEmpeños.Refresh();
            txtMonto.Text = listEmpeños.Sum(l => l.Monto).ToString("N2");
            txtPendiente.Text = listEmpeños.Sum(l => l.Pendiente).ToString("N2");
            txtOro.Text = listEmpeños.Where(l => l.Es_Oro).Sum(l => l.Monto).ToString("N2");
            txtArticulos.Text = listEmpeños.Where(l => !l.Es_Oro).Sum(l => l.Monto).ToString("N2");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void label2_Click(object sender, EventArgs e)
        //{

        //}

        //private void dtHasta_ValueChanged(object sender, EventArgs e)
        //{

        //}

        //private void dtDesde_ValueChanged(object sender, EventArgs e)
        //{

        //}

        //private void label3_Click(object sender, EventArgs e)
        //{

        //}


        private async void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await Buscar();
        }

        private void chbTodo_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbTodo.Checked)
            {
                chbRetirados.Checked = false;
                chbPendientes.Checked = false;
                chbPerdidos.Checked = false;
                chbActivos.Checked = false;

                chbTodo.Checked = true;
            }
        }

        private void chbVencidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbPendientes_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }

        private void chbRetirados_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;

        }

        private void chbPerdidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbActivos.Checked || chbPerdidos.Checked || chbPendientes.Checked || chbRetirados.Checked)
                chbTodo.Checked = false;
        }


        #region RenderSizeForm
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime month = DateTime.Today.AddMonths(-1);
            chbTodo.Checked = true;
            chbActivos.Checked = false;
            chbPendientes.Checked = false;
            chbPerdidos.Checked = false;
            chbRetirados.Checked = false;
            dtDesde.Value = month;
            dtHasta.Value = DateTime.Today;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

       

        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
           // this.panelContenedor.Region = region;
            this.Invalidate();
        }

        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        #endregion

        
    }
}
