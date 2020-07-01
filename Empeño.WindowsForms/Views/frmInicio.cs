using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmInicio : Form
    {
        public int xClick = 0, yClick = 0;

        public frmInicio()
        {
            InitializeComponent();
            //Estas lineas eliminan los parpadeos del formulario o controles en la interfaz grafica (Pero no en un 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }
        int cont = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            cont += 1;
            if (cont==100)
            {
                timer1.Stop();
            }            
        }

        private void frmInicio_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {            
            this.Opacity = 0.0;
            btnRestore.Visible = false;
            btnMaximize.Visible = true;
            timer1.Start();
            //AbrirFormulario<frmPrincipal>();
            //iconModulo.IconChar = FontAwesome.Sharp.IconChar.Home;
            //lblModulo.Text = "Inicio";
        }
 
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
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
            this.panelContenedor.Region = region;
            this.Invalidate();
        }

        private void panelContenedor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        int lx, ly;
        int sw, sh;

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);

            btnRestore.Visible = false;
            btnMaximize.Visible = true;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuConfiguracion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmConfiguracionGeneral>();
            iconModulo.IconChar= FontAwesome.Sharp.IconChar.Cogs;
            iconModulo.IconColor = Color.MediumTurquoise;
            lblModulo.Text = "Configuración";
        }

        private void mnuInicio_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmEmpeno>();
            iconModulo.IconChar = FontAwesome.Sharp.IconChar.Home;
            iconModulo.IconColor = Color.MediumPurple;
            lblModulo.Text = "Inicio";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmEmpleados>();
            iconModulo.IconChar = FontAwesome.Sharp.IconChar.Home;
            iconModulo.IconColor = Color.MediumSeaGreen;
            lblModulo.Text = "Empleados";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmEmpeño>();
            iconModulo.IconChar = FontAwesome.Sharp.IconChar.DiceD20;
            iconModulo.IconColor = Color.MediumVioletRed;
            lblModulo.Text = "Empeños";
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            lx = this.Location.X!=lx? this.Location.X:lx;
            ly = this.Location.Y!=ly? this.Location.Y:ly;
            sw = this.Size.Width!=sw? this.Size.Width:sw;
            sh = this.Size.Height!=sh? this.Size.Height:sh;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            btnMaximize.Visible = false;
            btnRestore.Visible = true;
        }

        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }


        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
                                                                                     //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }

    }
}
