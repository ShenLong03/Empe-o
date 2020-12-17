namespace Empeño.WindowsForms.Views
{
    using Empeño.CommonEF.Entities;
    using Empeño.WindowsForms.Data;
    using Empeño.WindowsForms.Reports;
    using FontAwesome.Sharp;
    using System;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class frmInicio : Form
    {
        public int xClick = 0, yClick = 0;
        private IconButton currentBtn;
        private Form currentChildForm;
        int cont = 0;
        int lx, ly;
        int sw, sh;
        DataContext _context = new DataContext();
        Funciones.Funciones funciones = new Funciones.Funciones();

        public frmInicio()
        {
            InitializeComponent();
        }

        #region Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        #endregion

        private async void frmInicio_Load(object sender, EventArgs e)
        {
            //TODO: Quitar esto es solo para forzar el login
            if (Program.Usuario == null)
                Program.Usuario = new User { Usuario= "Admin" };

            lblSesionUsuario.Text = Program.Usuario.Usuario;
            if (!_context.Configuraciones.Any())
            {                
                showSubMenu(panelSubMenuConfiguracion);
                ActivateButton(mnuSubConfiguracion, RGBColors.color3);
                OpenChildForm(new frmConfiguracionGeneral());
            }

            this.Opacity = 0.0;
            hideSubMenu();
            btnRestore.Visible = false;
            btnMaximize.Visible = true;
            timer1.Start();            
        }

        #region Menu
        private void mnuInicio_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
            hideSubMenu();
        }


        private void mnuArqueo_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new frmArqueo());
        }

        private void mnuCaja_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new frmCierreCaja());
            hideSubMenu();
        }

        private void mnuReporte_Click(object sender, EventArgs e)
        {
            if (panel1.Width <= 60)
            {
                panel1.Width = 280;
                mnuInicio.Text = "Inicio";
                mnuConfiguracion.Text = "Configuración";
                mnuEmpeños.Text = "Empeños";
                mnuClientes.Text = "Clientes";
                mnuReporte.Text = "Reportes";
                mnuCaja.Text = "Caja";
                mnuTablero.Text = "Tablero";
                mnuLogout.Text = "Cerrar";
            }
            Reset();
            showSubMenu(panelSubMenuReportes);
        }

        private async void mnuEmpeños_Click(object sender, EventArgs e)
        {
            var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
            if (configuracion==null)
            {
                MessageBox.Show("Para poder ingresar a este modulo primero debe configurar los datos generales del negocio en el modulo de configuración", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new frmEmpeno());
            hideSubMenu();
        }

        private void mnuEmpleados_Click(object sender, EventArgs e)
        {

            var oscuro = new frmOscuro();
            oscuro.Show();
            var frm = new frmClientes();
            frm.ShowDialog();
            oscuro.Close();
        }        

        private void mnuConfiguracion_Click(object sender, EventArgs e)
        {

            if (panel1.Width <= 60)
            {
                panel1.Width = 280;
                mnuInicio.Text = "Inicio";
                mnuConfiguracion.Text = "Configuración";
                mnuEmpeños.Text = "Empeños";
                mnuClientes.Text = "Clientes";
                mnuReporte.Text = "Reportes";
                mnuCaja.Text = "Caja";
                mnuTablero.Text = "Tablero";
                mnuLogout.Text = "Cerrar";
            }
            Reset();
            showSubMenu(panelSubMenuConfiguracion);
        }

        private void mnuSubConfiguracion_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Configuración"))
                return;           

            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new frmConfiguracionGeneral());
        }


        private void mnuReporteIngresos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new frmReporteIngresos());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new frmReporteEmpeños());
        }

        private void mnuSubIntereses_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Configuración"))
                return;

            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new frmIntereses());
        }
        #endregion       

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            cont += 1;
            if (cont == 100)
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

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Program.GetCargando(this.Size, this.Location);

            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);

            btnRestore.Visible = false;
            btnMaximize.Visible = true;

            Program.CargandoClose();
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
        
        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new frmTablero());
        }
     

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

        #region Funciones
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            panelSubMenuConfiguracion.Visible = false;
            panelSubMenuReportes.Visible = false;
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                iconModulo.IconChar = currentBtn.IconChar;
                iconModulo.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            Program.GetCargando(this.Size, this.Location);

            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(childForm);
            panelContenedor.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblModulo.Text = childForm.Text;

            Program.CargandoClose();
        }

        private void Reset()
        {
            DisableButton();

            iconModulo.IconChar = IconChar.Home;
            iconModulo.IconColor = Color.MediumPurple;
            lblModulo.Text = "Inicio";
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panel1.Width >= 220)
            {
                hideSubMenu();
                mnuInicio.Text = string.Empty;
                mnuConfiguracion.Text = string.Empty;
                mnuEmpeños.Text = string.Empty;
                mnuClientes.Text = string.Empty;
                mnuReporte.Text = string.Empty;
                mnuTablero.Text = string.Empty;
                mnuCaja.Text = string.Empty;
                panel1.Width = 50;
                //timerOcultar.Enabled = true;
                mnuLogout.Text = string.Empty;
                mnuLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            }
            else if (panel1.Width <= 50)
            {
                mnuInicio.Text = "Inicio";
                mnuConfiguracion.Text = "Configuración";
                mnuEmpeños.Text = "Empeños";
                mnuClientes.Text = "Clientes";
                mnuReporte.Text = "Reporte";
                mnuTablero.Text = "Tablero";
                mnuCaja.Text = "Caja";
                panel1.Width = 228;
                //timerMostrar.Enabled = true;
                mnuLogout.Text = "Cerrar";
                mnuLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        private void timerOcultar_Tick(object sender, EventArgs e)
        {
            if (panel1.Width <= 60)
            {

                timerOcultar.Enabled = false;
            }
            else
            {
                panel1.Width = panel1.Width - 20;
            }
        }

        private void timerMostrar_Tick(object sender, EventArgs e)
        {
            if (panel1.Width >= 280)
            {

                timerMostrar.Enabled = false;
            }
            else
            {
                panel1.Width = panel1.Width + 20;
            }
        }


        #region RenderSizeForm
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        private void mnuEmpleados_Click_1(object sender, EventArgs e)
        {

            if (!funciones.ValidatePIN("Empleado"))
                return;

            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new frmEmpleados());
            //hideSubMenu();
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Usuario = null;
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void mnuReporteArqueo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mnuReporteVencidos_Click(object sender, EventArgs e)
        {
            if (!funciones.ValidatePIN("Empeño"))
                return;

            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new frmVencidos());
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            bool listo = false;
            string maquina = Environment.MachineName;
            //SQLEXPRESS
            maquina = "DESKTOP-63NK2H8\\SQLSERVER";
            listo = Create(maquina, "Empeno", "C:\\Backup\\empeno.bak");
            if (listo)
            {
                MessageBox.Show("El BackUp se realizo de forma exitosa. Por favor revisar la carpeta C:\\Backup");
            }
            else
            {
                MessageBox.Show("Hubo un problema a la hora de generar el BackUp. Por favor contactarse con Amaru Systems para soporte.");
            }

        }

        public static Boolean Create(String p_server, String p_database, String p_backup_file)
        {
            Boolean inesem_ok = true;
            string sBackup = "BACKUP DATABASE " + p_database +
            " TO DISK = '" + p_backup_file + "'" +
            " WITH FORMAT, " +
            " MEDIANAME ='EmpenoFinal', NAME = 'CopiaBD ';";

            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = p_server;
            csb.InitialCatalog = "master";
            csb.IntegratedSecurity = true;
            using (SqlConnection con = new SqlConnection(csb.ConnectionString))
            {
                try

                {
                    con.Open();
                    SqlCommand cmdBackUp = new SqlCommand(sBackup, con);
                    cmdBackUp.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    inesem_ok = false;
                    con.Close();
                }
            }

            return inesem_ok;
        }

        private void btnBackup_Click_1(object sender, EventArgs e)
        {
            bool listo = false;
            string maquina = Environment.MachineName;
            //SQLEXPRESS
            maquina = "DESKTOP-63NK2H8\\SQLSERVER";
            listo = Create(maquina, "Empeno", "C:\\Backup\\empeno.bak");
            if (listo)
            {
                MessageBox.Show("El BackUp se realizo de forma exitosa. Por favor revisar la carpeta C:\\Backup");
            }
            else
            {
                MessageBox.Show("Hubo un problema a la hora de generar el BackUp. Por favor contactarse con Amaru Systems para soporte.");
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
            this.panelContenedor.Region = region;
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

        #endregion
    }
}
