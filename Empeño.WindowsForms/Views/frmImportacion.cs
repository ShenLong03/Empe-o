using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using Empeños.Importe.Models;
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
    public partial class frmImportacion : System.Windows.Forms.Form
    {
        private DataContext _context = new DataContext();
        int configuracionId = 0;
        Funciones.Funciones funciones = new Funciones.Funciones();
        OldEmpeños _oldContext = new OldEmpeños();

        public frmImportacion()
        {
            InitializeComponent();
        }

        private void frmConfiguracionGeneral_Load(object sender, EventArgs e)
        {
            lblContadorTabla.Text = "0";
            lblContadorTablaTotal.Text = "7";
            if (_context.Configuraciones.Count()>0)
            {
                var configuracion = _context.Configuraciones.FirstOrDefault();
                txtIdentificacion.Text = configuracion.Identificacion;
                txtCompania.Text = configuracion.Compañia;
                txtNombre.Text = configuracion.Nombre;
                txtMeses.Text = configuracion.Meses.ToString();
                txtTelefono.Text = configuracion.Telefono;
                configuracionId = configuracion.ConfiguracionId;
                txtEmail.Text = configuracion.Email;
                txtPassword.Text = configuracion.Password;
                txtSMTP.Text = configuracion.SMTP;
                txtPuerto.Text = configuracion.Puerto.ToString() ;
                chkSSL.Checked = configuracion.SSL;
                txtDirección.Text = configuracion.Direccion;
                txtEmailAdmin.Text = configuracion.EmailNotification;
            }
            else
            {
                txtPuerto.Text = "587";
            }
            
            txtCompania.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompania.Text!="")
                {
                    if (configuracionId == 0)
                    {
                        var configuracion = new Configuracion
                        {
                            ConfiguracionId = configuracionId,
                            Identificacion = txtIdentificacion.Text,
                            Meses = int.Parse(txtMeses.Text),
                            Compañia = txtCompania.Text,
                            Nombre = txtNombre.Text,
                            Telefono = txtTelefono.Text,
                            Email = txtEmail.Text,
                            Password = txtPassword.Text,
                            Puerto = int.Parse(!string.IsNullOrEmpty(txtPuerto.Text) ? txtPuerto.Text : "0"),
                            SMTP = txtSMTP.Text,
                            SSL = chkSSL.Checked,
                            Direccion = txtDirección.Text,
                            EmailNotification = txtEmailAdmin.Text
                        };

                        _context.Configuraciones.Add(configuracion);
                    }
                    else
                    {
                        var configuracion = _context.Configuraciones.Find(configuracionId);

                        configuracion.Identificacion = txtIdentificacion.Text;
                        configuracion.Meses = int.Parse(txtMeses.Text);
                        configuracion.Nombre = txtNombre.Text;
                        configuracion.Compañia = txtCompania.Text;
                        configuracion.Telefono = txtTelefono.Text;
                        configuracion.Email = txtEmail.Text;
                        configuracion.Password = txtPassword.Text;
                        configuracion.Puerto = int.Parse(!string.IsNullOrEmpty(txtPuerto.Text) ? txtPuerto.Text : "0");
                        configuracion.SMTP = txtSMTP.Text;
                        configuracion.SSL = chkSSL.Checked;
                        configuracion.Direccion = txtDirección.Text;
                        configuracion.EmailNotification = txtEmailAdmin.Text;

                        _context.Entry(configuracion).State = EntityState.Modified;
                    }
                    _context.SaveChanges(); 
                }

                progressBar2.Minimum = 0;
                progressBar2.Maximum = 9;
                progressBar2.Step = 1;
                progressBar2.Value = 0;
                
                lblContadorTabla.Text = "1";
                progressBar2.Value = 1;
                await AddInteres();
                lblContadorTabla.Text = "2";
                progressBar2.Value = 2;
                await ImportEmpleados();
                lblContadorTabla.Text = "3";
                progressBar2.Value = 3;
                await ImportUsuarios();
                lblContadorTabla.Text = "4";
                progressBar2.Value = 4;
                await ImportClientes();
                lblContadorTabla.Text = "5";
                progressBar2.Value = 5;
                await ImportEmpeños();
                lblContadorTabla.Text = "6";
                progressBar2.Value = 6;
                await ImportIntereses();
                lblContadorTabla.Text = "7";
                progressBar2.Value = 7;
                await ImportPagos();
                lblContadorTabla.Text = "9";
                progressBar2.Value = 9;
                lblContadorTablaTotal.Text = "9";
                MessageBox.Show("Datos importados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public async Task AddInteres()
        {
            try
            {
                lblContador.Text = "0";
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 3;
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                using (DataContext context = new DataContext())
                {
                    List<Interes> interes = new List<Interes>
                {
                    new Interes
                    {
                        InteresId=1,
                        Nombre="5%",
                        Porcentaje=5,
                        Mayor=1,
                        Igual=0,
                        Menor=0,
                        Activo=false
                    },
                    new Interes
                    {
                        InteresId=2,
                        Nombre="10%",
                        Porcentaje=10,
                        Mayor=100000,
                        Igual=0,
                        Menor=0,
                        Activo=false
                    },
                    new Interes
                    {
                        InteresId=3,
                        Nombre="MicroCrédito",
                        Porcentaje=4.43,
                        Mayor=1,
                        Igual=0,
                        Menor=0,
                        Activo=true
                    },
                    new Interes
                    {
                        InteresId=4,
                        Nombre="Crédito Regular",
                        Porcentaje=3.14,
                        Mayor=675000,
                        Igual=0,
                        Menor=0,
                        Activo=true
                    }
                };
                    lblContadorTotal.Text = interes.Count().ToString();
                    lblContador.Text = interes.Count().ToString();
                    progressBar1.Value = 3;
                    context.Interes.AddRange(interes);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
            }
        }

        public async Task ImportInteres()
        {
            try
            {               
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Porcentajes.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    var newList = new List<Interes>();

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Interes
                        {
                            Activo = item.Activo,
                            InteresId = item.Id_Porcentaje,
                            Nombre = $"{item.Porcentaje1}%",
                            Porcentaje = (double)item.Porcentaje1.Value
                        });
                        lblContador.Text = count.ToString();
                        progressBar1.Value = count;
                    }
                    context.Interes.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en la importacion del interes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task ImportEmpleados()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Empleadoes.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Empleado>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.Empleado
                        {
                            Activo = item.Activo,
                            Correo = string.Empty,
                            IsDelete = false,
                            Nombre = $"{item.Nombre} {item.Primer_Apellido} {item.Segundo_Apellido}",
                            Telefono = string.Empty,
                            Usuario = item.Usuario
                        });
                        lblContador.Text = count.ToString();
                        progressBar1.Value = 0;
                    }
                    context.Empleados.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ImportUsuarios()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Empleadoes.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.User>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.User
                        {
                            Activo = item.Activo,
                            Password=item.Clave,
                            PerfilId=4,
                            Codigo=item.Clave,
                            Usuario = item.Usuario,                            
                        });
                        lblContador.Text = count.ToString();
                        progressBar1.Value = 0;
                    }
                    context.User.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ImportClientes()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Clientes.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Cliente>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.Cliente
                        {
                            Activo = item.Activo,
                            Correo = item.Correo,
                            IsDelete = false,
                            Nombre = item.Nombre_Completo,
                            Telefono = item.Telefono,
                            Identificacion = item.Cedula_Cliente,
                            Direccion = string.Empty,
                            Fecha = DateTime.Today
                        });
                        progressBar1.Value = count;
                        lblContador.Text = count.ToString();
                    }
                    context.Clientes.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ImportEmpeños()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Empeño.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Empeno>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        var cliente = await GetCustomerID(item.Cedula_Cliente);

                        newList.Add(new Empeño.CommonEF.Entities.Empeno
                        {
                            EmpenoId = (int)item.Numero_Empeño,
                            ClienteId = cliente.ClienteId,
                            Comentario = string.Empty,
                            Descripcion = item.Descripcion_Producto,
                            EditorId = 3,
                            EmpleadoId = 3,
                            EsOro = item.Es_Oro,
                            Fecha = item.Fecha_Realizacion,
                            FechaRetiro = item.fretiro,
                            FechaRetiroAdministrador = null,
                            FechaVencimiento = item.Fecha_Vencimiento,
                            InteresId = item.Id_Porcentaje,
                            IsDelete = false,
                            Monto = item.Monto_Empeño,
                            MontoPendiente = item.Saldo_Pendiente,
                            Prorroga = false,
                            Retirado = item.Retirado != null ? item.Retirado.Value : false,
                            RetiradoAdministrador = false,
                            Estado = (item.Id_Estado == 2)
                            ? Empeño.CommonEF.Enum.Estado.Cancelado
                            : (item.Id_Estado == 4)
                            ? Empeño.CommonEF.Enum.Estado.Vencido
                            : (item.Id_Estado == 1)
                            ? Empeño.CommonEF.Enum.Estado.Vigente
                            : (item.Id_Estado == 3)
                            ? Empeño.CommonEF.Enum.Estado.Anulado
                            : Empeño.CommonEF.Enum.Estado.Pendiente
                        });
                        progressBar1.Value = count;
                        lblContador.Text = count.ToString();
                    }

                    context.Empenos.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ImportIntereses()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Interes_Empeño.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Intereses>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.Intereses
                        {
                            EmpenoId = (int)item.Numero_Empeño,
                            FechaCreacion = DateTime.Today,
                            FechaVencimiento = item.Fecha_Cobro_Interes,
                            InteresesId = item.Id_Interes,
                            Monto = (double)item.Monto_Interes,
                            Pagado = item.Monto_Pagado_Interes != null ? (double)item.Monto_Pagado_Interes : 0,
                        });
                        progressBar1.Value = count;
                        lblContador.Text = count.ToString();
                    }

                    context.Intereses.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ImportPagos()
        {
            try
            {
                await AbonosPrincipal();
                await PagoIntereses();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AbonosPrincipal()
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.AbonoPrincipals.ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Pago>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;
                 

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.Pago
                        {
                            EmpenoId = (int)item.Numero_Empeño,
                            EmpleadoId = 3,
                            Fecha = item.Fecha_Abono,
                            PagoId = item.Id_Abono,
                            Monto = item.Monto_Abono,
                            TipoPago = CommonEF.Enum.TipoPago.Principal
                        });
                        progressBar1.Value = count;
                        lblContador.Text = count.ToString();
                    }

                    context.Pago.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task PagoIntereses()
        {
            lblContadorTabla.Text = "8";
            progressBar2.Value = 8;
            try
            {
                using (DataContext context = new DataContext())
                {
                    int count = 0;
                    lblContador.Text = "0";
                    var oldList = await _oldContext.Interes_Empeño.Where(i => i.Monto_Pagado_Interes > 0 && i.Fecha_Registro != null).ToListAsync();
                    lblContadorTotal.Text = oldList.Count().ToString();
                    var newList = new List<Empeño.CommonEF.Entities.Pago>();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = oldList.Count();
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (var item in oldList)
                    {
                        count++;
                        newList.Add(new Empeño.CommonEF.Entities.Pago
                        {
                            EmpenoId = (int)item.Numero_Empeño,
                            EmpleadoId = 3,
                            Fecha = item.Fecha_Registro.Value,
                            PagoId = item.Id_Interes,
                            Monto = item.Monto_Pagado_Interes != null ? (double)item.Monto_Pagado_Interes.Value : 0,
                            TipoPago = CommonEF.Enum.TipoPago.Interes
                        });
                        progressBar1.Value = count;
                        lblContador.Text = count.ToString();
                    }

                    context.Pago.AddRange(newList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Empeño.CommonEF.Entities.Cliente> GetCustomerID(string id)
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    if (context.Clientes.Where(x => x.Identificacion == id).Any())
                    {
                        return await context.Clientes.Where(x => x.Identificacion == id).FirstOrDefaultAsync();
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }     
    }
}
