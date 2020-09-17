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
    public partial class frmEmpeñoInteres : Form
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
            if (!funciones.ValidatePIN("Editar Empeño"))
                return;


            if (intereses.Empeno.InteresId != await funciones.GetInteresIdByNombre(cbInteres.Text) && Program.PerfilId != 4)
            {
                intereses.Empeno.InteresId = await funciones.GetInteresIdByNombre(cbInteres.Text);

                if (intereses.Empeno.Intereses.Count() == 1)
                {
                    var interes = intereses.Empeno.Intereses.FirstOrDefault();
                    if (interes.Pagado == 0)
                    {
                        var porcentaje = await _context.Interes.FindAsync(intereses.Empeno.InteresId);
                        interes.Monto = intereses.Empeno.Monto * ((double)porcentaje.Porcentaje / (double)100);
                        _context.Entry(interes).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                intereses.Monto = double.Parse(txtMonto.Text);                
            }

            intereses.Pagado = double.Parse(txtPagado.Text);
            intereses.FechaCreacion = dtFecha.Value;
            intereses.FechaVencimiento = dtVence.Value;

            _context.Entry(intereses).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            this.Close();
        }

        private async void frmEmpeñoInteres_Load(object sender, EventArgs e)
        {
            cbInteres.DataSource = await _context.Interes.Where(i => i.Activo).ToListAsync();
            cbInteres.DisplayMember = "Nombre";
            cbInteres.ValueMember = "InteresId";
            cbInteres.Text = "Porcentaje";

            dtFecha.Value = intereses.FechaCreacion;
            dtVence.Value = intereses.FechaVencimiento;
            cbInteres.Text = intereses.Empeno.Interes.Nombre;
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
