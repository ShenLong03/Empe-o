using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
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
            try
            {
                if (!funciones.ValidatePIN("Configuración"))
                    return;

                if (!funciones.Validate(txtNombre, lblNombre))
                    return;
                if (!funciones.ValidateNum(txtValor, lblValor))
                    return;

                // Parseo con guardas (respeta los placeholders), sin cambios de base de datos
                int mayor = 0, menor = 0, igual = 0, meses = 0;
                double avaluo = 0, bodegaje = 0, porcentaje = 0;

                if (txtMayor.Text != "Mayor que" && !int.TryParse(txtMayor.Text, out mayor))
                {
                    MessageBox.Show("El valor de 'Mayor que' debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMenor.Text != "Menor que" && !int.TryParse(txtMenor.Text, out menor))
                {
                    MessageBox.Show("El valor de 'Menor que' debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtIgual.Text != "Igual que" && !int.TryParse(txtIgual.Text, out igual))
                {
                    MessageBox.Show("El valor de 'Igual que' debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMeses.Text != "Meses" && !int.TryParse(txtMeses.Text, out meses))
                {
                    MessageBox.Show("El plazo (Meses) debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtValor.Text != "Porcentaje" && !double.TryParse(txtValor.Text, out porcentaje))
                {
                    MessageBox.Show("El porcentaje debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtAvaluo.Text != "Avalúo" && !double.TryParse(txtAvaluo.Text, out avaluo))
                {
                    MessageBox.Show("El avalúo debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtBodegaje.Text != "Bodegaje" && !double.TryParse(txtBodegaje.Text, out bodegaje))
                {
                    MessageBox.Show("El bodegaje debe ser numérico.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (porcentaje < 0 || meses < 0 || mayor < 0 || menor < 0 || igual < 0)
                {
                    MessageBox.Show("No se permiten valores negativos.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (avaluo < 0 || avaluo > 100 || bodegaje < 0 || bodegaje > 100)
                {
                    MessageBox.Show("El avalúo y el bodegaje deben estar entre 0 y 100.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object antes = null;
                string accion;

                if (interesId > 0)
                {
                    accion = "Editar";
                    var interes = await _context.Interes.FindAsync(interesId);

                    antes = new
                    {
                        interes.Nombre,
                        interes.Porcentaje,
                        interes.Mayor,
                        interes.Menor,
                        interes.Igual,
                        interes.Meses,
                        interes.Avaluo,
                        interes.Bodegaje,
                        interes.Activo
                    };

                    interes.Igual = igual;
                    interes.Mayor = mayor;
                    interes.Menor = menor;
                    interes.Avaluo = avaluo;
                    interes.Bodegaje = bodegaje;
                    interes.Nombre = txtNombre.Text;
                    interes.Porcentaje = porcentaje;
                    interes.Activo = chbActivo.Checked;
                    interes.Meses = meses;
                    _context.Entry(interes).State = EntityState.Modified;
                }
                else
                {
                    accion = "Crear";
                    var interes = new Interes
                    {
                        Igual = igual,
                        Mayor = mayor,
                        Menor = menor,
                        Avaluo = avaluo,
                        Bodegaje = bodegaje,
                        Nombre = txtNombre.Text,
                        Activo = chbActivo.Checked,
                        Meses = meses,
                        Porcentaje = porcentaje,
                    };

                    _context.Interes.Add(interes);
                }

                await _context.SaveChangesAsync();

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Intereses",
                    Accion = accion,
                    Valor = JsonConvert.SerializeObject(new
                    {
                        antes,
                        despues = new
                        {
                            Nombre = txtNombre.Text,
                            Porcentaje = porcentaje,
                            Mayor = mayor,
                            Menor = menor,
                            Igual = igual,
                            Meses = meses,
                            Avaluo = avaluo,
                            Bodegaje = bodegaje,
                            Activo = chbActivo.Checked
                        }
                    })
                });

                await LoadData();

                interesId = 0;
                funciones.ResetForm(panelFormulario);
                MessageBox.Show("Datos guardados correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmIntereses_Load(object sender, EventArgs e)
        {
            await LoadData();

        }

        private async Task LoadData()
        {
            dgvLista.DataSource = await _context.Interes.Select(x => new
            {
                Id = x.InteresId,
                x.Nombre,
                Porcentaje = x.Porcentaje + "%",
                Mayor_que = "> " + x.Mayor,
                Menor_que = x.Menor + " >",
                Igual_que = "= " + x.Igual,
                Avaluo = x.Avaluo + "%",
                Bodegaje = x.Bodegaje + "%",
                Meses = x.Meses,
                x.Activo
            }).ToListAsync();
            lblCantidad.Text = dgvLista.Rows.Count.ToString();

            DataGridViewColumn column = dgvLista.Columns[0];
            column.Width = 60;
        }

        public void DeleteTextbox()
        {
            txtNombre.Text = string.Empty;
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
            txtValor.Text = string.Empty;
            funciones.PlaceHolder(txtValor, lblValor, PlaceHolderType.Leave, "Porcentaje");
            txtMayor.Text = string.Empty;
            funciones.PlaceHolder(txtMayor, lblMayor, PlaceHolderType.Leave, "Mayor que");
            txtMenor.Text = string.Empty;
            funciones.PlaceHolder(txtMenor, lblMenor, PlaceHolderType.Leave, "Menor que");
            txtIgual.Text = string.Empty;
            funciones.PlaceHolder(txtIgual, lblIgual, PlaceHolderType.Leave, "Igual que");
            txtAvaluo.Text = string.Empty;
            funciones.PlaceHolder(txtAvaluo, lblAvaluo, PlaceHolderType.Leave, "Avalúo");
            txtBodegaje.Text = string.Empty;
            funciones.PlaceHolder(txtBodegaje, lblBodegaje, PlaceHolderType.Leave, "Bodegaje");
            txtMeses.Text = string.Empty;
            funciones.PlaceHolder(txtMeses, lblMeses, PlaceHolderType.Leave, "Meses");

            interesId = 0;
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Enter, "Nombre");
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtValor, lblValor, PlaceHolderType.Leave, "Porcentaje");
        }

        private void txtValor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtValor, lblValor, PlaceHolderType.Enter, "Porcentaje");
        }

        private void txtMayor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMayor, lblMayor, PlaceHolderType.Leave, "Mayor que");
        }

        private void txtMayor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMayor, lblMayor, PlaceHolderType.Enter, "Mayor que");
        }

        private void txtMenor_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMenor, lblMenor, PlaceHolderType.Leave, "Menor que");
        }

        private void txtMenor_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMenor, lblMenor, PlaceHolderType.Enter, "Menor que");
        }

        private void txtIgual_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIgual, lblIgual, PlaceHolderType.Leave, "Igual que");
        }

        private void txtIgual_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIgual, lblIgual, PlaceHolderType.Enter, "Igual que");
        }

        private void txtAvaluo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtAvaluo, lblAvaluo, PlaceHolderType.Leave, "Avalúo");
        }

        private void txtAvaluo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtAvaluo, lblAvaluo, PlaceHolderType.Enter, "Avalúo");
        }

        private void txtBodegaje_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtBodegaje, lblBodegaje, PlaceHolderType.Leave, "Bodegaje");
        }

        private void txtBodegaje_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtBodegaje, lblBodegaje, PlaceHolderType.Enter, "Bodegaje");
        }

        private void txtMeses_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMeses, lblMeses, PlaceHolderType.Leave, "Meses");
        }

        private void txtMeses_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtMeses, lblMeses, PlaceHolderType.Enter, "Meses");
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
                    txtMayor.Text = interes.Mayor.ToString();
                    txtMenor.Text = interes.Menor.ToString();
                    txtIgual.Text = interes.Igual.ToString();
                    txtAvaluo.Text = interes.Avaluo.ToString();
                    txtBodegaje.Text = interes.Bodegaje.ToString();
                    txtMeses.Text = interes.Meses.ToString();
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
            if (!funciones.ValidatePIN("Configuración"))
                return;

            var result = MessageBox.Show("¿Está seguro que desea desactivar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            if (dgvLista.SelectedRows.Count <= 0)
                return;

            try
            {
                var interes = await _context.Interes.FindAsync(dgvLista.SelectedRows[0].Cells[0].Value);
                if (interes == null)
                    return;

                // Soft-delete: se desactiva en vez de borrar (evita romper FK con empeños existentes)
                interes.Activo = false;
                _context.Entry(interes).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await funciones.SaveBitacora(new ValorBitacora
                {
                    Modulo = "Intereses",
                    Accion = "Desactivar",
                    Valor = JsonConvert.SerializeObject(new { interes.InteresId, interes.Nombre })
                });

                await LoadData();
                MessageBox.Show("El plan de tasa fue desactivado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            funciones.BlockTextBox(panelFormulario, false);
        }
    }
}
