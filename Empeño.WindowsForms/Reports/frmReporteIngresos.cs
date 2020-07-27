﻿using Empeño.WindowsForms.Data;
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
            var egresos = await _context.Empenos.Where(x => x.Fecha > month).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha > month).ToListAsync();

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

            ReportDataSource rpd = new ReportDataSource("IngresosEgresosReporte", list.OrderBy(f => f.Fecha));

            rvIngresos.LocalReport.ReportEmbeddedResource = "Empeño.WindowsForms.Reports.IngresosEgresos.rdlc";
            rvIngresos.LocalReport.DataSources.Clear();
            rvIngresos.LocalReport.DataSources.Add(rpd);
            //rvIngresosEgresos.Dock = DockStyle.Fill;
            //rvIngresosEgresos.Width = 800;
            //rvIngresosEgresos.Height = 600;
            this.rvIngresos.RefreshReport();
            chbTodo.Checked = true;
            chbIngresos.Checked = false;
            chbEgresos.Checked = false;
            dtDesde.Value = month;
            dtHasta.Value = DateTime.Today;
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
            DateTime desde = dtDesde.Value;
            DateTime hasta = dtHasta.Value;

            var egresos = await _context.Empenos.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();

            var list = new List<IngresosEgresosReporte>();

            if (chbTodo.Checked || chbIngresos.Checked)
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
                        DiaMes=item.Fecha.Date.ToString("dd/MM"),
                        Identificacion = item.Cliente.Identificacion,
                        Ingresos = null
                    });
                }
            }

            if (chbTodo.Checked || chbEgresos.Checked)
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
                        DiaMes = item.Fecha.Date.ToString("dd/MM"),
                        Identificacion = item.Empeno.Cliente.Identificacion,
                        Ingresos = item.Monto
                    });
                }
            }

            ReportDataSource rpd = new ReportDataSource("IngresosEgresosReporte", list.OrderBy(f => f.Fecha));

            rvIngresos.LocalReport.ReportEmbeddedResource = "Empeño.WindowsForms.Reports.IngresosEgresos.rdlc";
            rvIngresos.LocalReport.DataSources.Clear();
            rvIngresos.LocalReport.DataSources.Add(rpd);
            //rvIngresosEgresos.Dock = DockStyle.Fill;
            //rvIngresosEgresos.Width = 800;
            //rvIngresosEgresos.Height = 600;
            this.rvIngresos.RefreshReport();
            //chbTodo.Checked = true;
            //chbIngresos.Checked = false;
            //chbEgresos.Checked = false;
            this.rvIngresos.RefreshReport();
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
