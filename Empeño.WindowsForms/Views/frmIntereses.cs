﻿using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmIntereses : Form, IDisposable
    {
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        int interesId;

        public frmIntereses()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (interesId>0)
            {
                var interes = await _context.Interes.FindAsync(interesId);
             
                interes.Igual = txtIgual.Text == "Igual que" ? 0 : int.Parse(txtIgual.Text);
                interes.Mayor = txtMayor.Text == "Mayor que" ? 0 : int.Parse(txtMayor.Text);
                interes.Menor = txtMenor.Text == "Menor que" ? 0 : int.Parse(txtMenor.Text);
                interes.Nombre = txtNombre.Text;
                interes.Porcentaje = txtValor.Text == "Porcentaje" ? 0 : double.Parse(txtValor.Text);
                interes.Bodega = txtBodega.Text == "Bodega" ? 0 : double.Parse(txtBodega.Text);
                interes.Activo = chbActivo.Checked;
                _context.Entry(interes).State=EntityState.Modified;
            }
            else
            {
                if (!funciones.Validate(txtNombre, lblNombre))
                    return;
                if (!funciones.ValidateNum(txtValor, lblValor))                
                    return;
                
                
                var interes = new Interes
                {
                    Igual = txtIgual.Text == "Igual que" ? 0 : int.Parse(txtIgual.Text),
                    Mayor = txtMayor.Text == "Mayor que" ? 0 : int.Parse(txtMayor.Text),
                    Menor = txtMenor.Text == "Menor que" ? 0 : int.Parse(txtMenor.Text),
                    Bodega = txtBodega.Text == "Bodega" ? 0 : double.Parse(txtBodega.Text),
                    Nombre = txtNombre.Text,
                    Activo = chbActivo.Checked,
                    Porcentaje = txtValor.Text == "Porcentaje" ? 0 : double.Parse(txtValor.Text),
                };

                _context.Interes.Add(interes);
            }
            
            await _context.SaveChangesAsync();
            await LoadData();

            interesId = 0;
            funciones.ResetForm(panelFormulario);
            MessageBox.Show("Datos guardados correctamente");         
        }

        private async void frmIntereses_Load(object sender, EventArgs e)
        {
            await LoadData();

        }

        private async Task LoadData()
        {
            dgvLista.DataSource = await _context.Interes.Select(x=>new 
            {
                Id=x.InteresId,
                x.Nombre,
                Porcentaje=x.Porcentaje + "%",
                Bodega=x.Bodega + "%",
                Mayor_que="> " + x.Mayor,
                Menor_que =x.Menor + " >",
                Igual_que="= " + x.Igual,
                x.Activo
            }).ToListAsync();
            lblCantidad.Text = dgvLista.Rows.Count.ToString();

            DataGridViewColumn column = dgvLista.Columns[0];
            column.Width = 60;
        }

        public void DeleteTextbox() 
        {
            txtNombre.Text = string.Empty;
            funciones.PlaceHolder(txtNombre, lblNombre,PlaceHolderType.Leave, "Nombre");
            txtValor.Text = string.Empty;
            funciones.PlaceHolder(txtValor, lblValor,PlaceHolderType.Leave, "Porcentaje");
            txtMayor.Text = string.Empty;
            funciones.PlaceHolder(txtMayor, lblMayor,PlaceHolderType.Leave, "Mayor que");
            txtMenor.Text = string.Empty;
            funciones.PlaceHolder(txtMenor, lblMenor,PlaceHolderType.Leave, "Menor que");
            txtIgual.Text = string.Empty;
            funciones.PlaceHolder(txtIgual, lblIgual,PlaceHolderType.Leave, "Igual que");
            txtBodega.Text = string.Empty;
            funciones.PlaceHolder(txtBodega, lblBodega, PlaceHolderType.Leave, "Bodega");
            interesId = 0;
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre,PlaceHolderType.Leave, "Nombre");      
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre,PlaceHolderType.Enter, "Nombre");
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtValor, lblValor,PlaceHolderType.Leave, "Porcentaje");
        }

        private void txtValor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtValor, lblValor,PlaceHolderType.Enter, "Porcentaje");
        }

        private void txtMayor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMayor, lblMayor,PlaceHolderType.Leave, "Mayor que");
        }

        private void txtMayor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMayor, lblMayor,PlaceHolderType.Enter, "Mayor que");
        }

        private void txtMenor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMenor, lblMenor,PlaceHolderType.Leave, "Menor que");
        }

        private void txtMenor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMenor, lblMenor,PlaceHolderType.Enter, "Menor que");
        }

        private void txtIgual_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIgual, lblIgual,PlaceHolderType.Leave, "Igual que");
        }

        private void txtIgual_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIgual, lblIgual,PlaceHolderType.Enter, "Igual que");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            interesId = 0;
            funciones.ResetForm(panelFormulario);            
        }


        public async Task Editar()
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                var interes = await _context.Interes.FindAsync(dgvLista.SelectedRows[0].Cells[0].Value);
                if (interes != null)
                {
                    interesId = interes.InteresId;
                    txtNombre.Text = interes.Nombre;
                    txtValor.Text = interes.Porcentaje.ToString();
                    txtBodega.Text = interes.Bodega.ToString();
                    txtMayor.Text = interes.Mayor.ToString();
                    txtMenor.Text = interes.Menor.ToString();
                    txtIgual.Text = interes.Igual.ToString();
                    chbActivo.Checked = interes.Activo;
                    funciones.ShowLabels(panelFormulario);
                    funciones.BlockTextBox(panelFormulario, true);
                    funciones.EditTextColor(panelFormulario);
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            await Editar();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Esta séguro que desea eliminar el registro?", "Pregunta",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result==DialogResult.Yes)
            {
                if (dgvLista.SelectedRows.Count > 0)
                {
                    var interes = await _context.Interes.FindAsync(dgvLista.SelectedRows[0].Cells[0].Value);
                    _context.Interes.Remove(interes);
                    await _context.SaveChangesAsync();
                    await LoadData();
                }
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
           // funciones.ShowLabelName((TextBox)sender, lblNombre);
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
           // funciones.ShowLabelName((TextBox)sender, lblValor);
        }

        private void txtMayor_TextChanged(object sender, EventArgs e)
        {
           // funciones.ShowLabelName((TextBox)sender, lblMayor);
        }

        private void txtMenor_TextChanged(object sender, EventArgs e)
        {
           // funciones.ShowLabelName((TextBox)sender, lblMenor);
        }

        private void txtIgual_TextChanged(object sender, EventArgs e)
        {
           // funciones.ShowLabelName((TextBox)sender, lblIgual);
        }

        private async void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            await Editar();
        }

        private async void btnVer_Click(object sender, EventArgs e)
        {
            await Editar();
            funciones.BlockTextBox(panelFormulario,false);
        }

        private void txtBodega_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtBodega, lblBodega, PlaceHolderType.Leave, "Bodega");
        }

        private void txtBodega_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtBodega, lblBodega, PlaceHolderType.Enter, "Bodega");
        }
    }
}
