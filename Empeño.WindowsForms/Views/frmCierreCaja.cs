using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Funciones;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmCierreCaja : Form
    {
        List<DetalleCierreCaja> detalles = new List<DetalleCierreCaja>();
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        int empleadoId=0;
        public frmCierreCaja()
        {
            InitializeComponent();
        }

        private void panelCierreInformacion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelCierreInformacion_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            var detalle = new DetalleCierreCaja();

            if (txtConcepto.Text!=string.Empty || txtValor.Text != string.Empty)
            {
                txtValor.Text = txtValor.Text == string.Empty? "0" : txtValor.Text;
                detalle.Concepto = txtConcepto.Text;
                detalle.Valor = double.Parse(txtValor.Text);
                detalles.Add(detalle);
                LoadList();
                txtConcepto.Focus();
            }
        }

        public void LoadList()
        {
            dgvDetalles.DataSource = null;
            textBox1.Text = string.IsNullOrEmpty(textBox1.Text) ? "0.00" : textBox1.Text;
            
            dgvDetalles.DataSource = detalles.Select(x=>new 
            {
                x.Concepto,
               Valor= x.Valor.ToString("N2"),
            }).ToList();
            dgvDetalles.Refresh();           
            txtTotal.Text = ((double.Parse(textBox1.Text) * -1) + detalles.Sum(d => d.Valor)).ToString("N2");
        }

        private async void frmCierreCaja_Load(object sender, EventArgs e)
        {
            try
            {
                //Dura demasiado ejecutando, solo lo vamos a dejar al inicio
                //funciones.ReviewEmpeños();
                txtFecha.Value = DateTime.Today;
                await ProcessClose();
                lblEspere.Visible = false;
            }
            catch (Exception)
            {

            }
        }



        public async Task ProcessClose()
        {            
            var fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var tomorrow = fecha.AddDays(1);
            empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            empleadoId = empleadoId == null ? 1 : empleadoId;
            var empleado = await _context.Empleados.FindAsync(empleadoId);
            txtEmpleado.Text = empleado.Nombre;
            txtEmpleado.Enabled = false;
            textBox1.Text = "0.00";

            var empeñosActivos = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                       || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido));
           

            double? vencidos = _context.Empenos.Where(x => !x.IsDelete && x.FechaRetiroAdministrador >= fecha && x.FechaRetiroAdministrador < tomorrow).ToList().Sum(x => x.MontoPendiente);

            double c1 = empeñosActivos.Where(x => x.Fecha < fecha
                     && (!x.Retirado || (x.FechaRetiroAdministrador >= fecha && x.FechaRetiroAdministrador<tomorrow))).Sum(x => x.MontoPendiente);

            double c2c3 = _context.Pago.Where(p => p.Fecha >= fecha && p.TipoPago == TipoPago.Principal).Any() ?
                _context.Pago.Where(p => p.Fecha >= fecha && p.TipoPago == TipoPago.Principal).Sum(x=>x.Monto)
                : 0;

            double acumuladoInicial = (c1 + (vencidos == null?0: vencidos.Value)) + c2c3;

            //double acumuladoInicial = c1 + c2c3;


            txtAcumuladoInicial.Text = acumuladoInicial.ToString("N2");

           
            double? montoEmpeñoDia = empeñosActivos.Where(x => !x.IsDelete && x.Fecha >= fecha && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            txtMonto.Text = montoEmpeñoDia != null ? montoEmpeñoDia.Value.ToString("N2") : "0.00";

            double? montoInteresDia = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                       || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado==Estado.Cancelado))
                  .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= fecha && x.Fecha < tomorrow).ToList().Sum(x => x.MontoTotal);

            txtInteres.Text = montoInteresDia != null ? montoInteresDia.Value.ToString("N2") : "0.00";

            double? abonoDia = empeñosActivos
                .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Principal && x.Fecha >=fecha && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            txtAbonos.Text = abonoDia != null ? abonoDia.Value.ToString("N2") : "0.00";   

            txtVencimientos.Text = vencidos != null ? vencidos.Value.ToString("N2") : "0.00";

            double? cancelados = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Cancelado
                     || x.Retirado || x.FechaRetiro != null))
              .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Principal && x.Fecha >= fecha && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            txtCancelados.Text = cancelados != null ? cancelados.Value.ToString("N2") : "0.00";
            txtAcumulado.Text = ((acumuladoInicial + montoEmpeñoDia) - (abonoDia + vencidos + cancelados)).Value.ToString("N2");

            double? montoAvaluoDia = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                      || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado == Estado.Cancelado))
                 .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= fecha && x.Fecha < tomorrow).ToList().Sum(x => x.MontoAvaluo);

            txtAvaluo.Text = montoAvaluoDia != null ? montoAvaluoDia.Value.ToString("N2") : "0.00";

            double? montoBodegajeDia = _context.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                     || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado == Estado.Cancelado))
                .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= fecha && x.Fecha < tomorrow).ToList().Sum(x => x.MontoBodega);

            txtBodegaje.Text = montoBodegajeDia != null ? montoBodegajeDia.Value.ToString("N2") : "0.00";

            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();

            double? IVA= ((double)(montoAvaluoDia + montoBodegajeDia) * (double)(configuracion.IVA / 100));

            txtIVA.Text = IVA.Value.ToString("N2");

            detalles.Add(
              new DetalleCierreCaja
              {
                  Concepto = "Empeños",
                  Valor = montoEmpeñoDia != null ? montoEmpeñoDia.Value : 0
              });

            detalles.Add(
            new DetalleCierreCaja
                {
                    Concepto = "Monto de Abonos",
                    Valor = abonoDia != null ? abonoDia.Value : 0
                });

            detalles.Add(
               new DetalleCierreCaja
               {
                   Concepto = "Intereses",
                   Valor = montoInteresDia != null ? montoInteresDia.Value : 0
               });

            detalles.Add(
              new DetalleCierreCaja
              {
                  Concepto = "Avalúos",
                  Valor = montoAvaluoDia != null ? montoAvaluoDia.Value : 0
              });

            detalles.Add(
              new DetalleCierreCaja
              {
                  Concepto = "Bodegajes",
                  Valor = montoBodegajeDia != null ? montoBodegajeDia.Value : 0
              });

            detalles.Add(
               new DetalleCierreCaja
               {
                   Concepto = "Retiros",
                   Valor = cancelados != null ? cancelados.Value : 0
               });

            detalles.Add(
               new DetalleCierreCaja
               {
                   Concepto = "Vencidos",
                   Valor = vencidos != null ? vencidos.Value : 0
               });

            detalles.Add(
               new DetalleCierreCaja
               {
                   Concepto = "Acumulado",
                   Valor = ((acumuladoInicial + montoEmpeñoDia) - (abonoDia + vencidos + cancelados)).Value
               });

            detalles.Add(
               new DetalleCierreCaja
               {
                   Concepto = "IVA",
                   Valor = IVA.Value
               });

            LoadList();
        }


        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (txtConcepto.Text!=string.Empty)
            {
            }
        }

        private async void btnGuardarCierreCaja_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            var fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            var cierreCaja = new CierreCaja
            {
                Fecha = fecha,
                EmpleadoId = Program.EmpleadoId,
                SaldoInicial=double.Parse(textBox1.Text),
                IsDelete = false,                
            };

            _context.CierreCajas.Add(cierreCaja);
            await _context.SaveChangesAsync();

            detalles.ForEach(d => d.CierreCajaId = cierreCaja.CierreCajaId);
            _context.DetalleCierreCajas.AddRange(detalles);
            await _context.SaveChangesAsync();

            MessageBox.Show("Cierre de caja realizado correctamente!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Print(cierreCaja);

            var configuracion = _context.Configuraciones.FirstOrDefault();
            if (!string.IsNullOrEmpty(configuracion.EmailNotification))
            {
                EmailFuncion emailFuncion = new EmailFuncion();
                var empleado = await _context.Empleados.FindAsync(Program.EmpleadoId);
                string str = "Se ha realizado el cierre de caja al ser el <b>" + cierreCaja.Fecha.ToLongDateString() + " " + cierreCaja.Fecha.ToLongTimeString() + "</b> por <b>" + empleado.Nombre + "</b>. <br /><br />";
                await emailFuncion.SendMail(configuracion.EmailNotification, "Cierre de Caja " + cierreCaja.Fecha, str, detalles);
            }
            //printDocument1.Print();
           // MessageBox.Show("Imprimiendo...");
        }

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    this.Dock = DockStyle.None;
        //    this.Width = 800;
        //    this.Height = 600;
        //    Bitmap bm = new Bitmap(800, 600);
        //    this.DrawToBitmap(bm, new Rectangle(0, 0, 800, 600));
        //    e.Graphics.DrawImage(bm, 0, 0);
        //    this.Dock = DockStyle.Fill;
        //}

        private void btnCancelarCierreCaja_Click(object sender, EventArgs e)
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.Rows.Clear();
            dgvDetalles.Refresh();
            txtConcepto.Text = "";
            txtValor.Text = "0";
        }

        private void btnDeleteCierreCaja_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            txtValor.Text = "0";
        }

        private async void btnDeleteDetalle_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show("Esta seguro que desea borrar los datos", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp==DialogResult.No)
            {
                return;
            }

            if (dgvDetalles.SelectedRows.Count>0)
            {
                var index=dgvDetalles.SelectedRows[0].Index;

                var detalle= detalles[index];

                detalles.Remove(detalle);
                LoadList();
            }
        }

        #region Funciones
        public async void Print(CierreCaja cierreCaja)
        {

            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();

            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteCierreCaja.xlsx";
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
                cexcel.Cells[14 + index, 1].value = item.Cells[1].Value.ToString();
                cexcel.Cells[14 + index, 2].value = item.Cells[2].Value.ToString();
                cexcel.Cells[14 + index, 3].value = item.Cells[3].Value.ToString();
                cexcel.Cells[14 + index, 4].value = item.Cells[4].Value.ToString();     

                Range range = (Range)cexcel.Rows[15 + index];
                Range line = range;
                line.Insert();
                ++index;              
            }

            double saldoInicial = double.Parse(textBox1.Text);
            cexcel.Cells[17 + index, 3].value = saldoInicial.ToString("N2");
            cexcel.Cells[19 + index, 3].value = txtTotal.Text;
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
        #endregion

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text== string.Empty)
            {
                textBox1.Text = "0";
            }
            double number;
            double.TryParse(textBox1.Text,out number);
            if (number>0)
            {
                txtTotal.Text = ((double.Parse(textBox1.Text) * -1) + detalles.Sum(d => d.Valor)).ToString("N2");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "0.00";
            }
            double numero = double.Parse(textBox1.Text);
            textBox1.Text = numero.ToString("N2");
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValor.Text))
            {
                txtValor.Text = "0.00";
            }
            double numero = double.Parse(txtValor.Text);
            txtValor.Text = numero.ToString("N2");
        }

        private void txtAcumuladoInicial_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var detalle = new DetalleCierreCaja();

            if (txtConcepto.Text != string.Empty || txtValor.Text != string.Empty)
            {
                txtValor.Text = txtValor.Text == string.Empty ? "0" : txtValor.Text;
                detalle.Concepto = txtConcepto.Text;
                detalle.Valor = double.Parse(txtValor.Text);
                detalles.Add(detalle);
                LoadList();
                txtConcepto.Focus();
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                lblEspere.Visible = true;
                await ProcessClose();
                lblEspere.Visible = false;
            }
            catch (Exception)
            {

               
            }
            lblEspere.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

    

        private async void btnPrint_Click(object sender, EventArgs e)
        {

            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
            string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathch = $"{pathch}\\Empeños\\Comprobantes\\Cierre.xlsx";
            cexcel.Workbooks.Open(pathch, true, true);

            cexcel.Visible = false;
            cexcel.Cells[2, 1].value = $"Empeños y Venta       {configuracion.Compañia}";
            cexcel.Cells[3, 3].value = txtFecha.Value.ToShortDateString();
            cexcel.Cells[5, 3].value = txtAcumuladoInicial.Text;
            cexcel.Cells[6, 3].value = txtMonto.Text;
            cexcel.Cells[8, 3].value = txtAvaluo.Text;
            cexcel.Cells[10, 3].value = txtBodegaje.Text;
            cexcel.Cells[12, 3].value = txtInteres.Text;
            cexcel.Cells[14, 3].value = txtAbonos.Text;
            cexcel.Cells[16, 3].value = txtVencimientos.Text;
            cexcel.Cells[18, 3].value = txtCancelados.Text;
            cexcel.Cells[20, 3].value = txtAcumulado.Text;
            cexcel.Cells[22, 3].value = txtIVA.Text;
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }

        private async void btnEnviarCierre_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            var fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var cierreCaja = new CierreCaja
            {
                Fecha = fecha,
                EmpleadoId = Program.EmpleadoId,
                SaldoInicial = double.Parse(textBox1.Text),
                IsDelete = false,
            };

            _context.CierreCajas.Add(cierreCaja);
            await _context.SaveChangesAsync();

            detalles.ForEach(d => d.CierreCajaId = cierreCaja.CierreCajaId);
            _context.DetalleCierreCajas.AddRange(detalles);
            await _context.SaveChangesAsync();                       

            var configuracion = _context.Configuraciones.FirstOrDefault();
            if (!string.IsNullOrEmpty(configuracion.EmailNotification))
            {
                EmailFuncion emailFuncion = new EmailFuncion();
                var empleado = await _context.Empleados.FindAsync(Program.EmpleadoId);
                string str = "Se ha realizado el cierre de caja al ser el <b>" + cierreCaja.Fecha.ToLongDateString() + " " + cierreCaja.Fecha.ToLongTimeString() + "</b> por <b>" + empleado.Nombre + "</b>. <br /><br />";
                await emailFuncion.SendMail(configuracion.EmailNotification, "Cierre de Caja " + cierreCaja.Fecha, str, detalles);
                
                MessageBox.Show("Mensaje enviado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteDetalle_Click_1(object sender, EventArgs e)
        {
            var resp = MessageBox.Show("Esta seguro que desea borrar los datos", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.No)
            {
                return;
            }

            if (dgvDetalles.SelectedRows.Count > 0)
            {
                var index = dgvDetalles.SelectedRows[0].Index;

                var detalle = detalles[index];

                detalles.Remove(detalle);
                LoadList();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            txtValor.Text = "0";
        }
    }
}
