using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            var intereses= empeño.Intereses.Sum(i => i.Monto - i.Pagado).ToString("N2");
            txtInteresAPagar.Text = intereses;
            txtPagaInteres.Text = txtInteresAPagar.Text;                        
            txtAdeudaIntereses.Text = (double.Parse(intereses) - double.Parse(txtInteresAPagar.Text)).ToString("N2");
            txtAdeudaMonto.Text = empeño.MontoPendiente.ToString("N2");
            var ultimoInteres = empeño.Intereses.OrderByDescending(o => o.InteresesId).FirstOrDefault();
            if (ultimoInteres!=null)
                txtProximaFecha.Text =ultimoInteres.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy");

            txtMontoAPagar.Text = empeño.MontoPendiente.ToString("N2");

            montoMinimo = empeño.Intereses.Where(i=>i.FechaVencimiento<=DateTime.Today).Sum(i => i.Monto - i.Pagado);            

            if (double.Parse(intereses)<1 || montoMinimo<1)
            {
                txtPagaMonto.Enabled = true;
                txtPagaMonto.Text = txtMontoAPagar.Text;
                txtTotalAPagar.Text = txtPagaMonto.Text;
                txtPagaCon.Text = txtPagaMonto.Text;
                txtAdeudaMonto.Text = (double.Parse(txtMontoAPagar.Text) - double.Parse(txtPagaMonto.Text)).ToString("N2");
            }
            else
            {
                txtTotalAPagar.Text = txtPagaInteres.Text;
                txtPagaCon.Text = txtPagaInteres.Text;
            }
            
            txtFechaVencimiento.Text = empeño.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy");
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

            if (double.Parse(txtPagaInteres.Text)<=montoMinimo && (double.Parse(txtPagaMonto.Text) == double.Parse(txtMontoAPagar.Text)))
            {
                MessageBox.Show("Debe cumplir con un pago minimo mayor a " + montoMinimo.ToString("N2") + " cólon", "Información");
                return;
            }
            var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            var pagoIntereses = double.Parse(txtPagaInteres.Text);
            var pagoMonto = double.Parse(txtPagaMonto.Text);
            if (pagoIntereses>0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Comentario = txtComentario.Text,
                    EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario),
                    Fecha = DateTime.Now,
                    Monto =pagoIntereses,
                    TipoPago=TipoPago.Interes,
                };

                _context.Pago.Add(pago);
                var sobrante = pago.Monto;
                foreach (var item in _context.Intereses.Where(i=>i.EmpenoId==pago.EmpenoId && i.Pagado<i.Monto).ToList())
                {
                    if ((item.Monto-item.Pagado)>sobrante)
                    {
                        item.Pagado += sobrante;
                        if (item.Pagado==item.Monto)
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                        }
                        break;
                    }
                    else
                    {
                        item.Pagado += (item.Monto - item.Pagado);
                        if (item.Pagado == item.Monto)
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                        }
                        sobrante -= (item.Monto - item.Pagado);
                    }
                }
            }
            if (pagoMonto>0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Comentario = txtComentario.Text=="Comentario"?string.Empty: txtComentario.Text,
                    EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario),
                    Fecha = DateTime.Now,
                    Monto = pagoMonto,
                    TipoPago = TipoPago.Principal,
                };

                _context.Pago.Add(pago);
                empeño.MontoPendiente -= pago.Monto;

                if (empeño.MontoPendiente < 1)
                { 
                    empeño.Estado = Estado.Retirada;
                    _context.Intereses.RemoveRange(empeño.Intereses.Where(i => i.Pagado == 0));
                }
            }
         
            await _context.SaveChangesAsync();
          
            MessageBox.Show("Pago recibido");
            this.Close();
        }

        private void txtPagaInteres_TextChanged_1(object sender, EventArgs e)
        {
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
            var monto = double.Parse(txtMontoAPagar.Text);

            var pagaMonto = double.Parse(txtPagaMonto.Text);

            txtAdeudaMonto.Text = (monto - pagaMonto).ToString("N2");

            var pagaInteres = double.Parse(txtPagaInteres.Text);

            txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

            txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");
        }
    }
}
