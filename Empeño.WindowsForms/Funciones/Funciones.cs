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
                var empeños = await _context.Empenos.Where(w => (w.Estado == Estado.Vigente
                     || w.Estado == Estado.Pendiente
                     || w.Estado == Estado.Vencido)).ToListAsync();

                if (empeños.Count > 0)
                {
                    foreach (var empeño in empeños)
                    {

                        var intereses = await _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).ToListAsync();

                        foreach (var item in intereses)
                        {
                            _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.InteresesId != item.InteresesId && i.EmpenoId == item.EmpenoId && i.FechaVencimiento == item.FechaVencimiento));
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
                        _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.InteresesId != item.InteresesId && i.EmpenoId == item.EmpenoId && i.FechaVencimiento == item.FechaVencimiento));
                    }
                    var proximoMes = DateTime.Today.AddMonths(1).AddDays(1);
                    _context.Intereses.RemoveRange(_context.Intereses.Where(i => i.FechaVencimiento > proximoMes && i.Pagado <= 0));
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task ReviewEmpeños() 
        {
            try
            {
                using (DataContext _context = new DataContext())
                {
                    var empeños = await _context.Empenos.Where(w => (w.Estado == Estado.Vigente
                          || w.Estado == Estado.Pendiente
                          || w.Estado == Estado.Vencido)).ToListAsync();

                    if (empeños.Count > 0)
                    {
                        foreach (var empeño in empeños)
                        {
                            if (empeño.Estado == Estado.Vigente || empeño.Estado == Estado.Vencido
                                    || empeño.Estado == Estado.Pendiente)
                            {
                                var fechaCalculo = empeño.Fecha;
                                if (empeño.Intereses.Any())
                                    fechaCalculo = empeño.Intereses.OrderByDescending(i => i.FechaVencimiento).FirstOrDefault().FechaVencimiento;

                                int cantidadMeses = ((int)(DateTime.Today - fechaCalculo).TotalDays / 30);
                                double sobrante = (DateTime.Today - fechaCalculo).TotalDays % 30;
                                cantidadMeses += sobrante > 1 ? 1 : 0;

                                var numeroIntereses = cantidadMeses;
                                for (int i = 0; i < numeroIntereses; i++)
                                {
                                    var intereses = new Intereses
                                    {
                                        EmpenoId = empeño.EmpenoId,
                                        FechaCreacion = DateTime.Now,
                                        Monto = Math.Truncate((double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100))
                                    };

                                    if (_context.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId).Count() > 0)
                                    {
                                        intereses.FechaVencimiento = _context.Intereses
                                            .Where(x => x.EmpenoId == empeño.EmpenoId)
                                            .OrderByDescending(x => x.InteresesId)
                                            .FirstOrDefault()
                                            .FechaVencimiento.AddMonths(1);
                                    }
                                    else
                                    {
                                        intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                                    }
                                    using (DataContext temp = new DataContext())
                                    {
                                        var interesesFind = await temp.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId && x.FechaVencimiento == intereses.FechaVencimiento).ToListAsync();
                                        if (interesesFind.Count() == 0)
                                        {
                                            _context.Intereses.Add(intereses);
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }

                            var count = await _context.Intereses.Where(i => i.EmpenoId == empeño.EmpenoId).ToListAsync();
                            if (count.Count() > 0)
                            {
                                var ultimoInteres = await _context.Intereses.Where(p => p.EmpenoId == empeño.EmpenoId)
                                            .OrderByDescending(o => o.InteresesId)
                                            .FirstOrDefaultAsync();
                                if (ultimoInteres != null)
                                {
                                    if (ultimoInteres.FechaVencimiento < DateTime.Today && ultimoInteres.Monto > ultimoInteres.Pagado)
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
                                else
                                {
                                    empeño.Estado = Estado.Vencido;
                                }

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
            catch (Exception)
            {

            }
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
                        if (empeño.Estado == Estado.Vigente || empeño.Estado == Estado.Vencido
                                || empeño.Estado == Estado.Pendiente)
                        {
                            var fechaCalculo = empeño.Fecha;
                            if (empeño.Intereses.Any())
                                fechaCalculo = empeño.Intereses.OrderByDescending(i => i.FechaVencimiento).FirstOrDefault().FechaVencimiento;

                            int cantidadMeses = ((int)(DateTime.Today - fechaCalculo).TotalDays / 30);
                            double sobrante = (DateTime.Today - fechaCalculo).TotalDays % 30;
                            cantidadMeses += sobrante > 1 ? 1 : 0;

                            if (cantidadMeses>0 && !empeño.Intereses.Any())
                            {
                                var intereses = new Intereses
                                {
                                    EmpenoId = empeño.EmpenoId,
                                    FechaCreacion = DateTime.Now,
                                    Monto = Math.Truncate((double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100)),
                                    FechaVencimiento = fechaCalculo.AddMonths(1)
                                };
                                using (DataContext tempFirst= new DataContext())
                                {
                                    tempFirst.Intereses.Add(intereses);
                                    await tempFirst.SaveChangesAsync();
                                    await ReviewEmpeño(id);
                                    return;
                                }
                            }                         

                            var numeroIntereses = cantidadMeses;
                            for (int i = 0; i < numeroIntereses; i++)
                            {
                                var intereses = new Intereses
                                {
                                    EmpenoId = empeño.EmpenoId,
                                    FechaCreacion = DateTime.Now,
                                    Monto = Math.Truncate((double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100))
                                };

                                if (_context.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId).Count() > 0)
                                {
                                    intereses.FechaVencimiento = _context.Intereses
                                        .Where(x => x.EmpenoId == empeño.EmpenoId)
                                        .OrderByDescending(x => x.InteresesId)
                                        .FirstOrDefault()
                                        .FechaVencimiento.AddMonths(1);
                                }
                                else
                                {
                                    intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                                }
                                using (DataContext temp = new DataContext())
                                {
                                    var interesesFind = await temp.Intereses.Where(x => x.EmpenoId == empeño.EmpenoId && x.FechaVencimiento == intereses.FechaVencimiento).ToListAsync();
                                    if (interesesFind.Count() == 0)
                                    {
                                        _context.Intereses.Add(intereses);
                                        await _context.SaveChangesAsync();
                                    }
                                }
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
                                    if (ultimoInteres.FechaVencimiento < DateTime.Today && ultimoInteres.Monto > ultimoInteres.Pagado)
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
            catch (Exception)
            {

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
            oscuro.Show();
            frmPIN pin = new frmPIN(modulo);
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
