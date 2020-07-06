using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    partial class frmInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }      
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicio));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSubMenuConfiguracion = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblModulo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnRestore = new FontAwesome.Sharp.IconPictureBox();
            this.btnMaximize = new FontAwesome.Sharp.IconPictureBox();
            this.btnMinimize = new FontAwesome.Sharp.IconPictureBox();
            this.btnClose = new FontAwesome.Sharp.IconPictureBox();
            this.iconModulo = new FontAwesome.Sharp.IconPictureBox();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.mnuSubIntereses = new FontAwesome.Sharp.IconButton();
            this.mnuSubConfiguracion = new FontAwesome.Sharp.IconButton();
            this.mnuConfiguracion = new FontAwesome.Sharp.IconButton();
            this.mnuEmpleados = new FontAwesome.Sharp.IconButton();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.mnuEmpeños = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.mnuInicio = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panelSubMenuConfiguracion.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.panel1.Controls.Add(this.iconButton5);
            this.panel1.Controls.Add(this.panelSubMenuConfiguracion);
            this.panel1.Controls.Add(this.mnuConfiguracion);
            this.panel1.Controls.Add(this.mnuEmpleados);
            this.panel1.Controls.Add(this.iconButton6);
            this.panel1.Controls.Add(this.mnuEmpeños);
            this.panel1.Controls.Add(this.iconButton2);
            this.panel1.Controls.Add(this.mnuInicio);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 900);
            this.panel1.TabIndex = 0;
            // 
            // panelSubMenuConfiguracion
            // 
            this.panelSubMenuConfiguracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(116)))));
            this.panelSubMenuConfiguracion.Controls.Add(this.mnuSubIntereses);
            this.panelSubMenuConfiguracion.Controls.Add(this.mnuSubConfiguracion);
            this.panelSubMenuConfiguracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubMenuConfiguracion.Location = new System.Drawing.Point(0, 584);
            this.panelSubMenuConfiguracion.Name = "panelSubMenuConfiguracion";
            this.panelSubMenuConfiguracion.Size = new System.Drawing.Size(312, 160);
            this.panelSubMenuConfiguracion.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.shapeContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 184);
            this.panel2.TabIndex = 0;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(312, 184);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.White;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 4;
            this.lineShape1.X2 = 307;
            this.lineShape1.Y1 = 163;
            this.lineShape1.Y2 = 163;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.panel3.Controls.Add(this.btnRestore);
            this.panel3.Controls.Add(this.btnMaximize);
            this.panel3.Controls.Add(this.btnMinimize);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.lblModulo);
            this.panel3.Controls.Add(this.iconModulo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(312, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1314, 80);
            this.panel3.TabIndex = 1;
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblModulo.Location = new System.Drawing.Point(58, 26);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(54, 19);
            this.lblModulo.TabIndex = 1;
            this.lblModulo.Text = "Inicio";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.panel);
            this.panelContenedor.Controls.Add(this.panel3);
            this.panelContenedor.Controls.Add(this.panel1);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1626, 900);
            this.panelContenedor.TabIndex = 3;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.pictureBox3);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(312, 80);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1314, 820);
            this.panel.TabIndex = 2;
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(392, 264);
            this.pictureBox3.MaximumSize = new System.Drawing.Size(450, 250);
            this.pictureBox3.MinimumSize = new System.Drawing.Size(400, 200);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(400, 200);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRestore.BackColor = System.Drawing.Color.Transparent;
            this.btnRestore.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.btnRestore.IconColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(1232, 12);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(32, 32);
            this.btnRestore.TabIndex = 5;
            this.btnRestore.TabStop = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMaximize.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.btnMaximize.IconColor = System.Drawing.Color.White;
            this.btnMaximize.Location = new System.Drawing.Point(1232, 12);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(32, 32);
            this.btnMaximize.TabIndex = 4;
            this.btnMaximize.TabStop = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinimize.IconColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(1194, 12);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(32, 32);
            this.btnMinimize.TabIndex = 3;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1270, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // iconModulo
            // 
            this.iconModulo.BackColor = System.Drawing.Color.Transparent;
            this.iconModulo.ForeColor = System.Drawing.Color.MediumPurple;
            this.iconModulo.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconModulo.IconColor = System.Drawing.Color.MediumPurple;
            this.iconModulo.Location = new System.Drawing.Point(20, 26);
            this.iconModulo.Name = "iconModulo";
            this.iconModulo.Size = new System.Drawing.Size(32, 32);
            this.iconModulo.TabIndex = 0;
            this.iconModulo.TabStop = false;
            // 
            // iconButton5
            // 
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton5.ForeColor = System.Drawing.Color.White;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.Cogs;
            this.iconButton5.IconColor = System.Drawing.Color.White;
            this.iconButton5.IconSize = 32;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(0, 744);
            this.iconButton5.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.iconButton5.Rotation = 0D;
            this.iconButton5.Size = new System.Drawing.Size(312, 80);
            this.iconButton5.TabIndex = 10;
            this.iconButton5.Text = "Caja";
            this.iconButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = true;
            // 
            // mnuSubIntereses
            // 
            this.mnuSubIntereses.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuSubIntereses.FlatAppearance.BorderSize = 0;
            this.mnuSubIntereses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuSubIntereses.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuSubIntereses.ForeColor = System.Drawing.Color.White;
            this.mnuSubIntereses.IconChar = FontAwesome.Sharp.IconChar.User;
            this.mnuSubIntereses.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(116)))));
            this.mnuSubIntereses.IconSize = 1;
            this.mnuSubIntereses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuSubIntereses.Location = new System.Drawing.Point(0, 80);
            this.mnuSubIntereses.Margin = new System.Windows.Forms.Padding(4);
            this.mnuSubIntereses.Name = "mnuSubIntereses";
            this.mnuSubIntereses.Padding = new System.Windows.Forms.Padding(75, 0, 0, 0);
            this.mnuSubIntereses.Rotation = 0D;
            this.mnuSubIntereses.Size = new System.Drawing.Size(312, 80);
            this.mnuSubIntereses.TabIndex = 10;
            this.mnuSubIntereses.Text = "Intereses";
            this.mnuSubIntereses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuSubIntereses.UseVisualStyleBackColor = true;
            this.mnuSubIntereses.Click += new System.EventHandler(this.mnuSubIntereses_Click);
            // 
            // mnuSubConfiguracion
            // 
            this.mnuSubConfiguracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuSubConfiguracion.FlatAppearance.BorderSize = 0;
            this.mnuSubConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuSubConfiguracion.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuSubConfiguracion.ForeColor = System.Drawing.Color.White;
            this.mnuSubConfiguracion.IconChar = FontAwesome.Sharp.IconChar.Cogs;
            this.mnuSubConfiguracion.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(116)))));
            this.mnuSubConfiguracion.IconSize = 1;
            this.mnuSubConfiguracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuSubConfiguracion.Location = new System.Drawing.Point(0, 0);
            this.mnuSubConfiguracion.Margin = new System.Windows.Forms.Padding(4);
            this.mnuSubConfiguracion.Name = "mnuSubConfiguracion";
            this.mnuSubConfiguracion.Padding = new System.Windows.Forms.Padding(75, 0, 0, 0);
            this.mnuSubConfiguracion.Rotation = 0D;
            this.mnuSubConfiguracion.Size = new System.Drawing.Size(312, 80);
            this.mnuSubConfiguracion.TabIndex = 9;
            this.mnuSubConfiguracion.Text = "Configuración General";
            this.mnuSubConfiguracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuSubConfiguracion.UseVisualStyleBackColor = true;
            this.mnuSubConfiguracion.Click += new System.EventHandler(this.mnuSubConfiguracion_Click);
            // 
            // mnuConfiguracion
            // 
            this.mnuConfiguracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuConfiguracion.FlatAppearance.BorderSize = 0;
            this.mnuConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuConfiguracion.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuConfiguracion.ForeColor = System.Drawing.Color.White;
            this.mnuConfiguracion.IconChar = FontAwesome.Sharp.IconChar.Cogs;
            this.mnuConfiguracion.IconColor = System.Drawing.Color.White;
            this.mnuConfiguracion.IconSize = 32;
            this.mnuConfiguracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuConfiguracion.Location = new System.Drawing.Point(0, 504);
            this.mnuConfiguracion.Margin = new System.Windows.Forms.Padding(4);
            this.mnuConfiguracion.Name = "mnuConfiguracion";
            this.mnuConfiguracion.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.mnuConfiguracion.Rotation = 0D;
            this.mnuConfiguracion.Size = new System.Drawing.Size(312, 80);
            this.mnuConfiguracion.TabIndex = 8;
            this.mnuConfiguracion.Text = " Configuración";
            this.mnuConfiguracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuConfiguracion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mnuConfiguracion.UseVisualStyleBackColor = true;
            this.mnuConfiguracion.Click += new System.EventHandler(this.mnuConfiguracion_Click);
            // 
            // mnuEmpleados
            // 
            this.mnuEmpleados.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuEmpleados.FlatAppearance.BorderSize = 0;
            this.mnuEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuEmpleados.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuEmpleados.ForeColor = System.Drawing.Color.White;
            this.mnuEmpleados.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.mnuEmpleados.IconColor = System.Drawing.Color.White;
            this.mnuEmpleados.IconSize = 32;
            this.mnuEmpleados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuEmpleados.Location = new System.Drawing.Point(0, 424);
            this.mnuEmpleados.Margin = new System.Windows.Forms.Padding(4);
            this.mnuEmpleados.Name = "mnuEmpleados";
            this.mnuEmpleados.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.mnuEmpleados.Rotation = 0D;
            this.mnuEmpleados.Size = new System.Drawing.Size(312, 80);
            this.mnuEmpleados.TabIndex = 7;
            this.mnuEmpleados.Text = " Empleados";
            this.mnuEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuEmpleados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mnuEmpleados.UseVisualStyleBackColor = true;
            this.mnuEmpleados.Click += new System.EventHandler(this.mnuEmpleados_Click);
            // 
            // iconButton6
            // 
            this.iconButton6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.iconButton6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButton6.FlatAppearance.BorderSize = 0;
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton6.ForeColor = System.Drawing.Color.White;
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.iconButton6.IconColor = System.Drawing.Color.White;
            this.iconButton6.IconSize = 32;
            this.iconButton6.Location = new System.Drawing.Point(0, 860);
            this.iconButton6.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.iconButton6.Rotation = 0D;
            this.iconButton6.Size = new System.Drawing.Size(312, 40);
            this.iconButton6.TabIndex = 6;
            this.iconButton6.Text = "Cerrar";
            this.iconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton6.UseVisualStyleBackColor = false;
            // 
            // mnuEmpeños
            // 
            this.mnuEmpeños.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuEmpeños.FlatAppearance.BorderSize = 0;
            this.mnuEmpeños.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuEmpeños.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuEmpeños.ForeColor = System.Drawing.Color.White;
            this.mnuEmpeños.IconChar = FontAwesome.Sharp.IconChar.DiceD20;
            this.mnuEmpeños.IconColor = System.Drawing.Color.White;
            this.mnuEmpeños.IconSize = 32;
            this.mnuEmpeños.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuEmpeños.Location = new System.Drawing.Point(0, 344);
            this.mnuEmpeños.Margin = new System.Windows.Forms.Padding(4);
            this.mnuEmpeños.Name = "mnuEmpeños";
            this.mnuEmpeños.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.mnuEmpeños.Rotation = 0D;
            this.mnuEmpeños.Size = new System.Drawing.Size(312, 80);
            this.mnuEmpeños.TabIndex = 3;
            this.mnuEmpeños.Text = " Empeños";
            this.mnuEmpeños.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuEmpeños.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mnuEmpeños.UseVisualStyleBackColor = true;
            this.mnuEmpeños.Click += new System.EventHandler(this.mnuEmpeños_Click);
            // 
            // iconButton2
            // 
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.ChartArea;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconSize = 32;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 264);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.iconButton2.Rotation = 0D;
            this.iconButton2.Size = new System.Drawing.Size(312, 80);
            this.iconButton2.TabIndex = 2;
            this.iconButton2.Text = " Tablero";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = true;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // mnuInicio
            // 
            this.mnuInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuInicio.FlatAppearance.BorderSize = 0;
            this.mnuInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mnuInicio.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mnuInicio.ForeColor = System.Drawing.Color.White;
            this.mnuInicio.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.mnuInicio.IconColor = System.Drawing.Color.White;
            this.mnuInicio.IconSize = 32;
            this.mnuInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuInicio.Location = new System.Drawing.Point(0, 184);
            this.mnuInicio.Margin = new System.Windows.Forms.Padding(4);
            this.mnuInicio.Name = "mnuInicio";
            this.mnuInicio.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.mnuInicio.Rotation = 0D;
            this.mnuInicio.Size = new System.Drawing.Size(312, 80);
            this.mnuInicio.TabIndex = 1;
            this.mnuInicio.Text = " Inicio";
            this.mnuInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mnuInicio.UseVisualStyleBackColor = true;
            this.mnuInicio.Click += new System.EventHandler(this.mnuInicio_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(23, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(269, 143);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(753, 354);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(430, 230);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1626, 900);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInicio";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmInicio_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panelSubMenuConfiguracion.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton mnuEmpeños;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton mnuInicio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconPictureBox btnMinimize;
        private FontAwesome.Sharp.IconPictureBox btnClose;
        private System.Windows.Forms.Label lblModulo;
        private FontAwesome.Sharp.IconPictureBox iconModulo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private FontAwesome.Sharp.IconButton iconButton6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Panel panel;
        private FontAwesome.Sharp.IconPictureBox btnMaximize;
        private FontAwesome.Sharp.IconPictureBox btnRestore;
        private System.Windows.Forms.PictureBox pictureBox3;
        private FontAwesome.Sharp.IconButton mnuConfiguracion;
        private FontAwesome.Sharp.IconButton mnuEmpleados;
        private FontAwesome.Sharp.IconButton iconButton5;
        private Panel panelSubMenuConfiguracion;
        private FontAwesome.Sharp.IconButton mnuSubIntereses;
        private FontAwesome.Sharp.IconButton mnuSubConfiguracion;
    }
}