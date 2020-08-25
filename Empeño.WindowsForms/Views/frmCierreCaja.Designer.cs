namespace Empeño.WindowsForms.Views
{
    partial class frmCierreCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCierreCaja));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.elipsePanelCierreInformacion = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panelCierreInformacion = new System.Windows.Forms.Panel();
            this.txtFecha = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnDeleteCierreCaja = new System.Windows.Forms.Button();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.btnCancelarCierreCaja = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.elipseDetalleCierre = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDetalleCierre = new System.Windows.Forms.Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.btnEnviarCierre = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardarDetalle = new System.Windows.Forms.Button();
            this.btnDeleteDetalle = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAcumuladoInicial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCancelados = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAcumulado = new System.Windows.Forms.TextBox();
            this.txtVencimientos = new System.Windows.Forms.TextBox();
            this.txtAbonos = new System.Windows.Forms.TextBox();
            this.txtInteres = new System.Windows.Forms.TextBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblEspere = new System.Windows.Forms.Label();
            this.panelCierreInformacion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDetalleCierre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elipsePanelCierreInformacion
            // 
            this.elipsePanelCierreInformacion.ElipseRadius = 20;
            this.elipsePanelCierreInformacion.TargetControl = this.panelCierreInformacion;
            // 
            // panelCierreInformacion
            // 
            this.panelCierreInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCierreInformacion.BackColor = System.Drawing.Color.LightGray;
            this.panelCierreInformacion.Controls.Add(this.lblEspere);
            this.panelCierreInformacion.Controls.Add(this.txtFecha);
            this.panelCierreInformacion.Controls.Add(this.btnBuscar);
            this.panelCierreInformacion.Controls.Add(this.btnDeleteCierreCaja);
            this.panelCierreInformacion.Controls.Add(this.txtEmpleado);
            this.panelCierreInformacion.Controls.Add(this.lblEmpleado);
            this.panelCierreInformacion.Controls.Add(this.btnCancelarCierreCaja);
            this.panelCierreInformacion.Controls.Add(this.lblFecha);
            this.panelCierreInformacion.Controls.Add(this.label1);
            this.panelCierreInformacion.Location = new System.Drawing.Point(20, 17);
            this.panelCierreInformacion.Margin = new System.Windows.Forms.Padding(2);
            this.panelCierreInformacion.Name = "panelCierreInformacion";
            this.panelCierreInformacion.Size = new System.Drawing.Size(1016, 162);
            this.panelCierreInformacion.TabIndex = 1;
            this.panelCierreInformacion.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCierreInformacion_Paint_1);
            // 
            // txtFecha
            // 
            this.txtFecha.CustomFormat = "dd/MM/yyyy";
            this.txtFecha.Font = new System.Drawing.Font("Century Gothic", 13.8F);
            this.txtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFecha.Location = new System.Drawing.Point(19, 70);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(200, 30);
            this.txtFecha.TabIndex = 32;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.DarkViolet;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(19, 107);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(92, 36);
            this.btnBuscar.TabIndex = 31;
            this.btnBuscar.Text = " Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnDeleteCierreCaja
            // 
            this.btnDeleteCierreCaja.BackColor = System.Drawing.Color.Crimson;
            this.btnDeleteCierreCaja.FlatAppearance.BorderSize = 0;
            this.btnDeleteCierreCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCierreCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteCierreCaja.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCierreCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCierreCaja.Image")));
            this.btnDeleteCierreCaja.Location = new System.Drawing.Point(211, 107);
            this.btnDeleteCierreCaja.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteCierreCaja.Name = "btnDeleteCierreCaja";
            this.btnDeleteCierreCaja.Size = new System.Drawing.Size(80, 36);
            this.btnDeleteCierreCaja.TabIndex = 7;
            this.btnDeleteCierreCaja.Text = " Eliminar";
            this.btnDeleteCierreCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteCierreCaja.UseVisualStyleBackColor = false;
            this.btnDeleteCierreCaja.Click += new System.EventHandler(this.btnDeleteCierreCaja_Click);
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmpleado.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(240, 70);
            this.txtEmpleado.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(243, 23);
            this.txtEmpleado.TabIndex = 6;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmpleado.Location = new System.Drawing.Point(237, 49);
            this.lblEmpleado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(90, 21);
            this.lblEmpleado.TabIndex = 5;
            this.lblEmpleado.Text = "Empleado";
            // 
            // btnCancelarCierreCaja
            // 
            this.btnCancelarCierreCaja.BackColor = System.Drawing.Color.Gold;
            this.btnCancelarCierreCaja.FlatAppearance.BorderSize = 0;
            this.btnCancelarCierreCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarCierreCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelarCierreCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarCierreCaja.Image")));
            this.btnCancelarCierreCaja.Location = new System.Drawing.Point(114, 107);
            this.btnCancelarCierreCaja.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelarCierreCaja.Name = "btnCancelarCierreCaja";
            this.btnCancelarCierreCaja.Size = new System.Drawing.Size(94, 36);
            this.btnCancelarCierreCaja.TabIndex = 4;
            this.btnCancelarCierreCaja.Text = " Cancelar";
            this.btnCancelarCierreCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelarCierreCaja.UseVisualStyleBackColor = false;
            this.btnCancelarCierreCaja.Click += new System.EventHandler(this.btnCancelarCierreCaja_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.DimGray;
            this.lblFecha.Location = new System.Drawing.Point(16, 49);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(59, 21);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cierre Caja";
            // 
            // elipseDetalleCierre
            // 
            this.elipseDetalleCierre.ElipseRadius = 20;
            this.elipseDetalleCierre.TargetControl = this;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panelDetalleCierre, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(21, 196);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 460);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panelDetalleCierre
            // 
            this.panelDetalleCierre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalleCierre.BackColor = System.Drawing.Color.Gainsboro;
            this.panelDetalleCierre.Controls.Add(this.dgvDetalles);
            this.panelDetalleCierre.Controls.Add(this.btnEnviarCierre);
            this.panelDetalleCierre.Controls.Add(this.textBox1);
            this.panelDetalleCierre.Controls.Add(this.label4);
            this.panelDetalleCierre.Controls.Add(this.txtTotal);
            this.panelDetalleCierre.Controls.Add(this.label3);
            this.panelDetalleCierre.Controls.Add(this.btnGuardarDetalle);
            this.panelDetalleCierre.Controls.Add(this.btnDeleteDetalle);
            this.panelDetalleCierre.Controls.Add(this.btnCancelar);
            this.panelDetalleCierre.Controls.Add(this.panel2);
            this.panelDetalleCierre.Controls.Add(this.txtValor);
            this.panelDetalleCierre.Controls.Add(this.lblValor);
            this.panelDetalleCierre.Controls.Add(this.txtConcepto);
            this.panelDetalleCierre.Controls.Add(this.label5);
            this.panelDetalleCierre.Controls.Add(this.label6);
            this.panelDetalleCierre.Location = new System.Drawing.Point(401, 2);
            this.panelDetalleCierre.Margin = new System.Windows.Forms.Padding(2);
            this.panelDetalleCierre.Name = "panelDetalleCierre";
            this.panelDetalleCierre.Size = new System.Drawing.Size(595, 443);
            this.panelDetalleCierre.TabIndex = 9;
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalles.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDetalles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvDetalles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalles.ColumnHeadersHeight = 40;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalles.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalles.EnableHeadersVisualStyles = false;
            this.dgvDetalles.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDetalles.Location = new System.Drawing.Point(20, 162);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.RowHeadersWidth = 40;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvDetalles.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetalles.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.RowTemplate.DividerHeight = 1;
            this.dgvDetalles.RowTemplate.Height = 40;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(544, 268);
            this.dgvDetalles.TabIndex = 24;
            // 
            // btnEnviarCierre
            // 
            this.btnEnviarCierre.BackColor = System.Drawing.Color.LimeGreen;
            this.btnEnviarCierre.FlatAppearance.BorderSize = 0;
            this.btnEnviarCierre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarCierre.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarCierre.ForeColor = System.Drawing.Color.White;
            this.btnEnviarCierre.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarCierre.Image")));
            this.btnEnviarCierre.Location = new System.Drawing.Point(128, 104);
            this.btnEnviarCierre.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviarCierre.Name = "btnEnviarCierre";
            this.btnEnviarCierre.Size = new System.Drawing.Size(92, 36);
            this.btnEnviarCierre.TabIndex = 67;
            this.btnEnviarCierre.Text = " Enviar";
            this.btnEnviarCierre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnviarCierre.UseVisualStyleBackColor = false;
            this.btnEnviarCierre.Click += new System.EventHandler(this.btnEnviarCierre_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(24, 407);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 23);
            this.textBox1.TabIndex = 66;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(21, 386);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 21);
            this.label4.TabIndex = 65;
            this.label4.Text = "Saldo Inicial";
            this.label4.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Enabled = false;
            this.txtTotal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(201, 407);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(164, 23);
            this.txtTotal.TabIndex = 62;
            this.txtTotal.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(201, 386);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 21);
            this.label3.TabIndex = 61;
            this.label3.Text = "Saldo Final";
            this.label3.Visible = false;
            // 
            // btnGuardarDetalle
            // 
            this.btnGuardarDetalle.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGuardarDetalle.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnGuardarDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarDetalle.Image")));
            this.btnGuardarDetalle.Location = new System.Drawing.Point(20, 104);
            this.btnGuardarDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarDetalle.Name = "btnGuardarDetalle";
            this.btnGuardarDetalle.Size = new System.Drawing.Size(103, 36);
            this.btnGuardarDetalle.TabIndex = 59;
            this.btnGuardarDetalle.Text = " Agregar";
            this.btnGuardarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarDetalle.UseVisualStyleBackColor = false;
            this.btnGuardarDetalle.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeleteDetalle
            // 
            this.btnDeleteDetalle.BackColor = System.Drawing.Color.Crimson;
            this.btnDeleteDetalle.FlatAppearance.BorderSize = 0;
            this.btnDeleteDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDetalle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteDetalle.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDetalle.Image")));
            this.btnDeleteDetalle.Location = new System.Drawing.Point(324, 104);
            this.btnDeleteDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteDetalle.Name = "btnDeleteDetalle";
            this.btnDeleteDetalle.Size = new System.Drawing.Size(80, 36);
            this.btnDeleteDetalle.TabIndex = 10;
            this.btnDeleteDetalle.Text = " Eliminar";
            this.btnDeleteDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteDetalle.UseVisualStyleBackColor = false;
            this.btnDeleteDetalle.Click += new System.EventHandler(this.btnDeleteDetalle_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gold;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(225, 104);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(94, 36);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(13, 156);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 1);
            this.panel2.TabIndex = 7;
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtValor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValor.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Location = new System.Drawing.Point(241, 69);
            this.txtValor.Margin = new System.Windows.Forms.Padding(2);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(164, 23);
            this.txtValor.TabIndex = 6;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.ForeColor = System.Drawing.Color.DimGray;
            this.lblValor.Location = new System.Drawing.Point(237, 48);
            this.lblValor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(50, 21);
            this.lblValor.TabIndex = 5;
            this.lblValor.Text = "Valor";
            // 
            // txtConcepto
            // 
            this.txtConcepto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtConcepto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConcepto.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcepto.Location = new System.Drawing.Point(19, 69);
            this.txtConcepto.Margin = new System.Windows.Forms.Padding(2);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(164, 23);
            this.txtConcepto.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(16, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Concepto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(16, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cierre del Negocio";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtAcumuladoInicial);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtCancelados);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtAcumulado);
            this.panel1.Controls.Add(this.txtVencimientos);
            this.panel1.Controls.Add(this.txtAbonos);
            this.panel1.Controls.Add(this.txtInteres);
            this.panel1.Controls.Add(this.txtMonto);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 454);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Location = new System.Drawing.Point(7, 321);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(368, 1);
            this.panel3.TabIndex = 60;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkViolet;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(6, 326);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(92, 36);
            this.btnPrint.TabIndex = 59;
            this.btnPrint.Text = " Imprimir";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label13.Location = new System.Drawing.Point(2, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(182, 22);
            this.label13.TabIndex = 25;
            this.label13.Text = "Cierre de Empeños";
            // 
            // txtAcumuladoInicial
            // 
            this.txtAcumuladoInicial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcumuladoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcumuladoInicial.Location = new System.Drawing.Point(179, 41);
            this.txtAcumuladoInicial.Margin = new System.Windows.Forms.Padding(2);
            this.txtAcumuladoInicial.Name = "txtAcumuladoInicial";
            this.txtAcumuladoInicial.ReadOnly = true;
            this.txtAcumuladoInicial.Size = new System.Drawing.Size(192, 26);
            this.txtAcumuladoInicial.TabIndex = 58;
            this.txtAcumuladoInicial.TextChanged += new System.EventHandler(this.txtAcumuladoInicial_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(2, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 21);
            this.label8.TabIndex = 57;
            this.label8.Text = "Acumulado Inicial:";
            // 
            // txtCancelados
            // 
            this.txtCancelados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCancelados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancelados.Location = new System.Drawing.Point(179, 245);
            this.txtCancelados.Margin = new System.Windows.Forms.Padding(2);
            this.txtCancelados.Name = "txtCancelados";
            this.txtCancelados.ReadOnly = true;
            this.txtCancelados.Size = new System.Drawing.Size(192, 26);
            this.txtCancelados.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 249);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 21);
            this.label7.TabIndex = 55;
            this.label7.Text = "Monto Cancelados:";
            // 
            // txtAcumulado
            // 
            this.txtAcumulado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcumulado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcumulado.Location = new System.Drawing.Point(179, 286);
            this.txtAcumulado.Margin = new System.Windows.Forms.Padding(2);
            this.txtAcumulado.Name = "txtAcumulado";
            this.txtAcumulado.ReadOnly = true;
            this.txtAcumulado.Size = new System.Drawing.Size(192, 26);
            this.txtAcumulado.TabIndex = 53;
            // 
            // txtVencimientos
            // 
            this.txtVencimientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVencimientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVencimientos.Location = new System.Drawing.Point(179, 201);
            this.txtVencimientos.Margin = new System.Windows.Forms.Padding(2);
            this.txtVencimientos.Name = "txtVencimientos";
            this.txtVencimientos.ReadOnly = true;
            this.txtVencimientos.Size = new System.Drawing.Size(192, 26);
            this.txtVencimientos.TabIndex = 52;
            // 
            // txtAbonos
            // 
            this.txtAbonos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAbonos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbonos.Location = new System.Drawing.Point(179, 159);
            this.txtAbonos.Margin = new System.Windows.Forms.Padding(2);
            this.txtAbonos.Name = "txtAbonos";
            this.txtAbonos.ReadOnly = true;
            this.txtAbonos.Size = new System.Drawing.Size(192, 26);
            this.txtAbonos.TabIndex = 51;
            // 
            // txtInteres
            // 
            this.txtInteres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInteres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInteres.Location = new System.Drawing.Point(179, 118);
            this.txtInteres.Margin = new System.Windows.Forms.Padding(2);
            this.txtInteres.Name = "txtInteres";
            this.txtInteres.ReadOnly = true;
            this.txtInteres.Size = new System.Drawing.Size(192, 26);
            this.txtInteres.TabIndex = 50;
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonto.Location = new System.Drawing.Point(179, 79);
            this.txtMonto.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(192, 26);
            this.txtMonto.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 291);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 21);
            this.label2.TabIndex = 48;
            this.label2.Text = "Total Acumulado:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(2, 206);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(175, 21);
            this.label9.TabIndex = 47;
            this.label9.Text = "Monto Vencimientos:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(2, 164);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 21);
            this.label10.TabIndex = 46;
            this.label10.Text = "Abonos Recibidos:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 123);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(164, 21);
            this.label11.TabIndex = 45;
            this.label11.Text = "Intereses Cobrados:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(2, 84);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(141, 21);
            this.label12.TabIndex = 44;
            this.label12.Text = "Monto Empeños:";
            // 
            // lblEspere
            // 
            this.lblEspere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEspere.AutoSize = true;
            this.lblEspere.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEspere.Location = new System.Drawing.Point(768, 139);
            this.lblEspere.Name = "lblEspere";
            this.lblEspere.Size = new System.Drawing.Size(207, 23);
            this.lblEspere.TabIndex = 33;
            this.lblEspere.Text = "Espere un momento...";
            // 
            // frmCierreCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 660);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelCierreInformacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmCierreCaja";
            this.Text = "Cierre Caja";
            this.Load += new System.EventHandler(this.frmCierreCaja_Load);
            this.panelCierreInformacion.ResumeLayout(false);
            this.panelCierreInformacion.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDetalleCierre.ResumeLayout(false);
            this.panelDetalleCierre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuElipse elipsePanelCierreInformacion;
        private System.Windows.Forms.Panel panelCierreInformacion;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Button btnCancelarCierreCaja;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteCierreCaja;
        private Bunifu.Framework.UI.BunifuElipse elipseDetalleCierre;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDetalleCierre;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button btnDeleteDetalle;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAcumuladoInicial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCancelados;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAcumulado;
        private System.Windows.Forms.TextBox txtVencimientos;
        private System.Windows.Forms.TextBox txtAbonos;
        private System.Windows.Forms.TextBox txtInteres;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGuardarDetalle;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker txtFecha;
        private System.Windows.Forms.Button btnEnviarCierre;
        private System.Windows.Forms.Label lblEspere;
    }
}