using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmEmpeñoInteres : System.Windows.Forms.Form
    {
        DataContext _context = new DataContext();
        Intereses intereses = new Intereses();
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmEmpeñoInteres(int id)
        {
            InitializeComponent();
            intereses = _context.Intereses.Find(id);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            intereses.Monto = double.Parse(txtMonto.Text);
            intereses.Pagado = double.Parse(txtPagado.Text);
            intereses.FechaCreacion = dtFecha.Value;
            intereses.FechaVencimiento = dtVence.Value;

            _context.Entry(intereses).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            this.Close();
        }

        private void frmEmpeñoInteres_Load(object sender, EventArgs e)
        {
            dtFecha.Value = intereses.FechaCreacion;
            dtVence.Value = intereses.FechaVencimiento;
            txtInteres.Text = intereses.Empeno.Interes.Nombre;
            txtMonto.Text = intereses.Monto.ToString("N2");
            txtPagado.Text = intereses.Pagado.ToString("N2");
            txtTotal.Text = (intereses.Monto - intereses.Pagado).ToString("N2");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
