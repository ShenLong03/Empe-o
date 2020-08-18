namespace Empeño.WindowsForms.Reports
{
    partial class frmReporteEmpeños
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteEmpeños));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.txtArticulos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOro = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPendiente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chartEmpeños = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckbBorrados = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chbPerdidos = new System.Windows.Forms.CheckBox();
            this.chbRetirados = new System.Windows.Forms.CheckBox();
            this.chbVencidos = new System.Windows.Forms.CheckBox();
            this.chbPendientes = new System.Windows.Forms.CheckBox();
            this.chbActivos = new System.Windows.Forms.CheckBox();
            this.chbTodo = new System.Windows.Forms.CheckBox();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvEmpeños = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmpeños)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpeños)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txtArticulos);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOro);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPendiente);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtMonto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chartEmpeños);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.ckbBorrados);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chbPerdidos);
            this.panel1.Controls.Add(this.chbRetirados);
            this.panel1.Controls.Add(this.chbVencidos);
            this.panel1.Controls.Add(this.chbPendientes);
            this.panel1.Controls.Add(this.chbActivos);
            this.panel1.Controls.Add(this.chbTodo);
            this.panel1.Controls.Add(this.dtHasta);
            this.panel1.Controls.Add(this.dtDesde);
            this.panel1.Controls.Add(this.lblHasta);
            this.panel1.Controls.Add(this.lblDesde);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1413, 476);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(997, 391);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 47);
            this.button3.TabIndex = 64;
            this.button3.Text = "Exportar";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // txtArticulos
            // 
            this.txtArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArticulos.Enabled = false;
            this.txtArticulos.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArticulos.Location = new System.Drawing.Point(900, 302);
            this.txtArticulos.Name = "txtArticulos";
            this.txtArticulos.Size = new System.Drawing.Size(250, 36);
            this.txtArticulos.TabIndex = 63;
            this.txtArticulos.Text = "0.00";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(896, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 19);
            this.label4.TabIndex = 62;
            this.label4.Text = "Empeños Articulos";
            // 
            // txtOro
            // 
            this.txtOro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOro.Enabled = false;
            this.txtOro.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOro.Location = new System.Drawing.Point(606, 302);
            this.txtOro.Name = "txtOro";
            this.txtOro.Size = new System.Drawing.Size(250, 36);
            this.txtOro.TabIndex = 61;
            this.txtOro.Text = "0.00";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(602, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 19);
            this.label5.TabIndex = 60;
            this.label5.Text = "Empeños en Oro";
            // 
            // txtPendiente
            // 
            this.txtPendiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPendiente.Enabled = false;
            this.txtPendiente.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPendiente.Location = new System.Drawing.Point(900, 223);
            this.txtPendiente.Name = "txtPendiente";
            this.txtPendiente.Size = new System.Drawing.Size(250, 36);
            this.txtPendiente.TabIndex = 59;
            this.txtPendiente.Text = "0.00";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(896, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 19);
            this.label3.TabIndex = 58;
            this.label3.Text = "Pendiente";
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto.Enabled = false;
            this.txtMonto.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonto.Location = new System.Drawing.Point(606, 223);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(250, 36);
            this.txtMonto.TabIndex = 57;
            this.txtMonto.Text = "0.00";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(602, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 56;
            this.label1.Text = "Monto";
            // 
            // chartEmpeños
            // 
            this.chartEmpeños.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartEmpeños.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartEmpeños.Legends.Add(legend1);
            this.chartEmpeños.Location = new System.Drawing.Point(19, 191);
            this.chartEmpeños.Name = "chartEmpeños";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartEmpeños.Series.Add(series1);
            this.chartEmpeños.Size = new System.Drawing.Size(516, 275);
            this.chartEmpeños.TabIndex = 55;
            this.chartEmpeños.Text = "chart2";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Location = new System.Drawing.Point(19, 186);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1380, 1);
            this.panel3.TabIndex = 32;
            // 
            // ckbBorrados
            // 
            this.ckbBorrados.AutoSize = true;
            this.ckbBorrados.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbBorrados.ForeColor = System.Drawing.Color.Maroon;
            this.ckbBorrados.Location = new System.Drawing.Point(807, 58);
            this.ckbBorrados.Name = "ckbBorrados";
            this.ckbBorrados.Size = new System.Drawing.Size(319, 23);
            this.ckbBorrados.TabIndex = 31;
            this.ckbBorrados.Text = "Borrados, Cancelados o Elminados";
            this.ckbBorrados.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 472);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1413, 4);
            this.panel4.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(371, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 29;
            this.label2.Text = "Estados";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkOrange;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(607, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 47);
            this.button2.TabIndex = 28;
            this.button2.Text = "Cancelar";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(606, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 47);
            this.button1.TabIndex = 27;
            this.button1.Text = "Buscar";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chbPerdidos
            // 
            this.chbPerdidos.AutoSize = true;
            this.chbPerdidos.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbPerdidos.ForeColor = System.Drawing.Color.DimGray;
            this.chbPerdidos.Location = new System.Drawing.Point(463, 142);
            this.chbPerdidos.Name = "chbPerdidos";
            this.chbPerdidos.Size = new System.Drawing.Size(102, 23);
            this.chbPerdidos.TabIndex = 26;
            this.chbPerdidos.Text = "Perdidos";
            this.chbPerdidos.UseVisualStyleBackColor = true;
            this.chbPerdidos.CheckedChanged += new System.EventHandler(this.chbPerdidos_CheckedChanged);
            // 
            // chbRetirados
            // 
            this.chbRetirados.AutoSize = true;
            this.chbRetirados.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRetirados.ForeColor = System.Drawing.Color.DimGray;
            this.chbRetirados.Location = new System.Drawing.Point(463, 100);
            this.chbRetirados.Name = "chbRetirados";
            this.chbRetirados.Size = new System.Drawing.Size(106, 23);
            this.chbRetirados.TabIndex = 25;
            this.chbRetirados.Text = "Retirados";
            this.chbRetirados.UseVisualStyleBackColor = true;
            this.chbRetirados.CheckedChanged += new System.EventHandler(this.chbRetirados_CheckedChanged);
            // 
            // chbVencidos
            // 
            this.chbVencidos.AutoSize = true;
            this.chbVencidos.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbVencidos.ForeColor = System.Drawing.Color.DimGray;
            this.chbVencidos.Location = new System.Drawing.Point(290, 100);
            this.chbVencidos.Name = "chbVencidos";
            this.chbVencidos.Size = new System.Drawing.Size(109, 23);
            this.chbVencidos.TabIndex = 24;
            this.chbVencidos.Text = "Vencidos";
            this.chbVencidos.UseVisualStyleBackColor = true;
            this.chbVencidos.CheckedChanged += new System.EventHandler(this.chbVencidos_CheckedChanged);
            // 
            // chbPendientes
            // 
            this.chbPendientes.AutoSize = true;
            this.chbPendientes.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbPendientes.ForeColor = System.Drawing.Color.DimGray;
            this.chbPendientes.Location = new System.Drawing.Point(290, 142);
            this.chbPendientes.Name = "chbPendientes";
            this.chbPendientes.Size = new System.Drawing.Size(121, 23);
            this.chbPendientes.TabIndex = 19;
            this.chbPendientes.Text = "Pendientes";
            this.chbPendientes.UseVisualStyleBackColor = true;
            this.chbPendientes.CheckedChanged += new System.EventHandler(this.chbPendientes_CheckedChanged_1);
            // 
            // chbActivos
            // 
            this.chbActivos.AutoSize = true;
            this.chbActivos.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbActivos.ForeColor = System.Drawing.Color.DimGray;
            this.chbActivos.Location = new System.Drawing.Point(463, 58);
            this.chbActivos.Name = "chbActivos";
            this.chbActivos.Size = new System.Drawing.Size(92, 23);
            this.chbActivos.TabIndex = 18;
            this.chbActivos.Text = "Activos";
            this.chbActivos.UseVisualStyleBackColor = true;
            this.chbActivos.CheckedChanged += new System.EventHandler(this.chbActivos_CheckedChanged);
            // 
            // chbTodo
            // 
            this.chbTodo.AutoSize = true;
            this.chbTodo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbTodo.ForeColor = System.Drawing.Color.DimGray;
            this.chbTodo.Location = new System.Drawing.Point(290, 58);
            this.chbTodo.Name = "chbTodo";
            this.chbTodo.Size = new System.Drawing.Size(71, 23);
            this.chbTodo.TabIndex = 17;
            this.chbTodo.Text = "Todo";
            this.chbTodo.UseVisualStyleBackColor = true;
            this.chbTodo.CheckedChanged += new System.EventHandler(this.chbTodo_CheckedChanged_1);
            // 
            // dtHasta
            // 
            this.dtHasta.CustomFormat = "dd/MM/yyyy";
            this.dtHasta.Font = new System.Drawing.Font("Century Gothic", 13.8F);
            this.dtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtHasta.Location = new System.Drawing.Point(26, 130);
            this.dtHasta.Name = "dtHasta";
            this.dtHasta.Size = new System.Drawing.Size(235, 36);
            this.dtHasta.TabIndex = 16;
            // 
            // dtDesde
            // 
            this.dtDesde.CustomFormat = "dd/MM/yyyy";
            this.dtDesde.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDesde.Location = new System.Drawing.Point(23, 58);
            this.dtDesde.Name = "dtDesde";
            this.dtDesde.Size = new System.Drawing.Size(235, 36);
            this.dtDesde.TabIndex = 15;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.ForeColor = System.Drawing.Color.DimGray;
            this.lblHasta.Location = new System.Drawing.Point(23, 107);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(55, 19);
            this.lblHasta.TabIndex = 14;
            this.lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.ForeColor = System.Drawing.Color.DimGray;
            this.lblDesde.Location = new System.Drawing.Point(23, 35);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(62, 19);
            this.lblDesde.TabIndex = 13;
            this.lblDesde.Text = "Desde";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvEmpeños);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1413, 428);
            this.panel2.TabIndex = 6;
            // 
            // dgvEmpeños
            // 
            this.dgvEmpeños.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpeños.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvEmpeños.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpeños.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvEmpeños.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(2)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpeños.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpeños.ColumnHeadersHeight = 60;
            this.dgvEmpeños.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmpeños.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmpeños.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpeños.EnableHeadersVisualStyles = false;
            this.dgvEmpeños.GridColor = System.Drawing.SystemColors.Control;
            this.dgvEmpeños.Location = new System.Drawing.Point(0, 0);
            this.dgvEmpeños.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEmpeños.Name = "dgvEmpeños";
            this.dgvEmpeños.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpeños.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEmpeños.RowHeadersVisible = false;
            this.dgvEmpeños.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvEmpeños.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEmpeños.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvEmpeños.RowTemplate.DividerHeight = 1;
            this.dgvEmpeños.RowTemplate.Height = 45;
            this.dgvEmpeños.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpeños.Size = new System.Drawing.Size(1413, 428);
            this.dgvEmpeños.TabIndex = 22;
            // 
            // frmReporteEmpeños
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1413, 904);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporteEmpeños";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporte Empeños";
            this.Load += new System.EventHandler(this.frmReporteEmpeños_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmpeños)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpeños)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbPendientes;
        private System.Windows.Forms.CheckBox chbActivos;
        private System.Windows.Forms.CheckBox chbTodo;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chbPerdidos;
        private System.Windows.Forms.CheckBox chbRetirados;
        private System.Windows.Forms.CheckBox chbVencidos;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ckbBorrados;
        private System.Windows.Forms.DataGridView dgvEmpeños;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtArticulos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPendiente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEmpeños;
    }
}