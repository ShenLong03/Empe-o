using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Funciones
{
    public class Funciones
    {
        DataContext _context = new DataContext();

        public void PlaceHolder(TextBox textBox, PlaceHolderType type, string placeHolder)
        {
            switch (type)
            {
                case PlaceHolderType.Leave:
                    if (textBox.Text == "")
                    {
                        textBox.Text = placeHolder;
                        textBox.ForeColor = Color.DimGray;
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
                        textBox.ForeColor = Color.DimGray;
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
                        textBox.ForeColor = Color.DimGray;
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
                        textBox.ForeColor = Color.DimGray;
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
                var empleado = await _context.Empleados.SingleOrDefaultAsync(e => e.Usuario == user);
                if (empleado != null)
                {
                    return empleado.EmpleadoId;
                }
                return 0;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> GetInteresIdByNombre(string nombre)
        {
            try
            {
                var interes = await _context.Interes.SingleOrDefaultAsync(e => e.Nombre == nombre);
                if (interes != null)
                {
                    return interes.InteresId;
                }
                return 0;
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
                    textBox.ForeColor = Color.DimGray;
                }

                if (item is ComboBox)
                {
                    var comboBox = (ComboBox)item;
                    comboBox.ForeColor = Color.DimGray;
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
                                textBox.ForeColor = Color.DimGray;
                            }
                        }
                        else if (itemTextBox is ComboBox)
                        {
                            var comboBox = (ComboBox)itemTextBox;
                            var textBoxText = comboBox.Text;
                            if (labelText == textBoxText)
                            {
                                comboBox.ForeColor = Color.DimGray;
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
                                    textBox.ForeColor = Color.DimGray;

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
                    textBox.ForeColor = block ? Color.Black : Color.DimGray;
                }
                else if (item is ComboBox)
                {
                    var comboBox = (ComboBox)item;
                    comboBox.Enabled = block;
                    comboBox.ForeColor = block ? Color.Black : Color.DimGray;
                }
            }
        }

        public async Task ReviewEmpeños() 
        {
            var empeños = _context.Empenos.Where(w => (w.Estado == Estado.Activo
              || w.Estado == Estado.Pendiente
              || w.Estado == Estado.Vencido)).ToList();

            if (empeños.Count>0)
            {
                foreach (var empeño in empeños)
                {
                    int cantidadMeses = ((int)(DateTime.Today - empeño.Fecha).TotalDays / 30);
                    double sobrante = (DateTime.Today - empeño.Fecha).TotalDays % 30;
                    cantidadMeses += sobrante > 0 ? 1 : 0;
                    int cantidadIntereses = empeño.Intereses.Count();

                    if (cantidadMeses > cantidadIntereses)
                    {
                        var numeroIntereses = cantidadMeses - cantidadIntereses;
                        for (int i = 0; i < numeroIntereses; i++)
                        {
                            var intereses = new Intereses
                            {
                                EmpenoId = empeño.EmpenoId,
                                FechaCreacion = DateTime.Now,
                                Monto = (double)empeño.MontoPendiente * ((double)empeño.Interes.Porcentaje / (double)100)
                            };

                            if (empeño.Intereses.Count() > 0)
                            {
                                intereses.FechaVencimiento = empeño.Intereses.LastOrDefault().FechaVencimiento.AddMonths(1);
                            }
                            else
                            {
                                intereses.FechaVencimiento = empeño.FechaVencimiento.AddMonths(1);
                            }

                            _context.Intereses.Add(intereses);
                        }
                        await _context.SaveChangesAsync();
                    }
                    if (_context.Intereses.Where(p=>p.EmpenoId==empeño.EmpenoId)
                        .LastOrDefault().FechaVencimiento<DateTime.Today)
                    {
                        empeño.Estado = Estado.Pendiente;
                        _context.Entry(empeño).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
