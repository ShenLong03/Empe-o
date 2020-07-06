using Empeño.CommonEF.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Funciones
{
    public class Funciones
    {
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

        public void ClearTextBoxs(Panel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.Text = string.Empty;
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
                    label.Visible = false;
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

        public void ResetForm(Panel panel) 
        {
            ClearTextBoxs(panel);
            HideLabels(panel);
            GetPlaceHolders(panel);
        }

        public void BlockTextBox(Panel panel, bool block)
        {
            foreach (var item in panel.Controls)
            {
                if (item is TextBox)
                {
                    var textBox = (TextBox)item;
                    textBox.Enabled = block;
                }
            }
        }
    }
}
