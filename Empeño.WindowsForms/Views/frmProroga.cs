using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Newtonsoft.Json;
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
                int dias;
                if (!int.TryParse(txtTiempo.Text, out dias) || dias <= 0)
                {
                    MessageBox.Show("Los días de prórroga deben ser un número mayor a cero.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var empeño = await _context.Empenos.FindAsync(EmpeñoId);

                if (empeño!=null)
                {
                    string accion;
                    if (ProrogaId>0)
                    {
                        accion = "Editar";
                        var proroga = await _context.Prorrogas.FindAsync(ProrogaId);
                        int diasAnteriores = proroga.DiasProrroga;
                        proroga.Comentario = txtComentario.Text;
                        proroga.DiasProrroga = dias;
                        proroga.EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario);
                        proroga.EmpenoId = empeño.EmpenoId;
                        proroga.Fecha = DateTime.Today;

                        _context.Entry(proroga).State = EntityState.Modified;

                        // Al editar una prórroga solo se aplica la DIFERENCIA de días, para no volver a
                        // sumar la prórroga completa sobre una fecha que ya la incluía.
                        empeño.FechaVencimiento = empeño.FechaVencimiento.AddDays(dias - diasAnteriores);
                    }
                    else
                    {
                        accion = "Crear";
                        var proroga = new Prorroga()
                        {
                            Comentario = txtComentario.Text,
                            DiasProrroga = dias,
                            EmpleadoId = await funciones.GetEmpleadoIdByUser(Program.Usuario.Usuario),
                            EmpenoId = empeño.EmpenoId,
                            Fecha = DateTime.Today
                        };

                        _context.Prorrogas.Add(proroga);

                        // La prórroga se cuenta desde lo que caiga MÁS TARDE entre el vencimiento vigente y hoy:
                        // así nunca acorta el plazo de uno vigente y le da gracia real a uno ya vencido.
                        var baseProrroga = empeño.FechaVencimiento > DateTime.Today ? empeño.FechaVencimiento : DateTime.Today;
                        empeño.FechaVencimiento = baseProrroga.AddDays(dias);
                    }

                    // FIX: la prórroga ahora SÍ mueve la fecha de vencimiento. Antes solo guardaba el registro
                    // y ReviewEmpeño (Funciones.cs:531/640) volvía a marcar Vencido porque FechaVencimiento no cambiaba.
                    // Se persiste junto con la prórroga en un solo SaveChanges (atómico dentro de este contexto).
                    empeño.Prorroga = true;
                    _context.Entry(empeño).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    // Auditoría (sin PIN: acción de rutina)
                    await funciones.SaveBitacora(new ValorBitacora
                    {
                        Modulo = "Prórroga",
                        Accion = accion,
                        Valor = JsonConvert.SerializeObject(new { empeño.EmpenoId, DiasProrroga = dias, Comentario = txtComentario.Text })
                    });

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
