using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
                detalle.Cantidad = double.Parse(txtCantidad.Text);
                detalles.Add(detalle);
                LoadList();
            }
        }

        public void LoadList()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = detalles.Select(x=>new 
            {
                Id=x.CierreCajaId,
                x.Concepto,
                x.Valor,
                x.Cantidad,
                x.SubTotal
            }).ToList();
            dgvDetalles.Refresh();           
            txtTotal.Text = ((double.Parse(textBox1.Text) * -1) + detalles.Sum(d => d.SubTotal)).ToString("N2");
        }

        private async void frmCierreCaja_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtFecha.Enabled = false;
            empleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            empleadoId = empleadoId == null ? 1 : empleadoId;
            var empleado = await _context.Empleados.FindAsync(empleadoId);
            txtEmpleado.Text = empleado.Nombre;
            txtEmpleado.Enabled = false;
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (txtConcepto.Text!=string.Empty)
            {
                txtCantidad.Text = "1";
            }
        }

        private async void btnGuardarCierreCaja_Click(object sender, EventArgs e)
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
            txtCantidad.Text = "0";
        }

        private void btnDeleteCierreCaja_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            txtValor.Text = "0";
            txtCantidad.Text = "0";
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
            Microsoft.Office.Interop.Excel.Application cexcel = new Microsoft.Office.Interop.Excel.Application();

            cexcel.Workbooks.Open("C:\\Empeños\\CierreCaja\\CierreCaja.xlsx", true, true);
            cexcel.Visible = true;

            int posicion = 15;

            var empleadoId= await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
            var empleado = _context.Empleados.Find(empleadoId);
            cexcel.Cells[9, 2].value = empleado.Nombre;
            cexcel.Cells[10, 2].value = empleado.Usuario;

            foreach (DataGridViewRow item in dgvDetalles.Rows)
            {
                cexcel.Cells[16, 1].value = item.Cells[1].Value.ToString();
                cexcel.Cells[16, 2].value = item.Cells[2].Value.ToString();
                cexcel.Cells[16, 3].value = item.Cells[3].Value.ToString();
                cexcel.Cells[16, 4].value = item.Cells[4].Value.ToString();
                Range line = (Range)cexcel.Rows[16];
                line.Insert();
            }

            
          //cexcel.Cells[24, 3].value = Convert.ToDateTime(FechaVence).Day.ToString() + "/" + Convert.ToDateTime(FechaVence).Month.ToString() + "/" + Convert.ToDateTime(FechaVence).Year.ToString();
            //cexcel.Cells[26, 3].value = Convert.ToInt32(objinteres.Monto_Interes).ToString();
            cexcel.Cells[28, 3].value = "Pendiente";
            cexcel.ActiveWindow.SelectedSheets.PrintOut();
            System.Threading.Thread.Sleep(300);
            cexcel.ActiveWorkbook.Close(false);
            cexcel.Quit();
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text== string.Empty)
            {
                textBox1.Text = "0";
            }
            double number;
            double.TryParse(textBox1.Text,out number);
            if (number>0)
            {
                txtTotal.Text = ((double.Parse(textBox1.Text) * -1) + detalles.Sum(d => d.SubTotal)).ToString("N2");
            }
        }
    }
}
