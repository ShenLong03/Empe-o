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
    public partial class frmReporteIngresos : Form
    {
        public int xClick = 0, yClick = 0;
        int lx, ly;
        int sw, sh;
        DataContext _context = new DataContext();
        public frmReporteIngresos()
        {
            InitializeComponent();
        }

        private async void ReporteIngresos_Load(object sender, EventArgs e)
        {
            DateTime month = DateTime.Today.AddMonths(-1);
            var egresos = await _context.Empenos.Where(x =>!x.IsDelete && x.Fecha > month).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha > month && !x.Empeno.IsDelete).ToListAsync();

            var list = new List<IngresosEgresosReporte>();

            foreach (var item in egresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Cliente.Nombre,
                    Egresos = item.Monto,
                    Tipo = "Empeño",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Cliente.Identificacion,
                    Ingresos = null
                });
            }

            foreach (var item in ingresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Empeno.Cliente.Nombre,
                    Egresos = null,
                    Tipo = item.TipoPago==CommonEF.Enum.TipoPago.Interes?"Interes":
                    item.TipoPago==CommonEF.Enum.TipoPago.Principal?"Principal":"",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Empeno.Cliente.Identificacion,
                    Ingresos = item.Monto
                });
            }

            dgvIngresos.DataSource = list.ToList();
            dgvIngresos.Refresh();
            chbTodo.Checked = true;
            chbIngresos.Checked = false;
            chbEgresos.Checked = false;
            dtDesde.Value = month;
            dtHasta.Value = DateTime.Today;

            list = list.OrderBy(l => l.Fecha).ToList();
            var minDate = list.Count() > 0 ? list.Min(l => l.Fecha) : DateTime.Today;
            var maxDate = list.Count() > 0 ? list.Max(l => l.Fecha).AddHours(23).AddMinutes(59) : DateTime.Today.AddHours(23).AddMinutes(59);

            while (minDate < maxDate)
            {
                var endDate = minDate.AddHours(23).AddMinutes(59);
                chartVentas.Series[0].Points.AddXY(minDate.ToString("dd/MM"), list.Where(l => l.Fecha >= minDate && l.Fecha <= endDate).Sum(i => i.Ingresos));
                chartVentas.Series[1].Points.AddXY(minDate.ToString("dd/MM"), list.Where(l => l.Fecha >= minDate && l.Fecha <= endDate).Sum(i => i.Egresos));
                minDate = minDate.AddDays(1);
            }

            txtMonto.Text = list.Sum(l => l.Ingresos).Value.ToString("N2");
            txtPendiente.Text = list.Sum(l => l.Egresos).Value.ToString("N2");
        }


        #region RenderSizeForm
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            Program.GetCargando(Screen.PrimaryScreen.WorkingArea.Size, Screen.PrimaryScreen.WorkingArea.Location);

            lx = this.Location.X != lx ? this.Location.X : lx;
            ly = this.Location.Y != ly ? this.Location.Y : ly;
            sw = this.Size.Width != sw ? this.Size.Width : sw;
            sh = this.Size.Height != sh ? this.Size.Height : sh;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            Program.CargandoClose();
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


        public async Task Buscar()
        {
            chartVentas.Update();
            DateTime desde = dtDesde.Value;
            DateTime hasta = dtHasta.Value.AddHours(23).AddMinutes(59);

            var egresos = await _context.Empenos.Where(x => !x.IsDelete && x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha >= desde && x.Fecha <= hasta && !x.Empeno.IsDelete).ToListAsync();         

            var list = new List<IngresosEgresosReporte>();

            if (chbTodo.Checked || chbIngresos.Checked)
            {
                foreach (var item in ingresos)
                {
                    list.Add(new IngresosEgresosReporte
                    {
                        EmpeñoId = item.EmpenoId,
                        Cliente = item.Empeno.Cliente.Nombre,
                        Egresos = null,
                        Tipo = item.TipoPago == CommonEF.Enum.TipoPago.Interes ? "Interes" :
                        item.TipoPago == CommonEF.Enum.TipoPago.Principal ? "Principal" : "",                        
                        Empleado = item.Empleado.Nombre,
                        Fecha = item.Fecha.Date,
                        DiaMes=item.Fecha.Date.ToString("dd/MM"),
                        Identificacion = item.Empeno.Cliente.Identificacion,
                        Ingresos = item.Monto
                    });
                }
            }

            if (chbTodo.Checked || chbEgresos.Checked)
            {
                foreach (var item in egresos)
                {
                    list.Add(new IngresosEgresosReporte
                    {
                        EmpeñoId = item.EmpenoId,
                        Cliente = item.Cliente.Nombre,
                        Egresos = item.Monto,
                        Tipo = "Empeño",
                        Empleado = item.Empleado.Nombre,
                        Fecha = item.Fecha.Date,
                        DiaMes = item.Fecha.Date.ToString("dd/MM"),
                        Identificacion = item.Cliente.Identificacion,
                        Ingresos = null
                    });
                }
            }

            chartVentas.Series[0].Points.Clear();
            chartVentas.Series[1].Points.Clear();
            dgvIngresos.DataSource = list.ToList();
            dgvIngresos.Refresh();
            list = list.OrderBy(l => l.Fecha).ToList();
            var minDate = list.Count() > 0 ? list.Min(l => l.Fecha) : DateTime.Today;
            var maxDate = list.Count() > 0 ? list.Max(l => l.Fecha).AddHours(23).AddMinutes(59) : DateTime.Today.AddHours(23).AddMinutes(59);

            while (minDate < maxDate)
            {
                var endDate = minDate.AddHours(23).AddMinutes(59);
                chartVentas.Series[0].Points.AddXY(minDate.ToString("dd/MM"), list.Where(l => l.Fecha >= minDate && l.Fecha <= endDate).Sum(i => i.Ingresos));
                chartVentas.Series[1].Points.AddXY(minDate.ToString("dd/MM"), list.Where(l => l.Fecha >= minDate && l.Fecha <= endDate).Sum(i => i.Egresos));
                minDate = minDate.AddDays(1);
            }

            txtMonto.Text = list.Sum(l => l.Ingresos).Value.ToString("N2");
            txtPendiente.Text = list.Sum(l => l.Egresos).Value.ToString("N2");

        } 

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Program.GetCargando(this.Size, this.Location);

            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);


            Program.CargandoClose();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Program.GetCargando(Screen.PrimaryScreen.WorkingArea.Size, Screen.PrimaryScreen.WorkingArea.Location);

            lx = this.Location.X != lx ? this.Location.X : lx;
            ly = this.Location.Y != ly ? this.Location.Y : ly;
            sw = this.Size.Width != sw ? this.Size.Width : sw;
            sh = this.Size.Height != sh ? this.Size.Height : sh;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;


            Program.CargandoClose();
        }

        private void btnMaximize2_Click(object sender, EventArgs e)
        {
            Program.GetCargando(Screen.PrimaryScreen.WorkingArea.Size, Screen.PrimaryScreen.WorkingArea.Location);

            lx = this.Location.X != lx ? this.Location.X : lx;
            ly = this.Location.Y != ly ? this.Location.Y : ly;
            sw = this.Size.Width != sw ? this.Size.Width : sw;
            sh = this.Size.Height != sh ? this.Size.Height : sh;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;


            Program.CargandoClose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DateTime month = DateTime.Today.AddMonths(-1);
            chbTodo.Checked = true;
            chbIngresos.Checked = false;
            chbEgresos.Checked = false;
            dtDesde.Value = month;
            dtHasta.Value = DateTime.Today;
        }

        private void chbIngresos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbIngresos.Checked)
                chbTodo.Checked = false;
        }

        private void chbTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTodo.Checked)
            {
                chbEgresos.Checked = false;
                chbIngresos.Checked = false;

                chbTodo.Checked = true;
            }
        }

        private void chbEgresos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEgresos.Checked)
                chbTodo.Checked = false;
        }


    }
}
