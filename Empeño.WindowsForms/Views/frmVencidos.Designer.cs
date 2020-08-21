namespace Empeño.WindowsForms.Views
{
    partial class frmVencidos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVencidos));
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.panelArqueo = new System.Windows.Forms.Panel();
            this.lblTotalProrroga = new System.Windows.Forms.Label();
            this.lblTotalRetirados = new System.Windows.Forms.Label();
            this.lblTotalVencidos = new System.Windows.Forms.Label();
            this.txtTotalProrroga = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalRetirados = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalVencido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.elipsePanelArqueo = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.elipseDetalles = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panelArqueo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDetalle
            // 
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.BackColor = System.Drawing.Color.Gainsboro;
            this.panelDetalle.Controls.Add(this.dgvDetalles);
            this.panelDetalle.Controls.Add(this.label6);
            this.panelDetalle.Location = new System.Drawing.Point(19, 296);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(630, 414);
            this.panelDetalle.TabIndex = 10;
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.dgvDetalles.ColumnHeadersHeight = 60;
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
            this.dgvDetalles.Location = new System.Drawing.Point(19, 35);
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
            this.dgvDetalles.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvDetalles.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetalles.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.RowTemplate.DividerHeight = 1;
            this.dgvDetalles.RowTemplate.Height = 45;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(593, 359);
            this.dgvDetalles.TabIndex = 23;
            this.dgvDetalles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalles_CellContentClick_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(16, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "Detalles";
            // 
            // panelArqueo
            // 
            this.panelArqueo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArqueo.BackColor = System.Drawing.Color.LightGray;
            this.panelArqueo.Controls.Add(this.button1);
            this.panelArqueo.Controls.Add(this.textBox1);
            this.panelArqueo.Controls.Add(this.label2);
            this.panelArqueo.Controls.Add(this.lblTotalProrroga);
            this.panelArqueo.Controls.Add(this.lblTotalRetirados);
            this.panelArqueo.Controls.Add(this.lblTotalVencidos);
            this.panelArqueo.Controls.Add(this.txtTotalProrroga);
            this.panelArqueo.Controls.Add(this.label7);
            this.panelArqueo.Controls.Add(this.txtTotalRetirados);
            this.panelArqueo.Controls.Add(this.label4);
            this.panelArqueo.Controls.Add(this.txtTotalVencido);
            this.panelArqueo.Controls.Add(this.label3);
            this.panelArqueo.Controls.Add(this.btnDelete);
            this.panelArqueo.Controls.Add(this.txtEmpleado);
            this.panelArqueo.Controls.Add(this.lblEmpleado);
            this.panelArqueo.Controls.Add(this.btnCancelar);
            this.panelArqueo.Controls.Add(this.btnImprimir);
            this.panelArqueo.Controls.Add(this.txtFecha);
            this.panelArqueo.Controls.Add(this.lblFecha);
            this.panelArqueo.Controls.Add(this.label1);
            this.panelArqueo.Location = new System.Drawing.Point(19, 37);
            this.panelArqueo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelArqueo.Name = "panelArqueo";
            this.panelArqueo.Size = new System.Drawing.Size(631, 250);
            this.panelArqueo.TabIndex = 9;
            this.panelArqueo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelArqueo_Paint);
            // 
            // lblTotalProrroga
            // 
            this.lblTotalProrroga.AutoSize = true;
            this.lblTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProrroga.ForeColor = System.Drawing.Color.Black;
            this.lblTotalProrroga.Location = new System.Drawing.Point(609, 111);
            this.lblTotalProrroga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalProrroga.Name = "lblTotalProrroga";
            this.lblTotalProrroga.Size = new System.Drawing.Size(21, 22);
            this.lblTotalProrroga.TabIndex = 26;
            this.lblTotalProrroga.Text = "0";
            // 
            // lblTotalRetirados
            // 
            this.lblTotalRetirados.AutoSize = true;
            this.lblTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRetirados.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRetirados.Location = new System.Drawing.Point(370, 111);
            this.lblTotalRetirados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalRetirados.Name = "lblTotalRetirados";
            this.lblTotalRetirados.Size = new System.Drawing.Size(21, 22);
            this.lblTotalRetirados.TabIndex = 25;
            this.lblTotalRetirados.Text = "0";
            // 
            // lblTotalVencidos
            // 
            this.lblTotalVencidos.AutoSize = true;
            this.lblTotalVencidos.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVencidos.ForeColor = System.Drawing.Color.Black;
            this.lblTotalVencidos.Location = new System.Drawing.Point(144, 111);
            this.lblTotalVencidos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalVencidos.Name = "lblTotalVencidos";
            this.lblTotalVencidos.Size = new System.Drawing.Size(21, 22);
            this.lblTotalVencidos.TabIndex = 24;
            this.lblTotalVencidos.Text = "0";
            // 
            // txtTotalProrroga
            // 
            this.txtTotalProrroga.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalProrroga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalProrroga.Enabled = false;
            this.txtTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalProrroga.Location = new System.Drawing.Point(475, 138);
            this.txtTotalProrroga.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTotalProrroga.Name = "txtTotalProrroga";
            this.txtTotalProrroga.Size = new System.Drawing.Size(164, 23);
            this.txtTotalProrroga.TabIndex = 13;
            this.txtTotalProrroga.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(472, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 21);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total en Prórroga";
            // 
            // txtTotalRetirados
            // 
            this.txtTotalRetirados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalRetirados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalRetirados.Enabled = false;
            this.txtTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRetirados.Location = new System.Drawing.Point(246, 138);
            this.txtTotalRetirados.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTotalRetirados.Name = "txtTotalRetirados";
            this.txtTotalRetirados.Size = new System.Drawing.Size(164, 23);
            this.txtTotalRetirados.TabIndex = 11;
            this.txtTotalRetirados.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(243, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Retirados";
            // 
            // txtTotalVencido
            // 
            this.txtTotalVencido.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalVencido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalVencido.Enabled = false;
            this.txtTotalVencido.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVencido.Location = new System.Drawing.Point(19, 138);
            this.txtTotalVencido.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTotalVencido.Name = "txtTotalVencido";
            this.txtTotalVencido.Size = new System.Drawing.Size(164, 23);
            this.txtTotalVencido.TabIndex = 9;
            this.txtTotalVencido.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(16, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total Vencidos";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmpleado.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(475, 71);
            this.txtEmpleado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(164, 23);
            this.txtEmpleado.TabIndex = 6;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmpleado.Location = new System.Drawing.Point(471, 50);
            this.lblEmpleado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(90, 21);
            this.lblEmpleado.TabIndex = 5;
            this.lblEmpleado.Text = "Empleado";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFecha.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(246, 71);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(164, 23);
            this.txtFecha.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.DimGray;
            this.lblFecha.Location = new System.Drawing.Point(243, 50);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(56, 21);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vencidos";
            // 
            // elipsePanelArqueo
            // 
            this.elipsePanelArqueo.ElipseRadius = 20;
            this.elipsePanelArqueo.TargetControl = this.panelArqueo;
            // 
            // elipseDetalles
            // 
            this.elipseDetalles.ElipseRadius = 20;
            this.elipseDetalles.TargetControl = this.panelDetalle;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(20, 71);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 23);
            this.textBox1.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 21);
            this.label2.TabIndex = 28;
            this.label2.Text = "Desde";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkViolet;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(20, 189);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 36);
            this.button1.TabIndex = 30;
            this.button1.Text = " Buscar";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(310, 189);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 36);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = " Eliminar";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gold;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(212, 189);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(94, 36);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(116, 189);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(2);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(92, 36);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = " Enviar";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnGuardarCierreCaja_Click);
            // 
            // frmVencidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(714, 630);
            this.Controls.Add(this.panelDetalle);
            this.Controls.Add(this.panelArqueo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmVencidos";
            this.Text = "Vencidos";
            this.Load += new System.EventHandler(this.frmArqueo_Load);
            this.panelDetalle.ResumeLayout(false);
            this.panelDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.panelArqueo.ResumeLayout(false);
            this.panelArqueo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDetalle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelArqueo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalProrroga;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalRetirados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalVencido;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuElipse elipsePanelArqueo;
        private Bunifu.Framework.UI.BunifuElipse elipseDetalles;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblTotalProrroga;
        private System.Windows.Forms.Label lblTotalRetirados;
        private System.Windows.Forms.Label lblTotalVencidos;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}