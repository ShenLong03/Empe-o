using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmClientes : System.Windows.Forms.Form
    {
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();
        public int xClick = 0, yClick = 0;
        int lx, ly;
        int sw, sh;
        int clienteId = 0;

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            Program.GetCargando(Screen.PrimaryScreen.WorkingArea.Size, Screen.PrimaryScreen.WorkingArea.Location);

            lx = this.Location.X != lx ? this.Location.X : lx;
            ly = this.Location.Y != ly ? this.Location.Y : ly;
            sw = this.Size.Width != sw ? this.Size.Width : sw;
            sh = this.Size.Height != sh ? this.Size.Height : sh;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            btnMaximize.Visible = false;
            btnRestore.Visible = true;

            Program.CargandoClose();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Program.GetCargando(this.Size, this.Location);

            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);

            btnRestore.Visible = false;
            btnMaximize.Visible = true;

            Program.CargandoClose();
        }

        public frmClientes(int id=0)
        {
            InitializeComponent();
            if (id>0)
            {
                clienteId = id;
            }
        }

        private async void frmClientes_Load(object sender, EventArgs e)
        {
            Program.Cliente = null;
            dgvClientes.DataSource = await _context.Clientes.Where(c => !c.IsDelete).Select(x => new
            {
                Id = x.ClienteId,
                x.Identificacion,
                x.Nombre,
                x.Correo,
                x.Telefono
            }).ToListAsync();

            lblCantidad.Text = dgvClientes.Rows.Count.ToString();

            DataGridViewColumn column = dgvClientes.Columns[0];
            column.Width = 40;
            funciones.ResetForm(panelFormulario);
            btnRestore.Visible = false;
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            chbActivo.Checked = true;
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");

            if (clienteId > 0)
            {
                await Editar((int)dgvClientes.SelectedRows[0].Cells[0].Value);
            }
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = await _context.Clientes.Where(c => c.Nombre.Contains(txtBuscar.Text) || c.Identificacion.Contains(txtBuscar.Text))
                .Select(x => new
            {
                Id = x.ClienteId,
                x.Identificacion,
                x.Nombre,
                x.Correo,
                x.Telefono
            }).ToListAsync();

            lblCantidad.Text = dgvClientes.Rows.Count.ToString();

            DataGridViewColumn column = dgvClientes.Columns[0];
            column.Width = 40;            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public async Task Guardar()
        {
            try
            {
                if (txtIdentificacion.Text == "Identificación" || string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    MessageBox.Show("La Identificación es requerida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (clienteId == 0)
                {
                    var cliente = new Cliente
                    {
                        ClienteId = clienteId,
                        Identificacion = txtIdentificacion.Text==lblIdentificacion.Text?string.Empty:txtIdentificacion.Text,
                        Nombre = txtNombre.Text == lblNombre.Text ? string.Empty : txtNombre.Text,
                        Telefono = txtTelefono.Text == lblTelefono.Text ? string.Empty : txtTelefono.Text,
                        Correo = txtCorreo.Text == lblCorreo.Text ? string.Empty : txtCorreo.Text,
                        Activo = chbActivo.Checked,
                        Direccion = txtDireccion.Text == lblDireccion.Text ? string.Empty : txtDireccion.Text,
                        Comentario = txtComentario.Text == lblComentario.Text ? string.Empty : txtComentario.Text,
                        Fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    };

                    _context.Clientes.Add(cliente);

                }
                else
                {
                    var cliente = _context.Clientes.Find(clienteId);
                    cliente.Identificacion = txtIdentificacion.Text == lblIdentificacion.Text ? string.Empty : txtIdentificacion.Text;
                    cliente.Correo = txtCorreo.Text == lblCorreo.Text ? string.Empty : txtCorreo.Text;
                    cliente.Nombre = txtNombre.Text == lblNombre.Text ? string.Empty : txtNombre.Text;
                    cliente.Telefono = txtTelefono.Text == lblTelefono.Text ? string.Empty : txtTelefono.Text;
                    cliente.Activo = chbActivo.Checked;
                    cliente.Direccion = txtDireccion.Text == lblDireccion.Text ? string.Empty : txtDireccion.Text;
                    cliente.Comentario = txtComentario.Text == lblComentario.Text ? string.Empty : txtComentario.Text;
                    cliente.Fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _context.Entry(cliente).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                await LoadData();
                funciones.ResetForm(panelFormulario);
                clienteId = 0;
                chbActivo.Checked = true;
                txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task GuardarEmpeño()
        {
            try
            {
                Cliente cliente;
                if (txtIdentificacion.Text == "Identificación" || string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    MessageBox.Show("La Identificación es requerida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (clienteId == 0)
                {
                    cliente = new Cliente
                    {
                        ClienteId = clienteId,
                        Identificacion = txtIdentificacion.Text,
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Correo = txtCorreo.Text,
                        Activo = chbActivo.Checked,
                        Direccion = txtDireccion.Text,
                        Comentario = txtComentario.Text,
                        Fecha = DateTime.ParseExact(txtFecha.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture),
                    };

                    _context.Clientes.Add(cliente);

                }
                else
                {
                    cliente = _context.Clientes.Find(clienteId);
                    cliente.Identificacion = txtIdentificacion.Text;
                    cliente.Correo = txtCorreo.Text;
                    cliente.Nombre = txtNombre.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.Activo = chbActivo.Checked;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Comentario = txtComentario.Text;
                    cliente.Fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _context.Entry(cliente).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                await LoadData();
                funciones.ResetForm(panelFormulario);
                clienteId = 0;
                chbActivo.Checked = true;
                txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.Cliente = cliente;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await Guardar();
        }

        public async Task LoadData()
        {
            dgvClientes.DataSource = await _context.Clientes.Where(c=>!c.IsDelete).Select(x => new
            {
                Id = x.ClienteId,
                x.Identificacion,
                x.Nombre,
                x.Correo,
                x.Telefono
            }).ToListAsync();

            lblCantidad.Text = dgvClientes.Rows.Count.ToString();

            DataGridViewColumn column = dgvClientes.Columns[0];
            column.Width = 40;
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                await Editar((int)dgvClientes.SelectedRows[0].Cells[0].Value);
            }          
        }



        public async Task Editar(int id)
        {
            try
            {               
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clienteId = 0;
                    return;
                }
                funciones.ResetForm(panelFormulario);
                clienteId = 0;
                clienteId = int.Parse(dgvClientes.SelectedRows[0].Cells[0].Value.ToString());
                txtIdentificacion.Text = cliente.Identificacion;
                txtNombre.Text = cliente.Nombre;
                txtTelefono.Text = cliente.Telefono;
                txtCorreo.Text = cliente.Correo;
                chbActivo.Checked = cliente.Activo;
                txtDireccion.Text = cliente.Direccion;
                txtComentario.Text = cliente.Comentario;
                txtFecha.Text = cliente.Fecha.ToString("dd/MM/yyyy");
                lblGanancias.Text = cliente.Empenos.Where(m => m.Pagos.Count() > 0).SelectMany(m => m.Pagos).Sum(p => p.Monto).ToString("N2");
                lblEmpeños.Text = cliente.Empenos.Where(m => !m.IsDelete).Count().ToString();
                funciones.BlockTextBox(panelFormulario, true);
                funciones.EditTextColor(panelFormulario);
                funciones.ShowLabels(panelFormulario);
                funciones.TextBoxColorBlank(panelFormulario);
                funciones.IntelligHolders(panelFormulario);
            }
            catch (Exception)
            {

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            funciones.ResetForm(panelFormulario);
            clienteId = 0;
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            chbActivo.Checked = true;
        }

        private void txtIdentificacion_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIdentificacion, lblIdentificacion, PlaceHolderType.Enter, "Identificación");
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtIdentificacion, lblIdentificacion, PlaceHolderType.Leave, "Identificación");
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Leave, "Nombre");
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtNombre, lblNombre, PlaceHolderType.Enter, "Nombre");
        }

        private void txtCorreo_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Enter, "Correo");
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtCorreo, lblCorreo, PlaceHolderType.Leave, "Correo");
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Leave, "Teléfono");
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtTelefono, lblTelefono, PlaceHolderType.Enter, "Teléfono");
        }

        private void txtDirección_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtDireccion, lblDireccion, PlaceHolderType.Leave, "Dirección");
        }

        private void txtDirección_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtDireccion, lblDireccion, PlaceHolderType.Enter, "Dirección");
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtComentario, lblComentario, PlaceHolderType.Leave, "Comentarios");
        }

        private void txtComentario_Enter(object sender, EventArgs e)
        {
            funciones.PlaceHolder(txtComentario, lblComentario, PlaceHolderType.Enter, "Comentarios");
        }

        private async void btnGuardarEmpeñar_Click(object sender, EventArgs e)
        {
            await GuardarEmpeño();
            this.Close();
        }

        private async void btnEmpeñar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count>0)
            {
                var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                Program.Cliente = cliente;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un cliente", "Información",MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }

        }

        private async void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                await Editar((int)dgvClientes.SelectedRows[0].Cells[0].Value);
                funciones.BlockTextBox(panelFormulario, false);
            }            
        }

        #region RenderSizeForm
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var resp = MessageBox.Show("Esta seguro que desea borrar los datos", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp==DialogResult.Yes)
                {
                    var cliente = await _context.Clientes.FindAsync(dgvClientes.SelectedRows[0].Cells[0].Value);
                    if (cliente != null)
                    {
                        cliente.IsDelete = true;
                        _context.Entry(cliente).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        await LoadData();
                    }
                }
                       
            }
            else
            {
                MessageBox.Show("Seleccione un cliente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count>0)
            {
                await Editar((int)dgvClientes.SelectedRows[0].Cells[0].Value);
            }           
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
           // this.panelContenedor.Region = region;
            this.Invalidate();
        }

        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        #endregion
    }
}
