using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
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

namespace Empeño.WindowsForms.Views
{
    public partial class frmPagar : Form
    {
        public Empeno empeño = new Empeno();
        public DataContext _context = new DataContext();
        double montoMinimo = 0;
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmPagar(int id)
        {
            InitializeComponent();
            empeño = _context.Empenos.Find(id);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var interes = empeño.Intereses.Sum(i => i.Monto - i.Pagado);
            var intereses= interes.ToString("N2");
            txtInteresAPagar.Text = intereses;
            txtPagaInteres.Text = txtInteresAPagar.Text;                        
            txtAdeudaIntereses.Text = (double.Parse(intereses) - double.Parse(txtInteresAPagar.Text)).ToString("N2");
            txtAdeudaMonto.Text = empeño.MontoPendiente.ToString("N2");
            var ultimoInteres = empeño.Intereses.OrderByDescending(o => o.InteresesId).FirstOrDefault();
            if (ultimoInteres!=null)
                txtProximaFecha.Text =ultimoInteres.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy");

            txtMontoAPagar.Text = empeño.MontoPendiente.ToString("N2");

            montoMinimo = empeño.Intereses.Where(i=>i.FechaVencimiento<=DateTime.Today).Sum(i => i.Monto - i.Pagado);            

            if (interes<1 || montoMinimo<1)
            {
                txtPagaMonto.Enabled = true;
                txtPagaMonto.Text = "0.00";
                txtTotalAPagar.Text = intereses;
                txtPagaCon.Text = txtTotalAPagar.Text;
                txtAdeudaMonto.Text = (double.Parse(txtMontoAPagar.Text) - double.Parse(txtPagaMonto.Text)).ToString("N2");
            }
            else
            {
                txtPagaMonto.Text = "0.00";
                txtTotalAPagar.Text = txtPagaInteres.Text;
                txtPagaCon.Text = txtPagaInteres.Text;
            }
            
            txtFechaVencimiento.Text = empeño.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy");
            if (txtInteresAPagar.Text=="0.00")
            {
                txtPagaMonto.Focus();
                txtPagaMonto.Text = empeño.MontoPendiente.ToString("N2");
            }
            else
            {
                txtPagaInteres.Focus();
            }
        }

        private void txtPagaInteres_TextChanged(object sender, EventArgs e)
        {
            var interes = double.Parse(txtInteresAPagar.Text);

            var pagaInteres= double.Parse(txtPagaInteres.Text);

            txtAdeudaIntereses.Text = (interes - pagaInteres).ToString("N2");

            var pagaMonto = double.Parse(txtPagaMonto.Text);

            txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

            txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");

            if (pagaInteres>=montoMinimo)
            {
                txtPagaMonto.Enabled = true;
            }
            else
            {
                txtPagaMonto.Enabled = false;
                txtPagaMonto.Text = "0.00";
            }
        }

        private void txtPagaMonto_TextChanged(object sender, EventArgs e)
        {
            var monto = double.Parse(txtMontoAPagar.Text);

            var pagaMonto = double.Parse(txtPagaMonto.Text);

            txtAdeudaMonto.Text = (monto - pagaMonto).ToString("N2");

            var pagaInteres = double.Parse(txtPagaInteres.Text);

            txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

            txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");
        }

        private void txtPagaCon_TextChanged(object sender, EventArgs e)
        {
            funciones.KeyNumber(sender);

            if (string.IsNullOrEmpty(txtPagaCon.Text))
            {
                txtPagaCon.Text = "0.00";
            }
            else
            {
                txtPagaCon.Text = txtPagaCon.Text;
            }
           
            var monto = double.Parse(txtTotalAPagar.Text);

            var pagaMonto = double.Parse(txtPagaCon.Text);

            txtVuelto.Text = (pagaMonto - monto).ToString("N2");
        }

        private async void btnGuardarEmpeño_Click(object sender, EventArgs e)
        {
            await Guardar();
        }

        public async Task Guardar()
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            if (montoMinimo != 0)
            {
                if (double.Parse(txtPagaInteres.Text) != double.Parse(txtInteresAPagar.Text) && double.Parse(txtPagaInteres.Text) <= montoMinimo && (double.Parse(txtPagaMonto.Text) == double.Parse(txtMontoAPagar.Text)))
                {
                    MessageBox.Show("Debe cumplir con un pago minimo mayor a " + montoMinimo.ToString("N2") + " cólon", "Información");
                    return;
                }
            }

