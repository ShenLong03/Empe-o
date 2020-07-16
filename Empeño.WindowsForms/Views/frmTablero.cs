using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.ViewReports;
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
    public partial class frmTablero : Form
    {
        DataContext _context = new DataContext();

        public frmTablero()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private async void frmTablero_Load(object sender, EventArgs e)
        {
            DateTime desde = DateTime.Today.AddMonths(-4);
            DateTime hasta = DateTime.Today;

            var egresos = await _context.Empenos.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();

            var list = new List<IngresosEgresosReporte>();

            foreach (var item in egresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Cliente.Nombre,
                    Egresos = item.Monto,
                    Tipo = "Empeño",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Cliente.Identificacion,
                    Ingresos = null
                });
            }

            foreach (var item in ingresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Empeno.Cliente.Nombre,
                    Egresos = null,
                    Tipo = item.TipoPago == CommonEF.Enum.TipoPago.Interes ? "Interes" :
                    item.TipoPago == CommonEF.Enum.TipoPago.Principal ? "Principal" : "",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Empeno.Cliente.Identificacion,
                    Ingresos = item.Monto
                });
            }

            chartVentas.Series[0].Points.DataBindXY(list.Select(i => i.Fecha.Date).ToList(), list.Select(i => i.Ingresos).ToList());
            chartVentas.Series[1].Points.DataBindXY(list.Select(i => i.Fecha.Date).ToList(), list.Select(i => i.Egresos).ToList());
        }
    }
}
