using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Views;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Empeño.WindowsForms.Funciones
{
    public class Funciones:IDisposable
    {
        private bool disposedValue;

        public void PlaceHolder(TextBox textBox, PlaceHolderType type, string placeHolder)
        {
            switch (type)
            {
                case PlaceHolderType.Leave:
                    if (textBox.Text == "")
                    {
                        textBox.Text = placeHolder;
                        textBox.ForeColor = Color.LightGray;
                    }
                    break;
                case PlaceHolderType.Enter:
                    if (textBox.Text == placeHolder)
                    {
                        textBox.Text = "";
                        textBox.ForeColor = Color.Black;
                    }
                    break;
                default:
                    break;
            }           
        }

        public void PlaceHolder(TextBox textBox, Label label ,PlaceHolderType type, string placeHolder)
        {
            switch (type)
            {
                case PlaceHolderType.Leave:
                    if (textBox.Text == "")
                    {
                        textBox.Text = placeHolder;
                        textBox.ForeColor = Color.LightGray;
                    }
                    break;
                case PlaceHolderType.Enter:
                    if (textBox.Text == placeHolder)
                    {
                        textBox.Text = string.Empty;
                        textBox.ForeColor = Color.Black;
                    }
                    break;
                default:
                    break;
            }
            ShowLabelName(textBox, label);
        }

        public void PlaceHolder(ComboBox textBox, Label label, PlaceHolderType type, string placeHolder)
        {
            switch (type)
            {
                case PlaceHolderType.Leave:
                    if (textBox.Text == "")
                    {
                        textBox.Text = placeHolder;
                        textBox.ForeColor = Color.LightGray;
                    }
                    break;
                case PlaceHolderType.Enter:
                    if (textBox.Text == placeHolder)
                    {
                        textBox.Text = string.Empty;
                        textBox.ForeColor = Color.Black;
                    }
                    break;
                default:
                    break;
            }
            ShowLabelName(textBox, label);
        }

        public void PlaceHolder(TextBox textBox, Label label, PlaceHolderType type, string placeHolder, bool isPassword)
        {
            switch (type)
            {
                case PlaceHolderType.Leave:
                    if (textBox.Text == "")
                    {
                        textBox.Text = placeHolder;
                        textBox.ForeColor = Color.LightGray;
                        if (isPassword)                        
                            textBox.UseSystemPasswordChar = false;
                    }
                    break;
                case PlaceHolderType.Enter:
                    if (textBox.Text == placeHolder)
                    {
                        textBox.Text = string.Empty;
                        textBox.ForeColor = Color.Black;
                        if (isPassword)
                            textBox.UseSystemPasswordChar = true;
                    }
                    break;
                default:
                    break;
            }
            ShowLabelName(textBox, label);
        }

        public void ShowLabelName(TextBox textBox, Label label)
        {
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text != label.Text)
            {
                label.Visible = true;
            }
            else
            {
                label.Visible = false;

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = label.Text;
                }
            }
        }

        public void ShowLabelName(ComboBox comboBox, Label label)
        {
            if (string.IsNullOrEmpty(comboBox.Text) || comboBox.Text != label.Text)
            {
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
                comboBox.Text = label.Text;
            }
        }

        public async Task<int> GetEmpleadoIdByUser(string user)
        {
            try
            {
                using (DataContext _context=new DataContext())
                {
                    var empleado = await _context.Empleados.SingleOrDefaultAsync(e => e.Usuario == user);
                    if (empleado != null)
                    {
                        return empleado.EmpleadoId;
                    }
                    return 1; 
                }
            }
            catch (Exception ex)
            {

                return 1;
            }
        }

        public async Task<Empleado> GetEmpleadoByUser(string user)
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var empleado = await _context.Empleados.SingleOrDefaultAsync(e => e.Usuario == user);
                    if (empleado != null)
                    {
                        return empleado;
                    }
                    return null; 
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<int> GetInteresIdByNombre(string nombre)
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var interes = await _context.Interes.SingleOrDefaultAsync(e => e.Nombre == nombre);
                    if (interes != null)
                    {
                        return interes.InteresId;
                    }
                    return 0; 
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public void ClearTextBoxs(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.Text = string.Empty;
                    textBox.UseSystemPasswordChar = false;
                }
            }
        }

        public void EditTextColor(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.ForeColor = Color.Black;
                }
                else if (item is ComboBox)
                {
                    var comboBox = (ComboBox)item;
                    comboBox.ForeColor = Color.Black;
                }
            }
        }

        public void BlockTextColor(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.ForeColor = Color.LightGray;
                }

                if (item is ComboBox)
                {
                    var comboBox = (ComboBox)item;
                    comboBox.ForeColor = Color.LightGray;
                }
            }
        }

        public void HideLabels(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is Label)
                {
                    var label = (Label)item;
                    if (label.Name.StartsWith("lbl"))
                    {
                        label.Visible = false;
                    }                    
                }
            }
        }

        public void ShowLabels(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is Label)
                {
                    var label = (Label)item;
                    label.Visible = true;
                }
            }
        }

        public void TextBoxColorBlank(Panel panel) 
        {
            foreach (var itemLabel in panel.Controls)
            {
                if (itemLabel is Label)
                {
                    var label = (Label)itemLabel;
                    var labelText = label.Text;

                    foreach (var itemTextBox in panel.Controls)
                    {
                        if (itemTextBox is TextBox)
                        {
                            var textBox = (TextBox)itemTextBox;
                            var textBoxText = textBox.Text;
                            if (labelText == textBoxText)
                            {
                                textBox.ForeColor = Color.LightGray;
                            }
                        }
                        else if (itemTextBox is ComboBox)
                        {
                            var comboBox = (ComboBox)itemTextBox;
                            var textBoxText = comboBox.Text;
                            if (labelText == textBoxText)
                            {
                                comboBox.ForeColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
        }

        public void GetPlaceHolders(Panel panel)
        {
            foreach (var itemLabel in panel.Controls)
            {
                if (itemLabel is Label)
                {
                    var label = (Label)itemLabel;
                    var nombreLabel = label.Name.Replace("lbl", string.Empty);

                    foreach (var itemTextBox in panel.Controls)
                    {
                        if (itemTextBox is TextBox)
                        {
                            var textBox = (TextBox)itemTextBox;
                            var nombreTextBox = textBox.Name.Replace("txt", string.Empty);
                            if (nombreLabel==nombreTextBox)
                            {
                                textBox.Text = label.Text;
                            }
                        }
                    }
                }
            }
        }

        public void IntelligHolders(Panel panel)
        {
            foreach (var itemLabel in panel.Controls)
            {
                if (itemLabel is Label)
                {
                    var label = (Label)itemLabel;
                    var nombreLabel = label.Name.Replace("lbl", string.Empty);

                    foreach (var itemTextBox in panel.Controls)
                    {
                        if (itemTextBox is TextBox)
                        {
                            var textBox = (TextBox)itemTextBox;
                            var nombreTextBox = textBox.Name.Replace("txt", string.Empty);
                            if (nombreLabel == nombreTextBox)
                            {
                                if (textBox.Text==label.Text || string.IsNullOrEmpty(textBox.Text))
                                {
                                    label.Visible = false;
                                    textBox.ForeColor = Color.LightGray;

                                    textBox.Text = label.Text;
                                }
                                else
                                {
                                    label.Visible = true;
                                    textBox.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ResetForm(Panel panel) 
        {
            BlockTextBox(panel, true);
            ClearTextBoxs(panel);
            HideLabels(panel);
            GetPlaceHolders(panel);
            BlockTextColor(panel);
        }

        public void BlockTextBox(Panel panel, bool block)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.Enabled = block;               
                    textBox.ForeColor = block ? Color.Black : Color.LightGray;
                }
                else if (item is ComboBox)
                {
                    var comboBox = (ComboBox)item;
                    comboBox.Enabled = block;
                    comboBox.ForeColor = block ? Color.Black : Color.LightGray;
                }
            }
        }

        public async Task ReviewDuplicateEmpeños()
        {
            using (DataContext _context = new DataContext())
            {
                var empeños = await _context.Empenos.Where(w => !w.IsDelete && (w.Estado == Estado.Vigente
                     || w.Estado == Estado.Pendiente
                     || w.Estado == Estado.Vencido)).ToListAsync();

                if (empeños.Count > 0)
                {
                    foreach (var empeño in empeños)
                    {

                        var intereses = await _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).ToListAsync();

                        foreach (var item in intereses)
                        {
                            _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.InteresesId > item.InteresesId && i.EmpenoId == item.EmpenoId && i.FechaVencimiento == item.FechaVencimiento));
                        }
                    }
                    await _context.SaveChangesAsync();
                } 
            }
        }

        public async Task ReviewDuplicateEmpeños(int empeñoId)
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var intereses = await _context.Intereses.Where(i => i.EmpenoId == empeñoId).ToListAsync();

                    foreach (var item in intereses)
                    {
                        _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.InteresesId > item.InteresesId && i.EmpenoId == item.EmpenoId && i.FechaVencimiento == item.FechaVencimiento));
                    }
                    var proximoMes = DateTime.Today.AddMonths(1).AddDays(1);
                    _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.EmpenoId == empeñoId && i.FechaVencimiento > proximoMes && i.Pagado <= 0));
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        // Resultado del barrido de intereses. NO es una entidad: no se mapea, no toca el modelo EF.
        public class ReviewResumen
        {
            public int EmpeñosRevisados { get; set; }
            public int CuotasCreadas { get; set; }
            public int EmpeñosConError { get; set; }
            public bool Fallo { get; set; }          // true si el catch exterior atrapó algo o EmpeñosConError > 0
            public double Segundos { get; set; }
        }

        // Avance del barrido: sirve para que el usuario vea QUÉ se está haciendo, no sólo un
        // porcentaje corriendo. Tampoco es una entidad.
        public class ReviewProgreso
        {
            public int Porcentaje { get; set; }
            public int Procesados { get; set; }
            public int Total { get; set; }
            public int CuotasCreadas { get; set; }
            public string Etapa { get; set; }
        }

        // ===== Reparacion de intereses retroactivos ==========================================
        // Los datos vienen migrados de otro sistema y SOLO se paso parte del historial. El barrido
        // anterior rellenaba TODOS los meses faltantes desde la apertura del empeno, asi que invento
        // cuotas de anos que el cliente ya habia saldado en el sistema viejo: el sistema quedo
        // cobrando de mas. Este proceso borra unicamente esa basura.
        //
        // Reglas duras, las tres se cumplen a la vez o la cuota NO se toca:
        //   1. La cuota no tiene ni un colon pagado (Pagado <= 0 y sin PagoId).
        //   2. Vence ANTES del ultimo interes que el cliente realmente pago.
        //   3. Se creo mas de SEIS meses DESPUES de su propio vencimiento: una cuota legitima nace
        //      cuando arranca su periodo, y aun cuando el local tarde en abrir la aplicacion el atraso
        //      se mide en semanas. Medido contra el respaldo real: lo legitimo llega a 3 meses de
        //      hueco y lo inventado arranca en 8, asi que 6 cae en tierra de nadie.
        // Nunca borra pagos. Nunca borra nada posterior al ultimo interes pagado.
        public class ReparacionResumen
        {
            public int EmpeñosTocados { get; set; }
            public int CuotasEliminadas { get; set; }
            public double MontoLiberado { get; set; }
            public bool YaSeHabiaCorrido { get; set; }
            public bool Fallo { get; set; }
            public string Detalle { get; set; }
        }

        // Marca en Bitacora: la reparacion corre UNA sola vez por base de datos.
        // V2: la V1 escribia la marca aunque no borrara nada, asi que una corrida fallida
        // bloqueaba el reintento para siempre. Al subir la version la reparacion vuelve a correr.
        public const string MarcaReparacion = "REPARACION-INTERESES-RETROACTIVOS-V2";

        public async Task<ReparacionResumen> RepararInteresesRetroactivos(bool soloSimular = false)
        {
            var r = new ReparacionResumen();
            var lineas = new List<string>();
            try
            {
                using (DataContext _context = new DataContext())
                {
                    if (!soloSimular && await _context.Bitacoras.AnyAsync(b => b.Mensaje == MarcaReparacion))
                    {
                        r.YaSeHabiaCorrido = true;
                        return r;
                    }

                    var empeños = await _context.Empenos
                        .Where(w => !w.IsDelete && (w.Estado == Estado.Vigente
                              || w.Estado == Estado.Pendiente
                              || w.Estado == Estado.Vencido))
                        .Select(w => w.EmpenoId)
                        .ToListAsync();

                    foreach (var empeñoId in empeños)
                    {
                        var cuotas = await _context.Intereses
                            .Where(i => i.EmpenoId == empeñoId)
                            .ToListAsync();
                        if (cuotas.Count == 0)
                            continue;

                        // Piso: el ultimo interes que el cliente REALMENTE pago. Sin un solo pago no
                        // hay referencia de hasta donde estaba al dia, y sin referencia no se borra nada.
                        var pagadas = cuotas.Where(i => i.Pagado > 0).ToList();
                        if (pagadas.Count == 0)
                            continue;
                        DateTime ultimoPagado = pagadas.Max(i => i.FechaVencimiento);

                        var basura = cuotas.Where(i => i.Pagado <= 0
                                                    && (i.PagoId == null || i.PagoId == 0)
                                                    && i.FechaVencimiento < ultimoPagado
                                                    && i.FechaCreacion > i.FechaVencimiento.AddMonths(6))
                                           .ToList();
                        if (basura.Count == 0)
                            continue;

                        r.EmpeñosTocados++;
                        r.CuotasEliminadas += basura.Count;
                        r.MontoLiberado += basura.Sum(i => i.MontoTotal);
                        lineas.Add("#" + empeñoId + ": " + basura.Count + " cuotas del "
                            + basura.Min(i => i.FechaVencimiento).ToString("dd/MM/yyyy") + " al "
                            + basura.Max(i => i.FechaVencimiento).ToString("dd/MM/yyyy")
                            + " (ultimo interes pagado " + ultimoPagado.ToString("dd/MM/yyyy") + ")");

                        if (!soloSimular)
                            _context.Intereses.RemoveRange(basura);
                    }

                    r.Detalle = string.Join(" | ", lineas);

                    if (!soloSimular && r.CuotasEliminadas > 0)
                        await _context.SaveChangesAsync();
                }

                if (!soloSimular)
                {
                    await SaveBitacora(new ValorBitacora
                    {
                        Modulo = "Intereses",
                        Accion = "Reparacion de intereses retroactivos",
                        Valor = r.EmpeñosTocados + " empenos, " + r.CuotasEliminadas + " cuotas eliminadas, "
                              + r.MontoLiberado.ToString("N2") + " liberados. " + r.Detalle
                    }, 0, MarcaReparacion);
                }
            }
            catch (Exception ex)
            {
                r.Fallo = true;
                r.Detalle = ex.Message;
                await SaveBitacora(new ValorBitacora
                {
                    Modulo = "Intereses",
                    Accion = "Reparacion de intereses retroactivos",
                    Valor = ex.ToString()
                }, 1, "Fallo la reparacion de intereses retroactivos");
            }
            return r;
        }
        public async Task<ReviewResumen> ReviewEmpeños(IProgress<ReviewProgreso> progreso = null)
        {
            var resumen = new ReviewResumen();
            var cronometro = Stopwatch.StartNew();
            try
            {
                // La consulta inicial sobre una cartera grande no es instantánea: sin este aviso el
                // anillo se queda en 0% sin explicar nada.
                progreso?.Report(new ReviewProgreso { Etapa = "Consultando la cartera" });
                using (DataContext _context = new DataContext())
                {
                    var empeños = await _context.Empenos.Where(w => !w.IsDelete && (w.Estado == Estado.Vigente
                          || w.Estado == Estado.Pendiente
                          || w.Estado == Estado.Vencido)).ToListAsync();

                    if (empeños.Count > 0)
                    {
                        int total = empeños.Count;
                        int procesados = 0;
                        int ultimoPct = -1;
                        progreso?.Report(new ReviewProgreso { Total = total, Etapa = "Actualizando intereses" });
                        foreach (var empeño in empeños)
                        {
                            try
                            {
                                if (!empeño.IsDelete && (empeño.Estado == Estado.Vigente || empeño.Estado == Estado.Vencido
                                        || empeño.Estado == Estado.Pendiente))
                                {
                                    // Generación mes a mes (calendario). El cursor avanza SIEMPRE, así una
                                    // cuota ya existente no estanca la generación de las siguientes.
                                    // Avalúo NO se acumula mensual (cargo único); bodegaje SÍ es mensual.
                                    await _context.Entry(empeño).Collection(e => e.Intereses).LoadAsync();
                                    // Ancla FIJA en la fecha de apertura del empeno. Cada cuota k se calcula como
                                    // Fecha.AddMonths(k), NUNCA encadenando desde la cuota anterior: encadenar pierde el dia
                                    // de corte para siempre al pasar por un mes corto (31/01 -> 28/02 -> 28/03 -> 28/04...).
                                    // Calculando desde el ancla, febrero se recorta pero marzo vuelve al 31.
                                    // El duplicado se evita por ANO+MES, no por fecha exacta: las cuotas que ya estan en la
                                    // base pueden tener el dia derivado y no coincidirian con el dia correcto recalculado.
                                    // La cuota k se crea cuando ARRANCA su periodo (Fecha.AddMonths(k-1)), no cuando vence.
                                    // El tope de 600 meses es una guarda: una Fecha corrupta no puede colgar el barrido.
                                    var mesesExistentes = new HashSet<int>(empeño.Intereses
                                        .Select(i => i.FechaVencimiento.Year * 12 + i.FechaVencimiento.Month));
                                    
                                    // PISO: el interes nuevo SOLO corre DESPUES del ultimo interes que ya existe, este
                                    // pagado o no. Muchos empenos vienen migrados de otro sistema y su historial viejo no
                                    // esta en esta base: rellenar hacia atras inventaba meses que el cliente nunca debio y
                                    // cobraba de mas. Sin cuotas previas no hay piso y la generacion arranca en Fecha.
                                    int pisoMes = empeño.Intereses.Any()
                                        ? empeño.Intereses.Max(i => i.FechaVencimiento.Year * 12 + i.FechaVencimiento.Month)
                                        : 0;
                                    
                                    for (int k = 1; k <= 600 && empeño.Fecha.AddMonths(k - 1).Date <= DateTime.Today; k++)
                                    {
                                        DateTime proxima = empeño.Fecha.AddMonths(k);
                                        int claveMes = proxima.Year * 12 + proxima.Month;
                                        if (claveMes <= pisoMes)
                                            continue;
                                        if (mesesExistentes.Contains(claveMes))
                                            continue;
                                    
                                        // Sin saldo pendiente no hay interes que cobrar. Una cuota en CERO se daria por pagada
                                        // sola (0 >= 0), adelantaria un mes el vencimiento del empeno y saldria una factura en
                                        // cero: no debe crearse.
                                        double montoCuota = Math.Truncate((double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100));
                                        double bodegaCuota = empeño.Interes.Bodegaje != null ? Math.Truncate((double)empeño.MontoPendiente * (double)empeño.Interes.PorcentajeBodegaje) : 0;
                                        if (montoCuota + bodegaCuota <= 0)
                                        continue;
                                    
                                        var intereses = new Intereses
                                        {
                                            EmpenoId = empeño.EmpenoId,
                                            FechaCreacion = DateTime.Now,
                                            Monto = montoCuota,
                                            MontoBodega = bodegaCuota,
                                            FechaVencimiento = proxima
                                        };
                                        _context.Intereses.Add(intereses);
                                        mesesExistentes.Add(claveMes);   // el guard sigue valido SIN SaveChanges
                                        resumen.CuotasCreadas++;
                                    }
                                    await _context.SaveChangesAsync();
                                }

                                var count = await _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).ToListAsync();
                                if (count.Count() > 0)
                                {
                                    var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId)
                                                .OrderByDescending(o => o.InteresesId)
                                                .FirstOrDefaultAsync();
                                    if (ultimoInteres != null)
                                    {
                                        Estado estadoAnterior = empeño.Estado;
                                        // Ahora que la cuota del mes en curso se crea al ARRANCAR el periodo, la ultima cuota
                                        // siempre vence a futuro. Mirar solo la ultima daria Vigente siempre y taparia la
                                        // mora, asi que se evalua si hay CUALQUIER cuota ya vencida y sin pagar.
                                        // 'count' ya viene materializado, por eso se puede usar MontoTotal (propiedad calculada).
                                        if (count.Any(i => i.FechaVencimiento < DateTime.Today
                                            && Math.Truncate(Math.Round(i.Pagado)) < Math.Truncate(i.MontoTotal)))
                                        {
                                            empeño.Estado = Estado.Pendiente;
                                        }
                                        else
                                        {
                                            empeño.Estado = Estado.Vigente;
                                        }
                                        if (empeño.Estado != estadoAnterior)
                                        {
                                            _context.Entry(empeño).State = EntityState.Modified;
                                        }
                                        await _context.SaveChangesAsync();
                                    }

                                }
                                if (empeño.FechaVencimiento < DateTime.Today)
                                {
                                    if (empeño.Retirado || empeño.FechaRetiro != null)
                                    {
                                        empeño.Estado = Estado.Cancelado;
                                    }
                                    else if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador != null)
                                    {
                                        empeño.Estado = Estado.Retirado;
                                    }
                                    else
                                    {
                                        empeño.Estado = Estado.Vencido;
                                    }

                                    await _context.SaveChangesAsync();
                                }

                                resumen.EmpeñosRevisados++;

                                // Soltar el grafo de ESTE empeño: sin esto el change tracker acumula TODA la
                                // cartera y DetectChanges se vuelve O(n²) en un barrido largo.
                                foreach (var it in empeño.Intereses.ToList()) _context.Entry(it).State = EntityState.Detached;
                                _context.Entry(empeño).State = EntityState.Detached;
                            }
                            catch (Exception exEmpeño)
                            {
                                // Un empeño con datos inconsistentes (p.ej. Interes null) NO debe abortar
                                // la revisión de los demás. Se registra y se continúa.
                                resumen.EmpeñosConError++;
                                await SaveBitacora(new ValorBitacora
                                {
                                    Valor = "Error al revisar el empeño " + empeño.EmpenoId,
                                    Modulo = "Revisar Empeños",
                                    Accion = "Error"
                                }, 1, exEmpeño.Message);
                            }
                            procesados++;
                            int pct = procesados * 100 / total;
                            // Se informa sólo cuando el porcentaje CAMBIA: en una cartera grande, avisar
                            // por cada empeño serían cientos de llamadas seguidas al WebView.
                            if (pct != ultimoPct)
                            {
                                ultimoPct = pct;
                                progreso?.Report(new ReviewProgreso
                                {
                                    Porcentaje = pct,
                                    Procesados = procesados,
                                    Total = total,
                                    CuotasCreadas = resumen.CuotasCreadas,
                                    Etapa = "Actualizando intereses"
                                });
                            }
                        }
                    }
                    cronometro.Stop();
                    resumen.Segundos = cronometro.Elapsed.TotalSeconds;
                    resumen.Fallo = resumen.EmpeñosConError > 0;
                    await SaveBitacora(new ValorBitacora
                    {
                        Valor = "Revisión Automática de Empeños — revisados: " + resumen.EmpeñosRevisados
                              + ", cuotas creadas: " + resumen.CuotasCreadas
                              + ", con error: " + resumen.EmpeñosConError
                              + ", duración: " + resumen.Segundos.ToString("N1") + "s",
                        Modulo = "Revisar Empeños",
                        Accion = "Automatico"
                    });
                }
            }
            catch (Exception ex)
            {
                // Antes: catch VACÍO. Un fallo FUERA del bucle (la consulta inicial, la conexión) dejaba la
                // revisión completa sin correr y SIN NINGÚN rastro. SaveBitacora abre su propio contexto.
                resumen.Fallo = true;
                await SaveBitacora(new ValorBitacora
                {
                    Valor = "Error en la revisión automática de empeños",
                    Modulo = "Revisar Empeños",
                    Accion = "Error"
                }, 1, ex.Message);
            }
            return resumen;
        }


        public async Task ReviewEmpeño(int id)
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var empeño = await _context.Empenos.FindAsync(id);

                    if (empeño != null)
                    {
                        if (!empeño.IsDelete && (empeño.Estado == Estado.Vigente || empeño.Estado == Estado.Vencido
                                || empeño.Estado == Estado.Pendiente))
                        {
                            // Ancla FIJA en la fecha de apertura del empeno. Cada cuota k se calcula como
                            // Fecha.AddMonths(k), NUNCA encadenando desde la cuota anterior: encadenar pierde el dia
                            // de corte para siempre al pasar por un mes corto (31/01 -> 28/02 -> 28/03 -> 28/04...).
                            // Calculando desde el ancla, febrero se recorta pero marzo vuelve al 31.
                            // El duplicado se evita por ANO+MES, no por fecha exacta: las cuotas que ya estan en la
                            // base pueden tener el dia derivado y no coincidirian con el dia correcto recalculado.
                            // La cuota k se crea cuando ARRANCA su periodo (Fecha.AddMonths(k-1)), no cuando vence.
                            // El tope de 600 meses es una guarda: una Fecha corrupta no puede colgar el barrido.
                            var mesesExistentes = new HashSet<int>(empeño.Intereses
                                .Select(i => i.FechaVencimiento.Year * 12 + i.FechaVencimiento.Month));
                            
                            // PISO: el interes nuevo SOLO corre DESPUES del ultimo interes que ya existe, este
                            // pagado o no. Muchos empenos vienen migrados de otro sistema y su historial viejo no
                            // esta en esta base: rellenar hacia atras inventaba meses que el cliente nunca debio y
                            // cobraba de mas. Sin cuotas previas no hay piso y la generacion arranca en Fecha.
                            int pisoMes = empeño.Intereses.Any()
                                ? empeño.Intereses.Max(i => i.FechaVencimiento.Year * 12 + i.FechaVencimiento.Month)
                                : 0;
                            
                            for (int k = 1; k <= 600 && empeño.Fecha.AddMonths(k - 1).Date <= DateTime.Today; k++)
                            {
                                DateTime proxima = empeño.Fecha.AddMonths(k);
                                int claveMes = proxima.Year * 12 + proxima.Month;
                                if (claveMes <= pisoMes)
                                    continue;
                                if (mesesExistentes.Contains(claveMes))
                                    continue;
                            
                                // Sin saldo pendiente no hay interes que cobrar. Una cuota en CERO se daria por pagada
                                // sola (0 >= 0), adelantaria un mes el vencimiento del empeno y saldria una factura en
                                // cero: no debe crearse.
                                double montoCuota = Math.Truncate((double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100));
                                double bodegaCuota = empeño.Interes.Bodegaje != null ? Math.Truncate((double)empeño.MontoPendiente * (double)empeño.Interes.PorcentajeBodegaje) : 0;
                                if (montoCuota + bodegaCuota <= 0)
                                continue;
                            
                                var intereses = new Intereses
                                {
                                    EmpenoId = empeño.EmpenoId,
                                    FechaCreacion = DateTime.Now,
                                    Monto = montoCuota,
                                    MontoBodega = bodegaCuota,
                                    FechaVencimiento = proxima
                                };
                                _context.Intereses.Add(intereses);
                                mesesExistentes.Add(claveMes);
                                await _context.SaveChangesAsync();
                            }


                            //Ultima Mejora
                            var count = await _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).ToListAsync();
                            if (count.Count() > 0)
                            {
                                var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId)
                                            .OrderByDescending(o => o.InteresesId)
                                            .FirstOrDefaultAsync();
                                if (ultimoInteres != null)
                                {
                                    // Ahora que la cuota del mes en curso se crea al ARRANCAR el periodo, la ultima cuota
                                    // siempre vence a futuro. Mirar solo la ultima daria Vigente siempre y taparia la
                                    // mora, asi que se evalua si hay CUALQUIER cuota ya vencida y sin pagar.
                                    // 'count' ya viene materializado, por eso se puede usar MontoTotal (propiedad calculada).
                                    if (count.Any(i => i.FechaVencimiento < DateTime.Today
                                        && Math.Truncate(Math.Round(i.Pagado)) < Math.Truncate(i.MontoTotal)))
                                    {
                                        empeño.Estado = Estado.Pendiente;
                                    }
                                    else
                                    {
                                        empeño.Estado = Estado.Vigente;
                                    }
                                    _context.Entry(empeño).State = EntityState.Modified;
                                    await _context.SaveChangesAsync();
                                }

                            }
                            if (empeño.FechaVencimiento < DateTime.Today)
                            {
                                if (empeño.Retirado || empeño.FechaRetiro != null)
                                {
                                    empeño.Estado = Estado.Cancelado;
                                }
                                else if (empeño.RetiradoAdministrador || empeño.FechaRetiroAdministrador != null)
                                {
                                    empeño.Estado = Estado.Retirado;
                                }
                                else if (empeño.MontoPendiente==0)
                                {
                                    empeño.Estado = Estado.Cancelado;
                                }
                                else
                                {
                                    empeño.Estado = Estado.Vencido;
                                }
                                _context.Entry(empeño).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                        }
                    
                    }
                    await SaveBitacora(new ValorBitacora
                    {
                        Valor = "Revisión Automatica de Empeños",
                        Modulo = "Revisar Empeños",
                        Accion = "Automatico"
                    });
                }
            }
            catch (Exception ex)
            {
                await SaveBitacora(new ValorBitacora
                {
                    Valor = "Error al revisar el empeño " + id,
                    Modulo = "Revisar Empeños",
                    Accion = "Error"
                }, 1, ex.Message);
            }
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

        public async Task SaveBitacora(ValorBitacora valorBitacora, int error=0, string message="") 
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var bitacora = new Bitacora
                    {
                        Error = error,
                        Fecha = DateTime.Now,
                        Mensaje = message,
                        Valor = JsonConvert.SerializeObject(valorBitacora)
                    };

                    if (Program.EmpleadoId > 0 && Program.Acceso)
                    {
                        bitacora.EmpleadoId = Program.EmpleadoId;
                    }
                    else if (Program.Usuario != null)
                    {
                        bitacora.Usuario = Program.Usuario.Usuario;
                        var empleadoId = await GetEmpleadoIdByUser(Program.Usuario.Usuario);
                        bitacora.EmpleadoId = empleadoId;
                    }

                    _context.Bitacoras.Add(bitacora);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public bool Validate(TextBox txt, Label lbl)
        {
            if (txt.Text == lbl.Text)
            {
                MessageBox.Show("El campo " + lbl.Text + " es un campo requerido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool ValidateNum(TextBox txt, Label lbl, bool requiered=true)
        {
            double number;

            if (txt.Text == lbl.Text && requiered)
            {
                MessageBox.Show("El campo " + lbl.Text + " es un campo requerido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (!double.TryParse(txt.Text, out number))
            {
                MessageBox.Show("El campo " + lbl.Text + " es un campo de Número", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public void KeyNumber(object sender)
        {
            var validate = false;
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text))
                return;

            var lastKey = txt.Text.Substring(txt.TextLength - 1, 1);
            switch (lastKey)
            {
                case "0":                
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ".":
                case ",":                
                    validate = true;
                    break;
                default:
                    validate = false;
                    break;
            }
            if (!validate && txt.TextLength>0)
            {
                MessageBox.Show("El campo debe ser un Número", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Text = txt.Text.Substring(0, txt.TextLength - 1);
                if (txt.Text == string.Empty)
                    txt.Text = "0.00";
            }
        }

        public string GetValue(TextBox txt, Label lbl)
        {
            if (txt.Text == lbl.Text)
            {
                return string.Empty;
            }
            return txt.Text;
        }

        public void FormatNumber(object sender) 
        {
            TextBox txt = (TextBox)sender;

            if (string.IsNullOrEmpty(txt.Text))
            {
                txt.Text = "0.00";
            }
            double numero=0;
            double.TryParse(txt.Text, out numero);                    
            txt.Text = numero.ToString("N2");
        }

        public bool ValidatePIN(string modulo)
        {
            frmOscuro oscuro = new frmOscuro();
            oscuro.TopMost = true;
            oscuro.Show();
            frmPIN pin = new frmPIN(modulo);
            pin.TopMost = true;                 // que el PIN quede SIEMPRE al frente del shell (WebView sin bordes), no detrás
            pin.ShowDialog();
            oscuro.Close();

            if (!Program.Acceso)            
                return false;
                        
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~Funciones()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
