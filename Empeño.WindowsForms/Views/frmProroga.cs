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
    public partial class frmProroga : Form
    {
        int EmpeñoId;
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        int ProrogaId=0;

        public frmProroga(int empeñoId)
        {
            InitializeComponent();
            EmpeñoId = empeñoId;
            Program.Proroga = false;
            Program.EmpeñoId = 0;
        }

        private void panelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void frmProroga_Load(object sender, EventArgs e)
        {
            Program.Proroga = false;
            Program.EmpeñoId = 0;
            if (_context.Prorrogas.Where(p=>p.EmpenoId==EmpeñoId).Count()>0)
            {
                var prorroga = await _context.Prorrogas.Where(p => p.EmpenoId == EmpeñoId).OrderByDescending(p => p.ProrrogaId).FirstOrDefaultAsync();

                txtComentario.Text = prorroga.Comentario;
                txtTiempo.Text = prorroga.DiasProrroga.ToString();
                ProrogaId = prorroga.ProrrogaId;
            }
            else
            {
                txtTiempo.Text = "7";
            }

        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (EmpeñoId>0)
            {
                var empeño = await _context.Empenos.FindAsync(EmpeñoId);

                if (empeño!=null)
                {
                    if (ProrogaId>0)
                    {
                        var proroga = await _context.Prorrogas.FindAsync(ProrogaId);
                        proroga.Comentario = txtComentario.Text;
                        proroga.DiasProrroga = int.Parse(txtTiempo.Text);
                        proroga.EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                        proroga.EmpenoId = empeño.EmpenoId;
                        proroga.Fecha = DateTime.Today;

                        _context.Entry(proroga).State = EntityState.Modified;
                    }
                    else
                    {
                        var proroga = new Prorroga()
                        {
                            Comentario = txtComentario.Text,
                            DiasProrroga = int.Parse(txtTiempo.Text),
                            EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario),
                            EmpenoId = empeño.EmpenoId,
                            Fecha = DateTime.Today
                        };

                        _context.Prorrogas.Add(proroga);
                    }
                    
                    await _context.SaveChangesAsync();
                    Program.Proroga = true;
                    Program.EmpeñoId = EmpeñoId;
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.Proroga = false;
            Program.EmpeñoId = 0;
            this.Close();
        }
    }
}