            var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            double montoIntereses = double.Parse(txtInteresAPagar.Text);
            double pagoIntereses = double.Parse(txtPagaInteres.Text);
            double pagoMonto = double.Parse(txtPagaMonto.Text);
            double montoPendiente = double.Parse(txtMontoAPagar.Text);
            if ((pagoMonto >= montoPendiente) && (pagoIntereses < montoMinimo))
            {
                MessageBox.Show("Para retirar la prenda debe pagar un minimo de intereses de " + montoMinimo.ToString("N2"), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pagoIntereses > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Comentario = txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = pagoIntereses,
                    TipoPago = TipoPago.Interes,
                };

                _context.Pago.Add(pago);
                await _context.SaveChangesAsync();
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(pago),
                    Modulo = "Pagos",
                    Accion = "Crear"
                });
                List<Intereses> intereses = new List<Intereses>();
                var sobrante = pago.Monto;
                var listInteres = await _context.Intereses.Where(i => i.EmpenoId == pago.EmpenoId && i.Pagado < i.Monto).ToListAsync();
                foreach (var item in listInteres)
                {
                    if ((item.Monto - item.Pagado) > sobrante && sobrante > 0)
                    {
                        item.Pagado += sobrante;
                        if (item.Pagado == item.Monto)
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                        }

                        intereses.Add(item);
                        _context.Entry(item).State = EntityState.Modified;
                        break;
                    }
                    else if (sobrante > 0)
                    {
                        double paga = (item.Monto - item.Pagado);
                        item.Pagado += paga;
                        if (item.Pagado == item.Monto)
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                        }
                        item.PagoId = pago.PagoId;
                        sobrante -= paga;
                        intereses.Add(item);
                        _context.Entry(item).State = EntityState.Modified;
                    }
                }
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(intereses),
                    Modulo = "Intereses",
                    Accion = "Crear"
                });
                await PrintInteres(empeño, intereses, pago);
            }
            if (pagoMonto > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Comentario = txtComentario.Text == "Comentario" ? string.Empty : txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = pagoMonto,
                    TipoPago = TipoPago.Principal,
                };

                _context.Pago.Add(pago);
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(pago),
                    Modulo = "Pagos",
                    Accion = "Crear"
                });
                empeño.MontoPendiente -= pago.Monto;

                if (empeño.MontoPendiente < 1)
                {
                    empeño.Estado = Estado.Retirada;
                    _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.EmpenoId == empleadoId && i.Pagado == 0));
                    await PrintRetiro(empeño, pago);
                }
                else
                {
                    await PrintAbono(empeño, pago);
                }
            }

            await _context.SaveChangesAsync();

            MessageBox.Show("Pago recibido");
            this.Close();
        }

        private void txtPagaInteres_TextChanged_1(object sender, EventArgs e)
        {
            funciones.KeyNumber(sender);

            if (string.IsNullOrEmpty(txtPagaInteres.Text))
            {
                txtPagaInteres.Text = "0.00";
            }
            var interes = double.Parse(txtInteresAPagar.Text);

            var pagaInteres = double.Parse(txtPagaInteres.Text);

            txtAdeudaIntereses.Text = (interes - pagaInteres).ToString("N2");

            var pagaMonto = double.Parse(txtPagaMonto.Text);

            txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

            txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");

            if (pagaInteres >= montoMinimo)
            {
                txtPagaMonto.Enabled = true;
            }
            else
            {
                txtPagaMonto.Enabled = false;
                txtPagaMonto.Text = "0.00";
            }
        }

        private void txtPagaMonto_TextChanged_1(object sender, EventArgs e)
        {
            funciones.KeyNumber(sender);

            if (string.IsNullOrEmpty(txtPagaMonto.Text))
            {
                txtPagaMonto.Text = "0.00";
            }

            var monto = double.Parse(txtMontoAPagar.Text);

            var pagaMonto = double.Parse(txtPagaMonto.Text);

            txtAdeudaMonto.Text = (monto - pagaMonto).ToString("N2");

            var pagaInteres = double.Parse(txtPagaInteres.Text);

            txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

            txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");
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
            var usuario = _context.Empleados.Find(Program.EmpleadoId);

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
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha.ToString("dd/MM/yyyy");
            cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

            if (empeno.EsOro)
            {
                cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
            }
            else
            {
                cexcel.Cells[19, 1].value = empeno.Descripcion;
            }
            cexcel.Cells[23, 3].value =  txtMontoAPagar.Text;
            cexcel.Cells[24, 3].value = txtPagaMonto.Text;
            cexcel.Cells[25, 3].value = txtAdeudaMonto.Text;
            cexcel.Cells[27, 3].value = txtPagaMonto.Text;
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
            var usuario = _context.Empleados.Find(Program.EmpleadoId);

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
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha.ToString("dd/MM/yyyy");
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
            cexcel.Cells[25, 3].value = txtAdeudaMonto.Text;
            cexcel.Cells[26, 3].value = txtTotalAPagar.Text;
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

            var empleado = _context.Empleados.Find(Program.EmpleadoId);
            cexcel.Cells[8, 2].value = pago.PagoId;
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;
            cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
            cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
            cexcel.Cells[16, 2].value = empeno.Cliente.Fecha.ToString("dd/MM/yyyy");
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

            cexcel.Cells[28 + index, 3].value =txtPagaInteres.Text;
            cexcel.Cells[30 + index, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
            cexcel.Cells[32 + index, 3].value = empeno.Estado.ToString();
           
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }

        #endregion

        private void txtPagaMonto_Leave(object sender, EventArgs e)
        {
            double number = double.Parse(txtPagaMonto.Text);
            txtPagaMonto.Text = (number).ToString("N2");
            txtPagaCon.Focus();
        }

        private void txtPagaInteres_Leave(object sender, EventArgs e)
        {

            double number = double.Parse(txtPagaInteres.Text);
            txtPagaInteres.Text = (number).ToString("N2");
        }

        private async void txtPagaInteres_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtPagaMonto.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                await Guardar();
            }
        }

        private async void txtPagaMonto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtPagaCon.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                await Guardar();
            }
        }

        private async void txtPagaCon_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                btnGuardarEmpeño.Focus();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                await Guardar();
            }
        }
    }
}
