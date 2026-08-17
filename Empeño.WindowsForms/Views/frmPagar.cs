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
        public int empeñoId = 0;
        public DataContext _context = new DataContext();
        double montoMinimo = 0;
        Funciones.Funciones funciones = new Funciones.Funciones();
        public double valorInteres = 0;
        public frmPagar(int id, double valor=0)
        {
            InitializeComponent();
            empeño = _context.Empenos.Find(id);
            empeñoId = id;
            if (valor > 0)
                valorInteres = valor;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void txtPagaInteres_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPagaInteres.Text))
                {
                    var interes = double.Parse(txtInteresAPagar.Text);

                    var pagaInteres = double.Parse(txtPagaInteres.Text);

                    txtAdeudaIntereses.Text = (interes - pagaInteres).ToString("N2");

                    var pagaMonto = double.Parse(txtPagaMonto.Text);

                    txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");

                    txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtPagaMonto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPagaMonto.Text) || string.IsNullOrEmpty(txtMontoAPagar.Text) || string.IsNullOrEmpty(txtPagaInteres.Text))
                    return;

                var monto = double.Parse(txtMontoAPagar.Text);
                var pagaMonto = double.Parse(txtPagaMonto.Text);
                txtAdeudaMonto.Text = (monto - pagaMonto).ToString("N2");
                var pagaInteres = double.Parse(txtPagaInteres.Text);
                txtTotalAPagar.Text = (pagaInteres + pagaMonto).ToString("N2");
                txtPagaCon.Text = (pagaInteres + pagaMonto).ToString("N2");
            }
            catch (Exception)
            {
            }
        }

        private void txtPagaCon_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPagaCon.Text))
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
            }
            catch (Exception ex)
            {
            }
        }

        private async void btnGuardarEmpeño_Click_1(object sender, EventArgs e)
        {
            await Guardar();
        }

        public async Task Guardar()
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            double pagoIntereses, pagoMonto, montoPendiente;
            if (!double.TryParse(txtPagaInteres.Text, out pagoIntereses) ||
                !double.TryParse(txtPagaMonto.Text, out pagoMonto) ||
                !double.TryParse(txtMontoAPagar.Text, out montoPendiente))
            {
                MessageBox.Show("Los montos ingresados no son válidos.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pagoMonto<montoPendiente)
            {
                if ((pagoMonto > 0) && (empeño.Intereses.Sum(i => i.MontoTotal) > (empeño.Intereses.Sum(i => i.Pagado) + pagoIntereses)))
                {
                    MessageBox.Show("Para abonar a la prenda debe pagar todos los intereses pendientes de " + montoMinimo.ToString("N2"), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } 
            }
            var empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            double montoIntereses;
            if (!double.TryParse(txtInteresAPagar.Text, out montoIntereses))
            {
                MessageBox.Show("El interés a pagar no es válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            
            
            if (pagoMonto>montoPendiente)
            {
                txtPagaMonto.Text = txtMontoAPagar.Text;
                pagoMonto = double.Parse(txtPagaMonto.Text);
            }
            if (pagoIntereses > montoIntereses)
            {
                txtPagaInteres.Text = txtInteresAPagar.Text;
                pagoIntereses = double.Parse(txtPagaInteres.Text);
            }

            empeño = null;
            var empeñoTemp = _context.Empenos.Find(empeñoId);

            if ((pagoMonto > 0 && pagoMonto < montoPendiente) && (pagoIntereses < montoMinimo-1))
            {
                MessageBox.Show("Para abonar a la prenda debe pagar todos los intereses pendientes de " + montoMinimo.ToString("N2"), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pagoMonto > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeñoTemp.EmpenoId,
                    Consecutivo = GetConsecutivo(),
                    Comentario = txtComentario.Text == "Comentario" ? string.Empty : txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = pagoMonto,
                    TipoPago = TipoPago.Principal,
                };

                _context.Pago.Add(pago);
                await _context.SaveChangesAsync();

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(pago),
                    Modulo = "Pagos",
                    Accion = "Crear"
                });

                empeñoTemp.MontoPendiente -= pago.Monto;
                
                if (empeñoTemp.MontoPendiente < 1)
                {
                    var pagoInteres=await SetPagaInteres(pagoIntereses, false);
                    empeñoTemp.Estado = Estado.Cancelado;
                    empeñoTemp.Retirado = true;
                    empeñoTemp.FechaRetiro = DateTime.Today;
                    _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.EmpenoId == empeñoTemp.EmpenoId && i.Pagado == 0));
                    _context.Entry(empeñoTemp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    if (pagoInteres==null)
                    {
                        await PrintRetiro(empeñoTemp, pago);
                    }
                    else
                    {
                        await PrintRetiro(empeñoTemp, pago, pagoInteres);
                    }                    
                }
                else
                {
                    empeñoTemp.Estado = Estado.Vigente;
                    _context.Entry(empeñoTemp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    await PagaInteres(pagoIntereses, true);
                    await PrintAbono(empeñoTemp, pago);                    
                }
            }
            else
            {
                await PagaInteres(pagoIntereses, true);              
            }
         
            await _context.SaveChangesAsync();

            this.Close();
        }

        // Cobro/abono reutilizable desde la versión nueva (frmShell), SIN mostrar el formulario.
        // Replica EXACTO la lógica de Guardar() (mismas guardas, mismo reparto vía PagaInteres/SetPagaInteres,
        // misma cancelación/retiro cuando el capital llega a 0) reusando los métodos existentes; corre el
        // Load headless para poblar los controles que PagaInteres/Print leen. Devuelve { ok, warn } o { ok=false, error }.
        // valorInteres (tope de interés) se pasa por el ctor = pagoIntereses.
        public async Task<object> CobrarHeadless(double pagoIntereses, double pagoMonto, string comentario)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return new { ok = false, error = "Necesita PIN para cobrar." };

            // Poblar el form como el Load (calcula montoMinimo, txtMontoAPagar, txtInteresAPagar, fechas)
            // para que PagaInteres/SetPagaInteres/Print (que leen controles) funcionen headless.
            frmPagar_Load(this, EventArgs.Empty);
            txtComentario.Text = string.IsNullOrEmpty(comentario) ? "Comentario" : comentario;
            txtPagaInteres.Text = pagoIntereses.ToString("N2");
            txtPagaMonto.Text = pagoMonto.ToString("N2");

            double montoPendiente = double.Parse(txtMontoAPagar.Text);
            double montoIntereses = double.Parse(txtInteresAPagar.Text);

            // Guarda de negocio (igual que Guardar): abonar a capital exige pagar TODO el interés pendiente.
            // BLINDADO (fuga de interés): abonar O CANCELAR (pagar todo el capital) exige cubrir TODO el interés
            // pendiente. Antes la guarda solo cubría el abono PARCIAL (pagoMonto < montoPendiente); pagar el capital
            // completo con interés de menos entraba a la rama de cancelación y PERDONABA el interés adeudado.
            if (pagoMonto > 0 && montoMinimo > 1 && pagoIntereses < montoMinimo - 1)
                return new { ok = false, error = "Para abonar o retirar la prenda debe pagar todos los intereses pendientes (₡" + montoMinimo.ToString("N2") + ")." };

            // Clamp igual que Guardar.
            if (pagoMonto > montoPendiente) { pagoMonto = montoPendiente; txtPagaMonto.Text = pagoMonto.ToString("N2"); }
            if (pagoIntereses > montoIntereses) { pagoIntereses = montoIntereses; txtPagaInteres.Text = pagoIntereses.ToString("N2"); }

            if (pagoIntereses <= 0 && pagoMonto <= 0)
                return new { ok = false, error = "Ingresá un pago de interés o un abono a capital." };

            string warn = null;
            empeño = null;
            var empeñoTemp = _context.Empenos.Find(empeñoId);
            // BLINDADO: guardas de servidor (no confiar solo en el front / mensajes stale del WebView).
            if (empeñoTemp == null)
                return new { ok = false, error = "Empeño no encontrado." };
            if (!(empeñoTemp.Estado == Estado.Vigente || empeñoTemp.Estado == Estado.Vencido || empeñoTemp.Estado == Estado.Pendiente))
                return new { ok = false, error = "El empeño no está en un estado cobrable (está " + empeñoTemp.Estado + ")." };
            // BLINDADO: no cobrar MÁS interés que el realmente adeudado en BD (antes el sobrante se descartaba sin asiento).
            double interesRealPendiente = _context.Intereses
                .Where(i => i.EmpenoId == empeñoTemp.EmpenoId && i.Pagado < i.Monto + (i.MontoBodega ?? 0) + (i.MontoAvaluo ?? 0))
                .ToList().Sum(i => i.MontoTotal - i.Pagado);
            if (pagoIntereses > interesRealPendiente + 0.5)
            {
                pagoIntereses = interesRealPendiente < 0 ? 0 : Math.Round(interesRealPendiente, 2);
                txtPagaInteres.Text = pagoIntereses.ToString("N2");
            }

            if (pagoMonto > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeñoTemp.EmpenoId,
                    Consecutivo = GetConsecutivo(),
                    Comentario = txtComentario.Text == "Comentario" ? string.Empty : txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = pagoMonto,
                    TipoPago = TipoPago.Principal,
                };
                _context.Pago.Add(pago);
                await _context.SaveChangesAsync();
                await funciones.SaveBitacora(new ValorBitacora { Valor = JsonConvert.SerializeObject(pago), Modulo = "Pagos", Accion = "Crear" });

                empeñoTemp.MontoPendiente -= pago.Monto;

                if (empeñoTemp.MontoPendiente < 1)
                {
                    Pago pagoInteres = null;
                    try { pagoInteres = await SetPagaInteres(pagoIntereses, false); }
                    catch (Exception ex) { warn = "El pago se registró, pero falló algo al procesar el interés: " + ex.Message; }
                    empeñoTemp.Estado = Estado.Cancelado;
                    empeñoTemp.Retirado = true;
                    empeñoTemp.FechaRetiro = DateTime.Today;
                    _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.EmpenoId == empeñoTemp.EmpenoId && i.Pagado == 0));
                    _context.Entry(empeñoTemp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    try { if (pagoInteres == null) await PrintRetiro(empeñoTemp, pago); else await PrintRetiro(empeñoTemp, pago, pagoInteres); }
                    catch (Exception ex) { warn = "El pago se registró, pero falló la impresión: " + ex.Message; }
                }
                else
                {
                    empeñoTemp.Estado = Estado.Vigente;
                    _context.Entry(empeñoTemp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    try { await PagaInteres(pagoIntereses, true); }
                    catch (Exception ex) { warn = "El pago se registró, pero falló la impresión: " + ex.Message; }
                    try { await PrintAbono(empeñoTemp, pago); }
                    catch (Exception ex) { warn = "El pago se registró, pero falló la impresión: " + ex.Message; }
                }
            }
            else
            {
                try { await PagaInteres(pagoIntereses, true); }
                catch (Exception ex) { warn = "El pago se registró, pero falló la impresión: " + ex.Message; }
            }

            await _context.SaveChangesAsync();
            return new { ok = true, warn };
        }

        private double? GetConsecutivo()
        {
            using (DataContext dataContext= new DataContext())
            {
                if (!dataContext.Pago.Any())
                    return 1;

                if (dataContext.Pago.Where(p => p.Consecutivo != null).Count() == 0)
                    return 1;

                return dataContext.Pago.Max(p => p.Consecutivo)+1;
            }
        }

        public async Task PagaInteres(double pagoIntereses, bool print=true)
        {
            empeño = await _context.Empenos.FindAsync(empeñoId);
            if (pagoIntereses > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Consecutivo = GetConsecutivo(),
                    Comentario = txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = 0,
                    MontoAvaluo = 0,
                    MontoBodega = 0,
                    TipoPago = TipoPago.Interes,
                };

                _context.Pago.Add(pago);
                List<Intereses> intereses = new List<Intereses>();
                // BLINDADO: TODA la escritura del pago va en UNA transacción (auto-rollback si algo falla →
                // nunca queda un pago a medio guardar / en 0). Antes el pago se guardaba en 0 y se actualizaba
                // después, sin transacción; un fallo a mitad dejaba un pago 0 aunque la plata se recibió.
                using (var _tx = _context.Database.BeginTransaction())
                {
                    await _context.SaveChangesAsync();

                    double sobrante = pagoIntereses;
                    double accInteres = 0, accAvaluo = 0, accBodega = 0;
                    var listInteres = await _context.Intereses.Where(i => i.EmpenoId == pago.EmpenoId && i.Pagado < i.Monto + (i.MontoBodega ?? 0) + (i.MontoAvaluo ?? 0)).OrderBy(i => i.FechaVencimiento).ToListAsync();
                    foreach (var item in listInteres)
                    {
                        if (sobrante <= 0)
                            break;

                        double due = item.MontoTotal - item.Pagado;
                        double paga = Math.Min(due, sobrante);

                        // Reparto proporcional del pago entre interés / bodegaje / avalúo de ESTA cuota.
                        double rowAvaluo = item.MontoAvaluo ?? 0;
                        double rowBodega = item.MontoBodega ?? 0;
                        double rowTotal = item.MontoTotal;
                        double fraccion = rowTotal > 0 ? paga / rowTotal : 0;
                        accInteres += Math.Truncate(Math.Round(item.Monto * fraccion));
                        accAvaluo += Math.Truncate(Math.Round(rowAvaluo * fraccion));
                        accBodega += Math.Truncate(Math.Round(rowBodega * fraccion));

                        item.Pagado = Math.Round(item.Pagado + paga, 2);   // redondeo a céntimo — mata el residuo float (2e-13)
                        if (Math.Truncate(Math.Round(item.Pagado)) >= Math.Truncate(item.MontoTotal))
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                            item.Pagado = item.MontoTotal;
                        }
                        item.PagoId = pago.PagoId;
                        sobrante -= paga;
                        _context.Entry(item).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        Intereses valorTemp = new Intereses
                        {
                            EmpenoId = item.EmpenoId,
                            FechaCreacion = item.FechaCreacion,
                            FechaVencimiento = item.FechaVencimiento,
                            InteresesId = item.InteresesId,
                            Monto = item.Monto,
                            MontoAvaluo = item.MontoAvaluo,
                            MontoBodega = item.MontoBodega,
                            Pagado = paga,
                            PagoId = item.PagoId
                        };
                        intereses.Add(valorTemp);
                    }

                    // El resto por redondeo se acumula en el interés para que MontoTotal == lo cobrado.
                    double aplicado = pagoIntereses - sobrante;
                    pago.MontoAvaluo = accAvaluo;
                    pago.MontoBodega = accBodega;
                    pago.Monto = Math.Round(aplicado - accAvaluo - accBodega, 2);   // monto a 2 decimales
                    _context.Entry(pago).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _tx.Commit();
                }

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(pago),
                    Modulo = "Pagos",
                    Accion = "Crear"
                });
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(intereses),
                    Modulo = "Intereses",
                    Accion = "Crear"
                });

                var id = empeño.EmpenoId;
                await funciones.ReviewEmpeño(id);
                empeño = null;
                using (DataContext contextTemp= new DataContext())
                {
                    empeño = await contextTemp.Empenos.FindAsync(id);
                    if (print)
                    {
                        await PrintInteres(empeño, intereses, pago);
                    } 
                }
            }          
        }

        public async Task<Pago> SetPagaInteres(double pagoIntereses, bool print = true)
        {
            empeño = await _context.Empenos.FindAsync(empeñoId);
            if (pagoIntereses > 0)
            {
                var pago = new Pago
                {
                    EmpenoId = empeño.EmpenoId,
                    Consecutivo = GetConsecutivo(),
                    Comentario = txtComentario.Text,
                    EmpleadoId = Program.EmpleadoId,
                    Fecha = DateTime.Now,
                    Monto = 0,
                    MontoAvaluo = 0,
                    MontoBodega = 0,
                    TipoPago = TipoPago.Interes,
                };

                _context.Pago.Add(pago);
                List<Intereses> intereses = new List<Intereses>();
                // BLINDADO: TODA la escritura del pago va en UNA transacción (auto-rollback si algo falla →
                // nunca queda un pago a medio guardar / en 0). Antes el pago se guardaba en 0 y se actualizaba
                // después, sin transacción; un fallo a mitad dejaba un pago 0 aunque la plata se recibió.
                using (var _tx = _context.Database.BeginTransaction())
                {
                    await _context.SaveChangesAsync();

                    double sobrante = pagoIntereses;
                    double accInteres = 0, accAvaluo = 0, accBodega = 0;
                    var listInteres = await _context.Intereses.Where(i => i.EmpenoId == pago.EmpenoId && i.Pagado < i.Monto + (i.MontoBodega ?? 0) + (i.MontoAvaluo ?? 0)).OrderBy(i => i.FechaVencimiento).ToListAsync();
                    foreach (var item in listInteres)
                    {
                        if (sobrante <= 0)
                            break;

                        double due = item.MontoTotal - item.Pagado;
                        double paga = Math.Min(due, sobrante);

                        // Reparto proporcional del pago entre interés / bodegaje / avalúo de ESTA cuota.
                        double rowAvaluo = item.MontoAvaluo ?? 0;
                        double rowBodega = item.MontoBodega ?? 0;
                        double rowTotal = item.MontoTotal;
                        double fraccion = rowTotal > 0 ? paga / rowTotal : 0;
                        accInteres += Math.Truncate(Math.Round(item.Monto * fraccion));
                        accAvaluo += Math.Truncate(Math.Round(rowAvaluo * fraccion));
                        accBodega += Math.Truncate(Math.Round(rowBodega * fraccion));

                        item.Pagado = Math.Round(item.Pagado + paga, 2);   // redondeo a céntimo — mata el residuo float (2e-13)
                        if (Math.Truncate(Math.Round(item.Pagado)) >= Math.Truncate(item.MontoTotal))
                        {
                            empeño.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                            item.Pagado = item.MontoTotal;
                        }
                        item.PagoId = pago.PagoId;
                        sobrante -= paga;
                        _context.Entry(item).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        Intereses valorTemp = new Intereses
                        {
                            EmpenoId = item.EmpenoId,
                            FechaCreacion = item.FechaCreacion,
                            FechaVencimiento = item.FechaVencimiento,
                            InteresesId = item.InteresesId,
                            Monto = item.Monto,
                            MontoAvaluo = item.MontoAvaluo,
                            MontoBodega = item.MontoBodega,
                            Pagado = paga,
                            PagoId = item.PagoId
                        };
                        intereses.Add(valorTemp);
                    }

                    // El resto por redondeo se acumula en el interés para que MontoTotal == lo cobrado.
                    double aplicado = pagoIntereses - sobrante;
                    pago.MontoAvaluo = accAvaluo;
                    pago.MontoBodega = accBodega;
                    pago.Monto = Math.Round(aplicado - accAvaluo - accBodega, 2);   // monto a 2 decimales
                    _context.Entry(pago).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _tx.Commit();
                }

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(pago),
                    Modulo = "Pagos",
                    Accion = "Crear"
                });
                await funciones.SaveBitacora(new ValorBitacora
                {
                    Valor = JsonConvert.SerializeObject(intereses),
                    Modulo = "Intereses",
                    Accion = "Crear"
                });

                var id = empeño.EmpenoId;
                await funciones.ReviewEmpeño(id);
                empeño = null;
                using (DataContext contextTemp = new DataContext())
                {
                    empeño = await contextTemp.Empenos.FindAsync(id);
                    if (print)
                    {
                        await PrintInteres(empeño, intereses, pago);
                    }
                }

                return pago;
            }

            return null;
        }

        private void txtPagaInteres_TextChanged_1(object sender, EventArgs e)
        {
            funciones.KeyNumber(sender);

            if (string.IsNullOrEmpty(txtPagaInteres.Text))
            {
                txtPagaInteres.Text = "0.00";
            }
            try
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
            catch (Exception)
            {
            }
        }

        private void txtPagaMonto_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPagaMonto.Text))
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
            }
            catch (Exception ex)
            {

            }
        }

        #region Funciones
        public async Task PrintAbono(Empeno empeno, Pago pago)
        {
            try
            {
                var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
                Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
                string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteAbono.xlsx";
                cexcel.Workbooks.Open(pathch, true, true);

                cexcel.Visible = false;
                var usuario = await _context.Empleados.FindAsync(Program.EmpleadoId);

                cexcel.Visible = false;
                cexcel.Cells[3, 1].value = configuracion.Compañia;
                cexcel.Cells[4, 1].value = configuracion.Direccion;
                cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
                cexcel.Cells[6, 1].value = configuracion.Nombre;
                cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;

                cexcel.Cells[8, 2].value = pago.Consecutivo;
                cexcel.Cells[9, 2].value = usuario.Nombre;
                cexcel.Cells[10, 2].value = usuario.Usuario;
                cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
                cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
                cexcel.Cells[16, 2].value = pago.Fecha.ToString("dd/MM/yyyy");
                cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

                if (empeno.EsOro)
                {
                    cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
                }
                else
                {
                    cexcel.Cells[19, 1].value = empeno.Descripcion;
                }
                // BLINDADO: montos desde el PAGO/EMPEÑO persistido, NO desde textboxes (que en el flujo
                // WebView headless quedan en 0/blanco). Igual que el gemelo correcto frmEmpeno.PrintAbono.
                cexcel.Cells[23, 3].value = (empeno.MontoPendiente + pago.Monto).ToString("N2");   // saldo anterior
                cexcel.Cells[24, 3].value = pago.Monto.ToString("N2");                              // abono
                cexcel.Cells[25, 3].value = empeno.MontoPendiente.ToString("N2");                   // saldo actual
                cexcel.Cells[27, 3].value = pago.MontoTotal.ToString("N2");                         // total pagado
                cexcel.Cells[29, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
                cexcel.Cells[31, 3].value = empeno.Estado.ToString();
                cexcel.ActiveWindow.SelectedSheets.PrintOut();
                System.Threading.Thread.Sleep(300);
                cexcel.ActiveWorkbook.Close(false);
                cexcel.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir la factura, es probable que el Microsoft Excel Office esta desactivado, por favor contacte con Soporte Técnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task PrintRetiro(Empeno empeno, Pago pago)
        {
            try
            {
                var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
                Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
                string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteCancelacion.xlsx";
                cexcel.Workbooks.Open(pathch, true, true);

                cexcel.Visible = false;
                var usuario = await _context.Empleados.FindAsync(Program.EmpleadoId);

                cexcel.Visible = false;
                cexcel.Cells[3, 1].value = configuracion.Compañia;
                cexcel.Cells[4, 1].value = configuracion.Direccion;
                cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
                cexcel.Cells[6, 1].value = configuracion.Nombre;
                cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;
                cexcel.Cells[8, 2].value = pago.Consecutivo;
                cexcel.Cells[9, 2].value = usuario.Nombre;
                cexcel.Cells[10, 2].value = Program.Usuario.Usuario;
                cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
                cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
                cexcel.Cells[16, 2].value = pago.Fecha.ToString("dd/MM/yyyy");
                cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

                if (empeno.EsOro)
                {
                    cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
                }
                else
                {
                    cexcel.Cells[19, 1].value = empeno.Descripcion;
                }

                // BLINDADO: montos desde datos persistidos, no textboxes (gemelo frmEmpeno.PrintRetiro).
                cexcel.Cells[24, 3].value = _context.Intereses.Where(i => i.EmpenoId == empeno.EmpenoId).Sum(i => i.Monto).ToString("N2");
                // Saldo del empeño ANTES del pago. CobrarHeadless hace `empeñoTemp.MontoPendiente -= pago.Monto` ANTES
                // de llamar acá, así que MontoPendiente ya está en 0. Recomponemos sumando pago.Monto para mostrar el
                // saldo real que el cliente pagó por la prenda (regla del dueño: el saldo del comprobante es lo cobrado).
                cexcel.Cells[25, 3].value = (empeno.MontoPendiente + pago.Monto).ToString("N2");
                cexcel.Cells[26, 3].value = pago.MontoTotal.ToString("N2");
                cexcel.Cells[28, 3].value = "Cancelado";
                cexcel.ActiveWindow.SelectedSheets.PrintOut();
                System.Threading.Thread.Sleep(300);
                cexcel.ActiveWorkbook.Close(false);
                cexcel.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir la factura, es probable que el Microsoft Excel Office esta desactivado, por favor contacte con Soporte Técnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task PrintRetiro(Empeno empeno, Pago pago, Pago pagoInteres)
        {
            try
            {
                var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
                Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();
                string pathch = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                pathch = $"{pathch}\\Empeños\\Comprobantes\\ComprobanteCancelacion.xlsx";
                cexcel.Workbooks.Open(pathch, true, true);

                cexcel.Visible = false;
                var usuario = await _context.Empleados.FindAsync(Program.EmpleadoId);

                cexcel.Visible = false;
                cexcel.Cells[3, 1].value = configuracion.Compañia;
                cexcel.Cells[4, 1].value = configuracion.Direccion;
                cexcel.Cells[5, 1].value = "Tel. " + configuracion.Telefono;
                cexcel.Cells[6, 1].value = configuracion.Nombre;
                cexcel.Cells[7, 1].value = "Cédula: " + configuracion.Identificacion;
                cexcel.Cells[8, 2].value = $"{pago.Consecutivo}, {pagoInteres.Consecutivo}";
                cexcel.Cells[9, 2].value = usuario.Nombre;
                cexcel.Cells[10, 2].value = Program.Usuario.Usuario;
                cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
                cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
                cexcel.Cells[16, 2].value = pago.Fecha.ToString("dd/MM/yyyy");
                cexcel.Cells[17, 2].value = empeno.EmpenoId.ToString();

                if (empeno.EsOro)
                {
                    cexcel.Cells[19, 1].value = "ORO : " + empeno.Descripcion;
                }
                else
                {
                    cexcel.Cells[19, 1].value = empeno.Descripcion;
                }

                // BLINDADO: desde el pago de capital y el pago de interés reales, no textboxes.
                cexcel.Cells[24, 3].value = pagoInteres.MontoTotal.ToString("N2");
                // Saldo del empeño ANTES del pago (mismo criterio que el overload sin pagoInteres): MontoPendiente aquí
                // ya está en 0 tras el descuento, así que sumamos pago.Monto (capital pagado) para mostrar lo cobrado.
                cexcel.Cells[25, 3].value = (empeno.MontoPendiente + pago.Monto).ToString("N2");
                cexcel.Cells[26, 3].value = (pago.MontoTotal + pagoInteres.MontoTotal).ToString("N2");
                cexcel.Cells[28, 3].value = "Cancelado";
                cexcel.ActiveWindow.SelectedSheets.PrintOut();
                System.Threading.Thread.Sleep(300);
                cexcel.ActiveWorkbook.Close(false);
                cexcel.Quit();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al imprimir la factura, es probable que el Microsoft Excel Office esta desactivado, por favor contacte con Soporte Técnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task PrintInteres(Empeno empeno, List<Intereses> intereses, Pago pago)
        {
            try
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

                var empleado = await _context.Empleados.FindAsync(Program.EmpleadoId);
                cexcel.Cells[8, 2].value = pago.Consecutivo;
                cexcel.Cells[9, 2].value = empleado.Nombre;
                cexcel.Cells[10, 2].value = empleado.Usuario;
                cexcel.Cells[14, 2].value = empeno.Cliente.Identificacion;
                cexcel.Cells[15, 1].value = empeno.Cliente.Nombre;
                cexcel.Cells[16, 2].value = pago.Fecha.ToString("dd/MM/yyyy");
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
                    if (item.Pagado>=1)
                    {
                        cexcel.Cells[26 + index, 1].value = Program.Meses(item.FechaVencimiento.Month);
                        cexcel.Cells[26 + index, 3].value = item.Pagado.ToString("N2");

                        Microsoft.Office.Interop.Excel.Worksheet ws = cexcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                        Range line = (Range)cexcel.Rows[27 + index];
                        line.Insert();
                        ++index;
                        ws.get_Range("A" + (26 + index), "B" + (26 + index)).Merge();
                        ws.get_Range("C" + (26 + index), "D" + (26 + index)).Merge();
                    }
                    

                }

                cexcel.Cells[28 + index, 3].value = pago.MontoTotal.ToString("N2");   // BLINDADO: total real del pago de interés (Monto+Avalúo+Bodegaje), no el textbox
                cexcel.Cells[30 + index, 3].value = empeno.FechaVencimiento.ToString("dd/MM/yyyy");
                cexcel.Cells[32 + index, 3].value = empeno.Estado.ToString();

                cexcel.ActiveWindow.SelectedSheets.PrintOut();
                System.Threading.Thread.Sleep(300);
                cexcel.ActiveWorkbook.Close(false);
                cexcel.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir la factura, es probable que el Microsoft Excel Office esta desactivado, por favor contacte con Soporte Técnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void txtPagaMonto_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPagaMonto.Text))
                txtPagaMonto.Text = "0.00";

            double number = double.Parse(txtPagaMonto.Text);
            txtPagaMonto.Text = (number).ToString("N2");
            txtPagaCon.Focus();
        }

        private void txtPagaInteres_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPagaMonto.Text))
                txtPagaMonto.Text = "0.00";

            double number = double.Parse(txtPagaInteres.Text);
            txtPagaInteres.Text = (number).ToString("N2");
        }

        private async void txtPagaInteres_KeyUp_1(object sender, KeyEventArgs e)
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

        private async void txtPagaMonto_KeyUp_1(object sender, KeyEventArgs e)
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

        private async void txtPagaCon_KeyUp_1(object sender, KeyEventArgs e)
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

        private void frmPagar_Load(object sender, EventArgs e)
        {
            double interes = 0;
            if (valorInteres>0)
            {
                interes = valorInteres;
            }
            else
            {
                // Sin selección: se cobra SOLO la cuota más antigua pendiente (no todo el pendiente).
                // Para pagar más meses, el usuario debe seleccionar más filas en la tabla.
                var masAntigua = empeño.Intereses.Where(i => i.MontoTotal > i.Pagado).OrderBy(i => i.FechaVencimiento).FirstOrDefault();
                interes = masAntigua != null ? (masAntigua.MontoTotal - masAntigua.Pagado) : 0;
            }
            montoMinimo= empeño.Intereses.Where(i => i.MontoTotal > i.Pagado).Sum(i => i.MontoTotal - i.Pagado);
            var intereses = interes.ToString("N2");
            txtInteresAPagar.Text = intereses;
            txtPagaInteres.Text = txtInteresAPagar.Text;
            txtAdeudaIntereses.Text = (double.Parse(intereses) - double.Parse(txtInteresAPagar.Text)).ToString("N2");
            txtAdeudaMonto.Text = empeño.MontoPendiente.ToString("N2");
            var ultimoInteres = empeño.Intereses.OrderByDescending(o => o.InteresesId).FirstOrDefault();
            if (ultimoInteres != null)
                txtProximaFecha.Text = ultimoInteres.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy");

            txtMontoAPagar.Text = empeño.MontoPendiente.ToString("N2");

            if (interes < 1 || montoMinimo < 1)
            {
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
            if (txtInteresAPagar.Text == "0.00")
            {
                txtPagaMonto.Focus();
                txtPagaMonto.Text = empeño.MontoPendiente.ToString("N2");
            }
            else
            {
                txtPagaInteres.Focus();
            }
        }

        private void txtAdeudaIntereses_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
