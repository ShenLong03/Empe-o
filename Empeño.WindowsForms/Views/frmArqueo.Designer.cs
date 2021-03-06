﻿namespace Empeño.WindowsForms.Views
{
    partial class frmArqueo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArqueo));
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.panelArqueo = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblTotalProrroga = new System.Windows.Forms.Label();
            this.lblTotalRetirados = new System.Windows.Forms.Label();
            this.lblTotalVencidos = new System.Windows.Forms.Label();
            this.lblTotalAlDia = new System.Windows.Forms.Label();
            this.lblTotalPrincipal = new System.Windows.Forms.Label();
            this.txtTotalGeneral = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalAlDia = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalIntereses = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalPrincipal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalProrroga = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalRetirados = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalVencido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.elipsePanelArqueo = new ns1.BunifuElipse(this.components);
            this.elipseDetalles = new ns1.BunifuElipse(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panelArqueo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDetalle
            // 
            this.panelDetalle.BackColor = System.Drawing.Color.Gainsboro;
            this.panelDetalle.Controls.Add(this.dgvDetalles);
            this.panelDetalle.Controls.Add(this.label6);
            this.panelDetalle.Location = new System.Drawing.Point(28, 531);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(1640, 573);
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
            this.dgvDetalles.Location = new System.Drawing.Point(30, 78);
            this.dgvDetalles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.dgvDetalles.Size = new System.Drawing.Size(1580, 453);
            this.dgvDetalles.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(24, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(278, 34);
            this.label6.TabIndex = 0;
            this.label6.Text = "Detalles de Arqueo";
            // 
            // panelArqueo
            // 
            this.panelArqueo.BackColor = System.Drawing.Color.LightGray;
            this.panelArqueo.Controls.Add(this.btnPrint);
            this.panelArqueo.Controls.Add(this.label10);
            this.panelArqueo.Controls.Add(this.txtObservaciones);
            this.panelArqueo.Controls.Add(this.lblTotalProrroga);
            this.panelArqueo.Controls.Add(this.lblTotalRetirados);
            this.panelArqueo.Controls.Add(this.lblTotalVencidos);
            this.panelArqueo.Controls.Add(this.lblTotalAlDia);
            this.panelArqueo.Controls.Add(this.lblTotalPrincipal);
            this.panelArqueo.Controls.Add(this.txtTotalGeneral);
            this.panelArqueo.Controls.Add(this.label9);
            this.panelArqueo.Controls.Add(this.txtTotalAlDia);
            this.panelArqueo.Controls.Add(this.label8);
            this.panelArqueo.Controls.Add(this.txtTotalIntereses);
            this.panelArqueo.Controls.Add(this.label5);
            this.panelArqueo.Controls.Add(this.txtTotalPrincipal);
            this.panelArqueo.Controls.Add(this.label2);
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
            this.panelArqueo.Controls.Add(this.btnEnviar);
            this.panelArqueo.Controls.Add(this.txtFecha);
            this.panelArqueo.Controls.Add(this.lblFecha);
            this.panelArqueo.Controls.Add(this.label1);
            this.panelArqueo.Location = new System.Drawing.Point(28, 33);
            this.panelArqueo.Name = "panelArqueo";
            this.panelArqueo.Size = new System.Drawing.Size(1640, 484);
            this.panelArqueo.TabIndex = 9;
            this.panelArqueo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelArqueo_Paint);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkViolet;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(170, 417);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(138, 54);
            this.btnPrint.TabIndex = 31;
            this.btnPrint.Text = " Imprimir";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(24, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 30);
            this.label10.TabIndex = 28;
            this.label10.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObservaciones.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(28, 266);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(1593, 144);
            this.txtObservaciones.TabIndex = 27;
            // 
            // lblTotalProrroga
            // 
            this.lblTotalProrroga.AutoSize = true;
            this.lblTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProrroga.ForeColor = System.Drawing.Color.Black;
            this.lblTotalProrroga.Location = new System.Drawing.Point(1246, 141);
            this.lblTotalProrroga.Name = "lblTotalProrroga";
            this.lblTotalProrroga.Size = new System.Drawing.Size(31, 34);
            this.lblTotalProrroga.TabIndex = 26;
            this.lblTotalProrroga.Text = "0";
            // 
            // lblTotalRetirados
            // 
            this.lblTotalRetirados.AutoSize = true;
            this.lblTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRetirados.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRetirados.Location = new System.Drawing.Point(890, 141);
            this.lblTotalRetirados.Name = "lblTotalRetirados";
            this.lblTotalRetirados.Size = new System.Drawing.Size(31, 34);
            this.lblTotalRetirados.TabIndex = 25;
            this.lblTotalRetirados.Text = "0";
            // 
            // lblTotalVencidos
            // 
            this.lblTotalVencidos.AutoSize = true;
            this.lblTotalVencidos.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVencidos.ForeColor = System.Drawing.Color.Black;
            this.lblTotalVencidos.Location = new System.Drawing.Point(549, 141);
            this.lblTotalVencidos.Name = "lblTotalVencidos";
            this.lblTotalVencidos.Size = new System.Drawing.Size(31, 34);
            this.lblTotalVencidos.TabIndex = 24;
            this.lblTotalVencidos.Text = "0";
            // 
            // lblTotalAlDia
            // 
            this.lblTotalAlDia.AutoSize = true;
            this.lblTotalAlDia.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAlDia.ForeColor = System.Drawing.Color.Black;
            this.lblTotalAlDia.Location = new System.Drawing.Point(178, 141);
            this.lblTotalAlDia.Name = "lblTotalAlDia";
            this.lblTotalAlDia.Size = new System.Drawing.Size(31, 34);
            this.lblTotalAlDia.TabIndex = 23;
            this.lblTotalAlDia.Text = "0";
            // 
            // lblTotalPrincipal
            // 
            this.lblTotalPrincipal.AutoSize = true;
            this.lblTotalPrincipal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrincipal.ForeColor = System.Drawing.Color.Black;
            this.lblTotalPrincipal.Location = new System.Drawing.Point(885, 48);
            this.lblTotalPrincipal.Name = "lblTotalPrincipal";
            this.lblTotalPrincipal.Size = new System.Drawing.Size(31, 34);
            this.lblTotalPrincipal.TabIndex = 22;
            this.lblTotalPrincipal.Text = "0";
            // 
            // txtTotalGeneral
            // 
            this.txtTotalGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalGeneral.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalGeneral.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalGeneral.Location = new System.Drawing.Point(1376, 87);
            this.txtTotalGeneral.Name = "txtTotalGeneral";
            this.txtTotalGeneral.Size = new System.Drawing.Size(246, 34);
            this.txtTotalGeneral.TabIndex = 21;
            this.txtTotalGeneral.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(1371, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 30);
            this.label9.TabIndex = 20;
            this.label9.Text = "Total General";
            // 
            // txtTotalAlDia
            // 
            this.txtTotalAlDia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalAlDia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalAlDia.Enabled = false;
            this.txtTotalAlDia.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAlDia.Location = new System.Drawing.Point(30, 182);
            this.txtTotalAlDia.Name = "txtTotalAlDia";
            this.txtTotalAlDia.Size = new System.Drawing.Size(246, 34);
            this.txtTotalAlDia.TabIndex = 19;
            this.txtTotalAlDia.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(26, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 30);
            this.label8.TabIndex = 18;
            this.label8.Text = "Total Al día";
            // 
            // txtTotalIntereses
            // 
            this.txtTotalIntereses.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalIntereses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalIntereses.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalIntereses.Location = new System.Drawing.Point(1042, 87);
            this.txtTotalIntereses.Name = "txtTotalIntereses";
            this.txtTotalIntereses.Size = new System.Drawing.Size(246, 34);
            this.txtTotalIntereses.TabIndex = 17;
            this.txtTotalIntereses.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(1038, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 30);
            this.label5.TabIndex = 16;
            this.label5.Text = "Total Intereses";
            // 
            // txtTotalPrincipal
            // 
            this.txtTotalPrincipal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalPrincipal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrincipal.Location = new System.Drawing.Point(710, 87);
            this.txtTotalPrincipal.Name = "txtTotalPrincipal";
            this.txtTotalPrincipal.Size = new System.Drawing.Size(246, 34);
            this.txtTotalPrincipal.TabIndex = 15;
            this.txtTotalPrincipal.Text = "0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(705, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 30);
            this.label2.TabIndex = 14;
            this.label2.Text = "Total Principal";
            // 
            // txtTotalProrroga
            // 
            this.txtTotalProrroga.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalProrroga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalProrroga.Enabled = false;
            this.txtTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalProrroga.Location = new System.Drawing.Point(1047, 182);
            this.txtTotalProrroga.Name = "txtTotalProrroga";
            this.txtTotalProrroga.Size = new System.Drawing.Size(246, 34);
            this.txtTotalProrroga.TabIndex = 13;
            this.txtTotalProrroga.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(1041, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(214, 30);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total en Prórroga";
            // 
            // txtTotalRetirados
            // 
            this.txtTotalRetirados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalRetirados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalRetirados.Enabled = false;
            this.txtTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRetirados.Location = new System.Drawing.Point(704, 182);
            this.txtTotalRetirados.Name = "txtTotalRetirados";
            this.txtTotalRetirados.Size = new System.Drawing.Size(246, 34);
            this.txtTotalRetirados.TabIndex = 11;
            this.txtTotalRetirados.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(699, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Retirados";
            // 
            // txtTotalVencido
            // 
            this.txtTotalVencido.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalVencido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalVencido.Enabled = false;
            this.txtTotalVencido.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVencido.Location = new System.Drawing.Point(363, 182);
            this.txtTotalVencido.Name = "txtTotalVencido";
            this.txtTotalVencido.Size = new System.Drawing.Size(246, 34);
            this.txtTotalVencido.TabIndex = 9;
            this.txtTotalVencido.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(357, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total Vencidos";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(458, 417);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 54);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = " Eliminar";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmpleado.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(363, 87);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(246, 34);
            this.txtEmpleado.TabIndex = 6;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmpleado.Location = new System.Drawing.Point(357, 57);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(136, 30);
            this.lblEmpleado.TabIndex = 5;
            this.lblEmpleado.Text = "Empleado";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gold;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(312, 417);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(141, 54);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.ForeColor = System.Drawing.Color.White;
            this.btnEnviar.Image = global::Empeño.WindowsForms.Properties.Resources.airplane_11_16;
            this.btnEnviar.Location = new System.Drawing.Point(30, 417);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(135, 54);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = " Enviar";
            this.btnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnGuardarCierreCaja_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFecha.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(28, 87);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(246, 34);
            this.txtFecha.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.DimGray;
            this.lblFecha.Location = new System.Drawing.Point(24, 57);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(88, 30);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(24, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arqueo";
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
            // frmArqueo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1782, 1106);
            this.Controls.Add(this.panelDetalle);
            this.Controls.Add(this.panelArqueo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmArqueo";
            this.Text = "Arqueo";
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
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalProrroga;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalRetirados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalVencido;
        private System.Windows.Forms.Label label3;
        private ns1.BunifuElipse elipsePanelArqueo;
        private ns1.BunifuElipse elipseDetalles;
        private System.Windows.Forms.TextBox txtTotalPrincipal;
        private System.Windows.Forms.Label label2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox txtTotalIntereses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalAlDia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalGeneral;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalProrroga;
        private System.Windows.Forms.Label lblTotalRetirados;
        private System.Windows.Forms.Label lblTotalVencidos;
        private System.Windows.Forms.Label lblTotalAlDia;
        private System.Windows.Forms.Label lblTotalPrincipal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button btnPrint;
    }
}