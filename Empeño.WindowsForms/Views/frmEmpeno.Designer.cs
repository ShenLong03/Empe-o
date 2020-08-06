﻿using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    partial class frmEmpeno
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
                _context.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpeno));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.panel17 = new System.Windows.Forms.Panel();
            this.btnEmpeñar = new FontAwesome.Sharp.IconButton();
            this.btnClienteNuevo = new FontAwesome.Sharp.IconButton();
            this.btnVerEmpleado = new FontAwesome.Sharp.IconButton();
            this.btnEditar = new FontAwesome.Sharp.IconButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel21 = new System.Windows.Forms.Panel();
            this.dgvEmpeños = new System.Windows.Forms.DataGridView();
            this.panel20 = new System.Windows.Forms.Panel();
            this.lblCatidadEmpeños = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel23 = new System.Windows.Forms.Panel();
            this.btnReimprimir = new FontAwesome.Sharp.IconButton();
            this.btnHoy = new FontAwesome.Sharp.IconButton();
            this.btnPendientes = new FontAwesome.Sharp.IconButton();
            this.btnVer = new FontAwesome.Sharp.IconButton();
            this.btnEditarEmpeño = new FontAwesome.Sharp.IconButton();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblVence = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.lblNumeroEmpeño = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.iconButton10 = new FontAwesome.Sharp.IconButton();
            this.btnGuardarEmpeño = new FontAwesome.Sharp.IconButton();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.Fecha = new System.Windows.Forms.TextBox();
            this.btnIntereses = new FontAwesome.Sharp.IconButton();
            this.btnPagos = new FontAwesome.Sharp.IconButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnVerPago = new FontAwesome.Sharp.IconButton();
            this.btnEditarPago = new FontAwesome.Sharp.IconButton();
            this.btnEliminarPago = new FontAwesome.Sharp.IconButton();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.cbInteres = new System.Windows.Forms.ComboBox();
            this.lblInteres = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.lblComentario = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblIdentificacion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.Realizado = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIdentificacion = new FontAwesome.Sharp.IconPictureBox();
            this.btnNewCustomer = new FontAwesome.Sharp.IconPictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.chbEsOro = new System.Windows.Forms.CheckBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.panelBuscar = new System.Windows.Forms.Panel();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.elipseEstado = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btnReimprimirPago = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.panel17.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpeños)).BeginInit();
            this.panel20.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIdentificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panelBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPrincipal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1478, 922);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPrincipal.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.RowCount = 2;
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPrincipal.Size = new System.Drawing.Size(873, 916);
            this.tableLayoutPrincipal.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 406);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel16, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel22, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5, 5, 15, 15);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(867, 406);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.panel5);
            this.panel16.Controls.Add(this.panel4);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(8, 8);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(841, 330);
            this.panel16.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.panel18);
            this.panel5.Controls.Add(this.panel17);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 40);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(841, 290);
            this.panel5.TabIndex = 1;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.dgvClientes);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(0, 50);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(841, 240);
            this.panel18.TabIndex = 37;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientes.ColumnHeadersHeight = 60;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClientes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.EnableHeadersVisualStyles = false;
            this.dgvClientes.GridColor = System.Drawing.SystemColors.Control;
            this.dgvClientes.Location = new System.Drawing.Point(0, 0);
            this.dgvClientes.Margin = new System.Windows.Forms.Padding(4);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvClientes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvClientes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvClientes.RowTemplate.DividerHeight = 1;
            this.dgvClientes.RowTemplate.Height = 45;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(841, 240);
            this.dgvClientes.TabIndex = 21;
            this.dgvClientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellContentClick);
            this.dgvClientes.DoubleClick += new System.EventHandler(this.dgvClientes_DoubleClick);
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.btnEmpeñar);
            this.panel17.Controls.Add(this.btnClienteNuevo);
            this.panel17.Controls.Add(this.btnVerEmpleado);
            this.panel17.Controls.Add(this.btnEditar);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel17.Size = new System.Drawing.Size(841, 50);
            this.panel17.TabIndex = 36;
            // 
            // btnEmpeñar
            // 
            this.btnEmpeñar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEmpeñar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnEmpeñar.FlatAppearance.BorderSize = 0;
            this.btnEmpeñar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpeñar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEmpeñar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpeñar.ForeColor = System.Drawing.Color.White;
            this.btnEmpeñar.IconChar = FontAwesome.Sharp.IconChar.DiceD20;
            this.btnEmpeñar.IconColor = System.Drawing.Color.White;
            this.btnEmpeñar.IconSize = 24;
            this.btnEmpeñar.Location = new System.Drawing.Point(1, 9);
            this.btnEmpeñar.Name = "btnEmpeñar";
            this.btnEmpeñar.Rotation = 0D;
            this.btnEmpeñar.Size = new System.Drawing.Size(100, 40);
            this.btnEmpeñar.TabIndex = 17;
            this.btnEmpeñar.Text = "Empeñar";
            this.btnEmpeñar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEmpeñar.UseVisualStyleBackColor = false;
            this.btnEmpeñar.Click += new System.EventHandler(this.btnEmpeñar_Click_2);
            // 
            // btnClienteNuevo
            // 
            this.btnClienteNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClienteNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnClienteNuevo.FlatAppearance.BorderSize = 0;
            this.btnClienteNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClienteNuevo.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnClienteNuevo.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnClienteNuevo.ForeColor = System.Drawing.Color.White;
            this.btnClienteNuevo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnClienteNuevo.IconColor = System.Drawing.Color.White;
            this.btnClienteNuevo.IconSize = 24;
            this.btnClienteNuevo.Location = new System.Drawing.Point(301, 8);
            this.btnClienteNuevo.Name = "btnClienteNuevo";
            this.btnClienteNuevo.Rotation = 0D;
            this.btnClienteNuevo.Size = new System.Drawing.Size(100, 41);
            this.btnClienteNuevo.TabIndex = 20;
            this.btnClienteNuevo.Text = "Nuevo";
            this.btnClienteNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClienteNuevo.UseVisualStyleBackColor = false;
            this.btnClienteNuevo.Click += new System.EventHandler(this.btnClienteNuevo_Click);
            // 
            // btnVerEmpleado
            // 
            this.btnVerEmpleado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnVerEmpleado.FlatAppearance.BorderSize = 0;
            this.btnVerEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerEmpleado.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnVerEmpleado.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnVerEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnVerEmpleado.IconChar = FontAwesome.Sharp.IconChar.Binoculars;
            this.btnVerEmpleado.IconColor = System.Drawing.Color.White;
            this.btnVerEmpleado.IconSize = 24;
            this.btnVerEmpleado.Location = new System.Drawing.Point(201, 9);
            this.btnVerEmpleado.Name = "btnVerEmpleado";
            this.btnVerEmpleado.Rotation = 0D;
            this.btnVerEmpleado.Size = new System.Drawing.Size(100, 40);
            this.btnVerEmpleado.TabIndex = 19;
            this.btnVerEmpleado.Text = "Ver";
            this.btnVerEmpleado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVerEmpleado.UseVisualStyleBackColor = false;
            this.btnVerEmpleado.Click += new System.EventHandler(this.btnVerEmpleado_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEditar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnEditar.IconColor = System.Drawing.Color.White;
            this.btnEditar.IconSize = 24;
            this.btnEditar.Location = new System.Drawing.Point(101, 8);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Rotation = 0D;
            this.btnEditar.Size = new System.Drawing.Size(100, 41);
            this.btnEditar.TabIndex = 18;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(841, 40);
            this.panel4.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "Clientes";
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.lblCantidad);
            this.panel22.Controls.Add(this.label11);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel22.Location = new System.Drawing.Point(8, 344);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(841, 44);
            this.panel22.TabIndex = 2;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(60, 9);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(28, 21);
            this.lblCantidad.TabIndex = 4;
            this.lblCantidad.Text = "00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 3;
            this.label11.Text = "Total : ";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 415);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 7, 15, 15);
            this.panel2.Size = new System.Drawing.Size(867, 498);
            this.panel2.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.tableLayoutPanel3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(7, 47);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(845, 436);
            this.panel6.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel21, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(845, 436);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pictureBox2);
            this.panel7.Controls.Add(this.comboBox1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(839, 44);
            this.panel7.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Location = new System.Drawing.Point(0, 41);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(839, 1);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.DimGray;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Activo, Pendiente, Vencido",
            "Cancelada",
            "Retiradas",
            "Perdidos",
            "Todo"});
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(839, 41);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.Text = "Activos, Pendientes, Vencidos";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.dgvEmpeños);
            this.panel21.Controls.Add(this.panel20);
            this.panel21.Controls.Add(this.panel23);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(3, 53);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(839, 380);
            this.panel21.TabIndex = 2;
            // 
            // dgvEmpeños
            // 
            this.dgvEmpeños.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpeños.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvEmpeños.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpeños.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvEmpeños.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpeños.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvEmpeños.ColumnHeadersHeight = 60;
            this.dgvEmpeños.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmpeños.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvEmpeños.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpeños.EnableHeadersVisualStyles = false;
            this.dgvEmpeños.GridColor = System.Drawing.SystemColors.Control;
            this.dgvEmpeños.Location = new System.Drawing.Point(0, 50);
            this.dgvEmpeños.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEmpeños.Name = "dgvEmpeños";
            this.dgvEmpeños.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpeños.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvEmpeños.RowHeadersVisible = false;
            this.dgvEmpeños.RowHeadersWidth = 51;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.dgvEmpeños.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvEmpeños.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvEmpeños.RowTemplate.DividerHeight = 1;
            this.dgvEmpeños.RowTemplate.Height = 45;
            this.dgvEmpeños.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpeños.Size = new System.Drawing.Size(839, 280);
            this.dgvEmpeños.TabIndex = 27;
            this.dgvEmpeños.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmpeños_CellFormatting);
            this.dgvEmpeños.DoubleClick += new System.EventHandler(this.dgvEmpeños_DoubleClick);
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.lblCatidadEmpeños);
            this.panel20.Controls.Add(this.label14);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel20.Location = new System.Drawing.Point(0, 330);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(839, 50);
            this.panel20.TabIndex = 39;
            // 
            // lblCatidadEmpeños
            // 
            this.lblCatidadEmpeños.AutoSize = true;
            this.lblCatidadEmpeños.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatidadEmpeños.Location = new System.Drawing.Point(63, 11);
            this.lblCatidadEmpeños.Name = "lblCatidadEmpeños";
            this.lblCatidadEmpeños.Size = new System.Drawing.Size(28, 21);
            this.lblCatidadEmpeños.TabIndex = 6;
            this.lblCatidadEmpeños.Text = "00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 19);
            this.label14.TabIndex = 5;
            this.label14.Text = "Total : ";
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.btnReimprimir);
            this.panel23.Controls.Add(this.btnHoy);
            this.panel23.Controls.Add(this.btnPendientes);
            this.panel23.Controls.Add(this.btnVer);
            this.panel23.Controls.Add(this.btnEditarEmpeño);
            this.panel23.Controls.Add(this.btnEliminar);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel23.Size = new System.Drawing.Size(839, 50);
            this.panel23.TabIndex = 37;
            // 
            // btnReimprimir
            // 
            this.btnReimprimir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReimprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnReimprimir.FlatAppearance.BorderSize = 0;
            this.btnReimprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReimprimir.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnReimprimir.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnReimprimir.ForeColor = System.Drawing.Color.White;
            this.btnReimprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnReimprimir.IconColor = System.Drawing.Color.White;
            this.btnReimprimir.IconSize = 24;
            this.btnReimprimir.Location = new System.Drawing.Point(511, 8);
            this.btnReimprimir.Name = "btnReimprimir";
            this.btnReimprimir.Rotation = 0D;
            this.btnReimprimir.Size = new System.Drawing.Size(121, 41);
            this.btnReimprimir.TabIndex = 28;
            this.btnReimprimir.Text = "Reimprimir";
            this.btnReimprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReimprimir.UseVisualStyleBackColor = false;
            this.btnReimprimir.Click += new System.EventHandler(this.btnReimprimir_Click);
            // 
            // btnHoy
            // 
            this.btnHoy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHoy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnHoy.FlatAppearance.BorderSize = 0;
            this.btnHoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoy.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnHoy.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnHoy.ForeColor = System.Drawing.Color.White;
            this.btnHoy.IconChar = FontAwesome.Sharp.IconChar.Calendar;
            this.btnHoy.IconColor = System.Drawing.Color.White;
            this.btnHoy.IconSize = 24;
            this.btnHoy.Location = new System.Drawing.Point(299, 8);
            this.btnHoy.Name = "btnHoy";
            this.btnHoy.Rotation = 0D;
            this.btnHoy.Size = new System.Drawing.Size(100, 41);
            this.btnHoy.TabIndex = 26;
            this.btnHoy.Text = "Hoy";
            this.btnHoy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHoy.UseVisualStyleBackColor = false;
            this.btnHoy.Click += new System.EventHandler(this.btnHoy_Click);
            // 
            // btnPendientes
            // 
            this.btnPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPendientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnPendientes.FlatAppearance.BorderSize = 0;
            this.btnPendientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendientes.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnPendientes.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnPendientes.ForeColor = System.Drawing.Color.White;
            this.btnPendientes.IconChar = FontAwesome.Sharp.IconChar.Clock;
            this.btnPendientes.IconColor = System.Drawing.Color.White;
            this.btnPendientes.IconSize = 24;
            this.btnPendientes.Location = new System.Drawing.Point(399, 9);
            this.btnPendientes.Name = "btnPendientes";
            this.btnPendientes.Rotation = 0D;
            this.btnPendientes.Size = new System.Drawing.Size(115, 40);
            this.btnPendientes.TabIndex = 27;
            this.btnPendientes.Text = "Pendientes";
            this.btnPendientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPendientes.UseVisualStyleBackColor = false;
            this.btnPendientes.Click += new System.EventHandler(this.btnPendientes_Click);
            // 
            // btnVer
            // 
            this.btnVer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnVer.FlatAppearance.BorderSize = 0;
            this.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVer.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnVer.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnVer.ForeColor = System.Drawing.Color.White;
            this.btnVer.IconChar = FontAwesome.Sharp.IconChar.Binoculars;
            this.btnVer.IconColor = System.Drawing.Color.White;
            this.btnVer.IconSize = 24;
            this.btnVer.Location = new System.Drawing.Point(100, 8);
            this.btnVer.Name = "btnVer";
            this.btnVer.Rotation = 0D;
            this.btnVer.Size = new System.Drawing.Size(100, 41);
            this.btnVer.TabIndex = 24;
            this.btnVer.Text = "Ver";
            this.btnVer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnEditarEmpeño
            // 
            this.btnEditarEmpeño.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarEmpeño.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnEditarEmpeño.FlatAppearance.BorderSize = 0;
            this.btnEditarEmpeño.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarEmpeño.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEditarEmpeño.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEditarEmpeño.ForeColor = System.Drawing.Color.White;
            this.btnEditarEmpeño.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnEditarEmpeño.IconColor = System.Drawing.Color.White;
            this.btnEditarEmpeño.IconSize = 24;
            this.btnEditarEmpeño.Location = new System.Drawing.Point(0, 9);
            this.btnEditarEmpeño.Name = "btnEditarEmpeño";
            this.btnEditarEmpeño.Rotation = 0D;
            this.btnEditarEmpeño.Size = new System.Drawing.Size(100, 40);
            this.btnEditarEmpeño.TabIndex = 23;
            this.btnEditarEmpeño.Text = "Editar";
            this.btnEditarEmpeño.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditarEmpeño.UseVisualStyleBackColor = false;
            this.btnEditarEmpeño.Click += new System.EventHandler(this.btnEditarEmpeño_Click_1);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEliminar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.btnEliminar.IconColor = System.Drawing.Color.White;
            this.btnEliminar.IconSize = 24;
            this.btnEliminar.Location = new System.Drawing.Point(200, 9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Rotation = 0D;
            this.btnEliminar.Size = new System.Drawing.Size(100, 40);
            this.btnEliminar.TabIndex = 25;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.panel3.Controls.Add(this.label10);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(7, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(845, 40);
            this.panel3.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(9, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 19);
            this.label10.TabIndex = 1;
            this.label10.Text = "Empeños";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(889, 3);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(579, 916);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(573, 910);
            this.panel8.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.tableLayoutPanel5);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(573, 910);
            this.panel10.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.panel11, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(5, 5, 15, 5);
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(573, 910);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.panel11.Controls.Add(this.lblVence);
            this.panel11.Controls.Add(this.lblEstado);
            this.panel11.Controls.Add(this.label7);
            this.panel11.Controls.Add(this.iconPictureBox2);
            this.panel11.Controls.Add(this.lblNumeroEmpeño);
            this.panel11.Controls.Add(this.label1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(8, 8);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(5, 5, 45, 5);
            this.panel11.Size = new System.Drawing.Size(547, 34);
            this.panel11.TabIndex = 0;
            // 
            // lblVence
            // 
            this.lblVence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.lblVence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblVence.Enabled = false;
            this.lblVence.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVence.ForeColor = System.Drawing.Color.White;
            this.lblVence.Location = new System.Drawing.Point(441, 11);
            this.lblVence.Name = "lblVence";
            this.lblVence.Size = new System.Drawing.Size(100, 21);
            this.lblVence.TabIndex = 18;
            this.lblVence.Text = "03/07/2020";
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblEstado.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.lblEstado.Location = new System.Drawing.Point(235, 3);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(4);
            this.lblEstado.Size = new System.Drawing.Size(70, 27);
            this.lblEstado.TabIndex = 5;
            this.lblEstado.Text = "Activo";
            this.lblEstado.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(368, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 19);
            this.label7.TabIndex = 4;
            this.label7.Text = "Vence";
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Pager;
            this.iconPictureBox2.IconColor = System.Drawing.Color.White;
            this.iconPictureBox2.IconSize = 20;
            this.iconPictureBox2.Location = new System.Drawing.Point(9, 10);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(34, 20);
            this.iconPictureBox2.TabIndex = 2;
            this.iconPictureBox2.TabStop = false;
            // 
            // lblNumeroEmpeño
            // 
            this.lblNumeroEmpeño.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumeroEmpeño.AutoSize = true;
            this.lblNumeroEmpeño.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblNumeroEmpeño.ForeColor = System.Drawing.Color.White;
            this.lblNumeroEmpeño.Location = new System.Drawing.Point(139, 10);
            this.lblNumeroEmpeño.Name = "lblNumeroEmpeño";
            this.lblNumeroEmpeño.Size = new System.Drawing.Size(29, 19);
            this.lblNumeroEmpeño.TabIndex = 1;
            this.lblNumeroEmpeño.Text = "00";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empeño #";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.panel13, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.panelFormulario, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(8, 48);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(547, 854);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.AutoScroll = true;
            this.panel13.Controls.Add(this.btnCancelar);
            this.panel13.Controls.Add(this.iconButton10);
            this.panel13.Controls.Add(this.btnGuardarEmpeño);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 802);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(541, 49);
            this.panel13.TabIndex = 3;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnCancelar.IconColor = System.Drawing.Color.White;
            this.btnCancelar.IconSize = 24;
            this.btnCancelar.Location = new System.Drawing.Point(105, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Rotation = 0D;
            this.btnCancelar.Size = new System.Drawing.Size(105, 41);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // iconButton10
            // 
            this.iconButton10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButton10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(187)))), ((int)(((byte)(173)))));
            this.iconButton10.FlatAppearance.BorderSize = 0;
            this.iconButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton10.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton10.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.iconButton10.ForeColor = System.Drawing.Color.White;
            this.iconButton10.IconChar = FontAwesome.Sharp.IconChar.Donate;
            this.iconButton10.IconColor = System.Drawing.Color.White;
            this.iconButton10.IconSize = 24;
            this.iconButton10.Location = new System.Drawing.Point(210, 6);
            this.iconButton10.Name = "iconButton10";
            this.iconButton10.Rotation = 0D;
            this.iconButton10.Size = new System.Drawing.Size(105, 40);
            this.iconButton10.TabIndex = 16;
            this.iconButton10.Text = "Pagar";
            this.iconButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton10.UseVisualStyleBackColor = false;
            this.iconButton10.Click += new System.EventHandler(this.iconButton10_Click);
            // 
            // btnGuardarEmpeño
            // 
            this.btnGuardarEmpeño.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardarEmpeño.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.btnGuardarEmpeño.FlatAppearance.BorderSize = 0;
            this.btnGuardarEmpeño.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarEmpeño.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnGuardarEmpeño.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnGuardarEmpeño.ForeColor = System.Drawing.Color.White;
            this.btnGuardarEmpeño.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnGuardarEmpeño.IconColor = System.Drawing.Color.White;
            this.btnGuardarEmpeño.IconSize = 24;
            this.btnGuardarEmpeño.Location = new System.Drawing.Point(5, 6);
            this.btnGuardarEmpeño.Name = "btnGuardarEmpeño";
            this.btnGuardarEmpeño.Rotation = 0D;
            this.btnGuardarEmpeño.Size = new System.Drawing.Size(100, 40);
            this.btnGuardarEmpeño.TabIndex = 14;
            this.btnGuardarEmpeño.Text = "Guardar";
            this.btnGuardarEmpeño.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarEmpeño.UseVisualStyleBackColor = false;
            this.btnGuardarEmpeño.Click += new System.EventHandler(this.btnGuardarEmpeño_Click_1);
            // 
            // panelFormulario
            // 
            this.panelFormulario.AutoScroll = true;
            this.panelFormulario.Controls.Add(this.btnReimprimirPago);
            this.panelFormulario.Controls.Add(this.Fecha);
            this.panelFormulario.Controls.Add(this.btnIntereses);
            this.panelFormulario.Controls.Add(this.btnPagos);
            this.panelFormulario.Controls.Add(this.panel9);
            this.panelFormulario.Controls.Add(this.btnVerPago);
            this.panelFormulario.Controls.Add(this.btnEditarPago);
            this.panelFormulario.Controls.Add(this.btnEliminarPago);
            this.panelFormulario.Controls.Add(this.dgvPagos);
            this.panelFormulario.Controls.Add(this.pictureBox8);
            this.panelFormulario.Controls.Add(this.cbInteres);
            this.panelFormulario.Controls.Add(this.lblInteres);
            this.panelFormulario.Controls.Add(this.lblMonto);
            this.panelFormulario.Controls.Add(this.lblComentario);
            this.panelFormulario.Controls.Add(this.lblDescripcion);
            this.panelFormulario.Controls.Add(this.lblNombre);
            this.panelFormulario.Controls.Add(this.lblIdentificacion);
            this.panelFormulario.Controls.Add(this.label9);
            this.panelFormulario.Controls.Add(this.pictureBox7);
            this.panelFormulario.Controls.Add(this.txtMonto);
            this.panelFormulario.Controls.Add(this.Realizado);
            this.panelFormulario.Controls.Add(this.label3);
            this.panelFormulario.Controls.Add(this.btnIdentificacion);
            this.panelFormulario.Controls.Add(this.btnNewCustomer);
            this.panelFormulario.Controls.Add(this.pictureBox6);
            this.panelFormulario.Controls.Add(this.pictureBox5);
            this.panelFormulario.Controls.Add(this.pictureBox4);
            this.panelFormulario.Controls.Add(this.pictureBox3);
            this.panelFormulario.Controls.Add(this.chbEsOro);
            this.panelFormulario.Controls.Add(this.txtComentario);
            this.panelFormulario.Controls.Add(this.txtDescripcion);
            this.panelFormulario.Controls.Add(this.txtNombre);
            this.panelFormulario.Controls.Add(this.txtIdentificacion);
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormulario.Location = new System.Drawing.Point(3, 3);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(541, 793);
            this.panelFormulario.TabIndex = 2;
            this.panelFormulario.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFormulario_Paint);
            // 
            // Fecha
            // 
            this.Fecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fecha.BackColor = System.Drawing.Color.White;
            this.Fecha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Fecha.Enabled = false;
            this.Fecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha.ForeColor = System.Drawing.Color.Black;
            this.Fecha.Location = new System.Drawing.Point(378, 469);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(129, 25);
            this.Fecha.TabIndex = 17;
            this.Fecha.Text = "03/07/2020";
            // 
            // btnIntereses
            // 
            this.btnIntereses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIntereses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.btnIntereses.FlatAppearance.BorderSize = 0;
            this.btnIntereses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntereses.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnIntereses.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnIntereses.ForeColor = System.Drawing.Color.White;
            this.btnIntereses.IconChar = FontAwesome.Sharp.IconChar.Percentage;
            this.btnIntereses.IconColor = System.Drawing.Color.White;
            this.btnIntereses.IconSize = 24;
            this.btnIntereses.Location = new System.Drawing.Point(506, 533);
            this.btnIntereses.Name = "btnIntereses";
            this.btnIntereses.Rotation = 0D;
            this.btnIntereses.Size = new System.Drawing.Size(31, 30);
            this.btnIntereses.TabIndex = 50;
            this.btnIntereses.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIntereses.UseVisualStyleBackColor = false;
            this.btnIntereses.Click += new System.EventHandler(this.btnIntereses_Click);
            // 
            // btnPagos
            // 
            this.btnPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagos.BackColor = System.Drawing.Color.DimGray;
            this.btnPagos.FlatAppearance.BorderSize = 0;
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagos.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnPagos.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnPagos.ForeColor = System.Drawing.Color.White;
            this.btnPagos.IconChar = FontAwesome.Sharp.IconChar.Coins;
            this.btnPagos.IconColor = System.Drawing.Color.White;
            this.btnPagos.IconSize = 24;
            this.btnPagos.Location = new System.Drawing.Point(475, 533);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Rotation = 0D;
            this.btnPagos.Size = new System.Drawing.Size(31, 30);
            this.btnPagos.TabIndex = 49;
            this.btnPagos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagos.UseVisualStyleBackColor = false;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.DimGray;
            this.panel9.Location = new System.Drawing.Point(7, 512);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(524, 1);
            this.panel9.TabIndex = 48;
            // 
            // btnVerPago
            // 
            this.btnVerPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.btnVerPago.FlatAppearance.BorderSize = 0;
            this.btnVerPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerPago.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnVerPago.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnVerPago.ForeColor = System.Drawing.Color.White;
            this.btnVerPago.IconChar = FontAwesome.Sharp.IconChar.Binoculars;
            this.btnVerPago.IconColor = System.Drawing.Color.White;
            this.btnVerPago.IconSize = 24;
            this.btnVerPago.Location = new System.Drawing.Point(104, 522);
            this.btnVerPago.Name = "btnVerPago";
            this.btnVerPago.Rotation = 0D;
            this.btnVerPago.Size = new System.Drawing.Size(100, 41);
            this.btnVerPago.TabIndex = 46;
            this.btnVerPago.Text = "Ver";
            this.btnVerPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVerPago.UseVisualStyleBackColor = false;
            // 
            // btnEditarPago
            // 
            this.btnEditarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnEditarPago.FlatAppearance.BorderSize = 0;
            this.btnEditarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarPago.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEditarPago.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEditarPago.ForeColor = System.Drawing.Color.White;
            this.btnEditarPago.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnEditarPago.IconColor = System.Drawing.Color.White;
            this.btnEditarPago.IconSize = 24;
            this.btnEditarPago.Location = new System.Drawing.Point(4, 523);
            this.btnEditarPago.Name = "btnEditarPago";
            this.btnEditarPago.Rotation = 0D;
            this.btnEditarPago.Size = new System.Drawing.Size(100, 40);
            this.btnEditarPago.TabIndex = 45;
            this.btnEditarPago.Text = "Editar";
            this.btnEditarPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditarPago.UseVisualStyleBackColor = false;
            this.btnEditarPago.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // btnEliminarPago
            // 
            this.btnEliminarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            this.btnEliminarPago.FlatAppearance.BorderSize = 0;
            this.btnEliminarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarPago.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEliminarPago.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnEliminarPago.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPago.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.btnEliminarPago.IconColor = System.Drawing.Color.White;
            this.btnEliminarPago.IconSize = 24;
            this.btnEliminarPago.Location = new System.Drawing.Point(204, 523);
            this.btnEliminarPago.Name = "btnEliminarPago";
            this.btnEliminarPago.Rotation = 0D;
            this.btnEliminarPago.Size = new System.Drawing.Size(100, 40);
            this.btnEliminarPago.TabIndex = 47;
            this.btnEliminarPago.Text = "Eliminar";
            this.btnEliminarPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminarPago.UseVisualStyleBackColor = false;
            this.btnEliminarPago.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // dgvPagos
            // 
            this.dgvPagos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPagos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvPagos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPagos.ColumnHeadersHeight = 60;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagos.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPagos.EnableHeadersVisualStyles = false;
            this.dgvPagos.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPagos.Location = new System.Drawing.Point(3, 567);
            this.dgvPagos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagos.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvPagos.RowHeadersVisible = false;
            this.dgvPagos.RowHeadersWidth = 51;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            this.dgvPagos.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvPagos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvPagos.RowTemplate.DividerHeight = 1;
            this.dgvPagos.RowTemplate.Height = 45;
            this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagos.Size = new System.Drawing.Size(538, 222);
            this.dgvPagos.TabIndex = 43;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox8.BackgroundImage")));
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(8, 422);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(520, 1);
            this.pictureBox8.TabIndex = 34;
            this.pictureBox8.TabStop = false;
            // 
            // cbInteres
            // 
            this.cbInteres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInteres.BackColor = System.Drawing.SystemColors.Control;
            this.cbInteres.Enabled = false;
            this.cbInteres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInteres.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cbInteres.ForeColor = System.Drawing.Color.LightGray;
            this.cbInteres.FormattingEnabled = true;
            this.cbInteres.Location = new System.Drawing.Point(7, 385);
            this.cbInteres.Name = "cbInteres";
            this.cbInteres.Size = new System.Drawing.Size(519, 37);
            this.cbInteres.TabIndex = 12;
            this.cbInteres.Text = "Porcentaje";
            this.cbInteres.SelectedIndexChanged += new System.EventHandler(this.cbInteres_SelectedIndexChanged_1);
            this.cbInteres.Enter += new System.EventHandler(this.cbInteres_Enter);
            this.cbInteres.Leave += new System.EventHandler(this.cbInteres_Leave);
            // 
            // lblInteres
            // 
            this.lblInteres.AutoSize = true;
            this.lblInteres.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInteres.ForeColor = System.Drawing.Color.DimGray;
            this.lblInteres.Location = new System.Drawing.Point(3, 366);
            this.lblInteres.Name = "lblInteres";
            this.lblInteres.Size = new System.Drawing.Size(97, 19);
            this.lblInteres.TabIndex = 11;
            this.lblInteres.Text = "Porcentaje";
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.ForeColor = System.Drawing.Color.DimGray;
            this.lblMonto.Location = new System.Drawing.Point(3, 314);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(60, 19);
            this.lblMonto.TabIndex = 9;
            this.lblMonto.Text = "Monto";
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentario.ForeColor = System.Drawing.Color.DimGray;
            this.lblComentario.Location = new System.Drawing.Point(4, 208);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(113, 19);
            this.lblComentario.TabIndex = 7;
            this.lblComentario.Text = "Comentarios";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.DimGray;
            this.lblDescripcion.Location = new System.Drawing.Point(4, 102);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(107, 19);
            this.lblDescripcion.TabIndex = 5;
            this.lblDescripcion.Text = "Descripción";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.DimGray;
            this.lblNombre.Location = new System.Drawing.Point(4, 51);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(76, 19);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre";
            // 
            // lblIdentificacion
            // 
            this.lblIdentificacion.AutoSize = true;
            this.lblIdentificacion.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentificacion.ForeColor = System.Drawing.Color.DimGray;
            this.lblIdentificacion.Location = new System.Drawing.Point(4, -1);
            this.lblIdentificacion.Name = "lblIdentificacion";
            this.lblIdentificacion.Size = new System.Drawing.Size(121, 19);
            this.lblIdentificacion.TabIndex = 1;
            this.lblIdentificacion.Text = "Identificación";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(231, 471);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 23);
            this.label9.TabIndex = 22;
            this.label9.Text = "Fecha : ";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.BackgroundImage")));
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(8, 362);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(520, 1);
            this.pictureBox7.TabIndex = 19;
            this.pictureBox7.TabStop = false;
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtMonto.ForeColor = System.Drawing.Color.LightGray;
            this.txtMonto.Location = new System.Drawing.Point(7, 334);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(519, 27);
            this.txtMonto.TabIndex = 10;
            this.txtMonto.Text = "Monto";
            this.txtMonto.TextChanged += new System.EventHandler(this.txtMonto_TextChanged);
            this.txtMonto.Enter += new System.EventHandler(this.txtMonto_Enter);
            this.txtMonto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMonto_KeyUp);
            this.txtMonto.Leave += new System.EventHandler(this.txtMonto_Leave);
            // 
            // Realizado
            // 
            this.Realizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Realizado.AutoSize = true;
            this.Realizado.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.Realizado.Location = new System.Drawing.Point(399, 436);
            this.Realizado.Name = "Realizado";
            this.Realizado.Size = new System.Drawing.Size(79, 23);
            this.Realizado.TabIndex = 17;
            this.Realizado.Text = "Usuario";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(231, 439);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Realizado por : ";
            // 
            // btnIdentificacion
            // 
            this.btnIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIdentificacion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnIdentificacion.ForeColor = System.Drawing.Color.DimGray;
            this.btnIdentificacion.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnIdentificacion.IconColor = System.Drawing.Color.DimGray;
            this.btnIdentificacion.IconSize = 19;
            this.btnIdentificacion.Location = new System.Drawing.Point(500, 23);
            this.btnIdentificacion.Name = "btnIdentificacion";
            this.btnIdentificacion.Size = new System.Drawing.Size(24, 19);
            this.btnIdentificacion.TabIndex = 15;
            this.btnIdentificacion.TabStop = false;
            this.btnIdentificacion.Click += new System.EventHandler(this.btnIdentificacion_Click);
            // 
            // btnNewCustomer
            // 
            this.btnNewCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewCustomer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewCustomer.ForeColor = System.Drawing.Color.DimGray;
            this.btnNewCustomer.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnNewCustomer.IconColor = System.Drawing.Color.DimGray;
            this.btnNewCustomer.IconSize = 19;
            this.btnNewCustomer.Location = new System.Drawing.Point(479, 23);
            this.btnNewCustomer.Name = "btnNewCustomer";
            this.btnNewCustomer.Size = new System.Drawing.Size(20, 19);
            this.btnNewCustomer.TabIndex = 14;
            this.btnNewCustomer.TabStop = false;
            this.btnNewCustomer.Click += new System.EventHandler(this.btnNewCustomer_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Location = new System.Drawing.Point(8, 307);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(520, 1);
            this.pictureBox6.TabIndex = 13;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(7, 203);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(520, 1);
            this.pictureBox5.TabIndex = 12;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(6, 96);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(520, 1);
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(6, 45);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(520, 1);
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // chbEsOro
            // 
            this.chbEsOro.AutoSize = true;
            this.chbEsOro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.chbEsOro.ForeColor = System.Drawing.Color.DimGray;
            this.chbEsOro.Location = new System.Drawing.Point(9, 435);
            this.chbEsOro.Name = "chbEsOro";
            this.chbEsOro.Size = new System.Drawing.Size(104, 27);
            this.chbEsOro.TabIndex = 13;
            this.chbEsOro.Text = "Es Oro?";
            this.chbEsOro.UseVisualStyleBackColor = true;
            // 
            // txtComentario
            // 
            this.txtComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComentario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtComentario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComentario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtComentario.ForeColor = System.Drawing.Color.LightGray;
            this.txtComentario.Location = new System.Drawing.Point(6, 226);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(520, 79);
            this.txtComentario.TabIndex = 8;
            this.txtComentario.Text = "Comentarios";
            this.txtComentario.TextChanged += new System.EventHandler(this.txtComentario_TextChanged);
            this.txtComentario.Enter += new System.EventHandler(this.txtComentario_Enter);
            this.txtComentario.Leave += new System.EventHandler(this.txtComentario_Leave);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtDescripcion.ForeColor = System.Drawing.Color.LightGray;
            this.txtDescripcion.Location = new System.Drawing.Point(6, 122);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(520, 79);
            this.txtDescripcion.TabIndex = 6;
            this.txtDescripcion.Text = "Descripción";
            this.txtDescripcion.Enter += new System.EventHandler(this.txtDescripcion_Enter);
            this.txtDescripcion.Leave += new System.EventHandler(this.txtDescripcion_Leave);
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtNombre.ForeColor = System.Drawing.Color.LightGray;
            this.txtNombre.Location = new System.Drawing.Point(6, 72);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(520, 27);
            this.txtNombre.TabIndex = 4;
            this.txtNombre.Text = "Nombre";
            this.txtNombre.Enter += new System.EventHandler(this.txtNombre_Enter);
            this.txtNombre.Leave += new System.EventHandler(this.txtNombre_Leave);
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentificacion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIdentificacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtIdentificacion.ForeColor = System.Drawing.Color.LightGray;
            this.txtIdentificacion.Location = new System.Drawing.Point(6, 21);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(520, 27);
            this.txtIdentificacion.TabIndex = 2;
            this.txtIdentificacion.Text = "Identificación";
            this.txtIdentificacion.Enter += new System.EventHandler(this.txtIdentificacion_Enter);
            this.txtIdentificacion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificacion_KeyUp);
            this.txtIdentificacion.Leave += new System.EventHandler(this.txtIdentificacion_Leave);
            // 
            // panelBuscar
            // 
            this.panelBuscar.BackColor = System.Drawing.SystemColors.Control;
            this.panelBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBuscar.BackgroundImage")));
            this.panelBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBuscar.Controls.Add(this.iconPictureBox1);
            this.panelBuscar.Controls.Add(this.pictureBox1);
            this.panelBuscar.Controls.Add(this.txtBuscar);
            this.panelBuscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBuscar.Location = new System.Drawing.Point(0, 0);
            this.panelBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.panelBuscar.Name = "panelBuscar";
            this.panelBuscar.Padding = new System.Windows.Forms.Padding(5, 5, 25, 5);
            this.panelBuscar.Size = new System.Drawing.Size(1478, 66);
            this.panelBuscar.TabIndex = 1;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.iconPictureBox1.ForeColor = System.Drawing.Color.LightGray;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPictureBox1.IconColor = System.Drawing.Color.LightGray;
            this.iconPictureBox1.IconSize = 33;
            this.iconPictureBox1.Location = new System.Drawing.Point(1409, 8);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(41, 33);
            this.iconPictureBox1.TabIndex = 2;
            this.iconPictureBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(5, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1448, 1);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBuscar.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.Color.LightGray;
            this.txtBuscar.Location = new System.Drawing.Point(5, 5);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(10, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(1448, 50);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = " Buscar";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter);
            this.txtBuscar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyUp);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // elipseEstado
            // 
            this.elipseEstado.ElipseRadius = 20;
            this.elipseEstado.TargetControl = this.lblEstado;
            // 
            // btnReimprimirPago
            // 
            this.btnReimprimirPago.BackColor = System.Drawing.Color.DimGray;
            this.btnReimprimirPago.Enabled = false;
            this.btnReimprimirPago.FlatAppearance.BorderSize = 0;
            this.btnReimprimirPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReimprimirPago.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnReimprimirPago.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnReimprimirPago.ForeColor = System.Drawing.Color.White;
            this.btnReimprimirPago.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnReimprimirPago.IconColor = System.Drawing.Color.White;
            this.btnReimprimirPago.IconSize = 24;
            this.btnReimprimirPago.Location = new System.Drawing.Point(303, 522);
            this.btnReimprimirPago.Name = "btnReimprimirPago";
            this.btnReimprimirPago.Rotation = 0D;
            this.btnReimprimirPago.Size = new System.Drawing.Size(121, 41);
            this.btnReimprimirPago.TabIndex = 29;
            this.btnReimprimirPago.Text = "Reimprimir";
            this.btnReimprimirPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReimprimirPago.UseVisualStyleBackColor = false;
            this.btnReimprimirPago.Click += new System.EventHandler(this.btnReimprimirPago_Click);
            // 
            // frmEmpeno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1478, 988);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelBuscar);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEmpeno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Empeño";
            this.Load += new System.EventHandler(this.frmEmpeno_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.panel17.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpeños)).EndInit();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIdentificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panelBuscar.ResumeLayout(false);
            this.panelBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBuscar;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPrincipal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel8;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel16;
        private Panel panel5;
        private Panel panel17;
        private FontAwesome.Sharp.IconButton btnEmpeñar;
        private FontAwesome.Sharp.IconButton btnClienteNuevo;
        private FontAwesome.Sharp.IconButton btnVerEmpleado;
        private FontAwesome.Sharp.IconButton btnEditar;
        private Panel panel4;
        private Label label5;
        private Panel panel18;
        private DataGridView dgvClientes;
        private Panel panel10;
        private TableLayoutPanel tableLayoutPanel5;
        private Panel panel11;
        private Label label7;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label lblNumeroEmpeño;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel6;
        private Panel panel13;
        private FontAwesome.Sharp.IconButton btnGuardarEmpeño;
        private FontAwesome.Sharp.IconButton iconButton10;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private Panel panelFormulario;
        private Label label9;
        private PictureBox pictureBox7;
        private TextBox txtMonto;
        private Label Realizado;
        private Label label3;
        private FontAwesome.Sharp.IconPictureBox btnIdentificacion;
        private FontAwesome.Sharp.IconPictureBox btnNewCustomer;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private CheckBox chbEsOro;
        private TextBox txtComentario;
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private TextBox txtIdentificacion;
        private Panel panel6;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel7;
        private PictureBox pictureBox2;
        private ComboBox comboBox1;
        private Panel panel21;
        private Panel panel3;
        private Label label10;
        private Panel panel22;
        private Label lblCantidad;
        private Label label11;
        private Panel panel23;
        private FontAwesome.Sharp.IconButton btnVer;
        private FontAwesome.Sharp.IconButton btnEditarEmpeño;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private DataGridView dgvEmpeños;
        private Panel panel20;
        private Label lblCatidadEmpeños;
        private Label label14;
        private Label lblMonto;
        private Label lblComentario;
        private Label lblDescripcion;
        private Label lblNombre;
        private Label lblIdentificacion;
        private PictureBox pictureBox8;
        private ComboBox cbInteres;
        private Label lblInteres;
        private FontAwesome.Sharp.IconButton btnVerPago;
        private FontAwesome.Sharp.IconButton btnEditarPago;
        private FontAwesome.Sharp.IconButton btnEliminarPago;
        private DataGridView dgvPagos;
        private Label lblEstado;
        private Bunifu.Framework.UI.BunifuElipse elipseEstado;
        private FontAwesome.Sharp.IconButton btnHoy;
        private FontAwesome.Sharp.IconButton btnPendientes;
        private FontAwesome.Sharp.IconButton btnReimprimir;
        private FontAwesome.Sharp.IconButton btnIntereses;
        private FontAwesome.Sharp.IconButton btnPagos;
        private Panel panel9;
        private TextBox lblVence;
        private TextBox Fecha;
        private FontAwesome.Sharp.IconButton btnReimprimirPago;
    }
}