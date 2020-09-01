namespace Empeño.WindowsForms.Views
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArqueo));
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.panelArqueo = new System.Windows.Forms.Panel();
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
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.elipsePanelArqueo = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.elipseDetalles = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.lblTotalPrincipal = new System.Windows.Forms.Label();
            this.lblTotalAlDia = new System.Windows.Forms.Label();
            this.lblTotalVencidos = new System.Windows.Forms.Label();
            this.lblTotalRetirados = new System.Windows.Forms.Label();
            this.lblTotalProrroga = new System.Windows.Forms.Label();
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
            this.panelDetalle.Location = new System.Drawing.Point(24, 346);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(1376, 475);
            this.panelDetalle.TabIndex = 10;
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalles.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDetalles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalles.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalles.Location = new System.Drawing.Point(25, 65);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.RowHeadersWidth = 51;
            this.dgvDetalles.RowTemplate.Height = 24;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(1327, 374);
            this.dgvDetalles.TabIndex = 8;
            this.dgvDetalles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalles_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(20, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(242, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Detalles de Arqueo";
            // 
            // panelArqueo
            // 
            this.panelArqueo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArqueo.BackColor = System.Drawing.Color.LightGray;
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
            this.panelArqueo.Controls.Add(this.btnImprimir);
            this.panelArqueo.Controls.Add(this.txtFecha);
            this.panelArqueo.Controls.Add(this.lblFecha);
            this.panelArqueo.Controls.Add(this.label1);
            this.panelArqueo.Location = new System.Drawing.Point(24, 28);
            this.panelArqueo.Name = "panelArqueo";
            this.panelArqueo.Size = new System.Drawing.Size(1376, 293);
            this.panelArqueo.TabIndex = 9;
            this.panelArqueo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelArqueo_Paint);
            // 
            // txtTotalGeneral
            // 
            this.txtTotalGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalGeneral.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalGeneral.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalGeneral.Location = new System.Drawing.Point(1146, 87);
            this.txtTotalGeneral.Name = "txtTotalGeneral";
            this.txtTotalGeneral.Size = new System.Drawing.Size(205, 29);
            this.txtTotalGeneral.TabIndex = 21;
            this.txtTotalGeneral.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(1142, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 23);
            this.label9.TabIndex = 20;
            this.label9.Text = "Total General";
            // 
            // txtTotalAlDia
            // 
            this.txtTotalAlDia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalAlDia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalAlDia.Enabled = false;
            this.txtTotalAlDia.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAlDia.Location = new System.Drawing.Point(25, 165);
            this.txtTotalAlDia.Name = "txtTotalAlDia";
            this.txtTotalAlDia.Size = new System.Drawing.Size(205, 29);
            this.txtTotalAlDia.TabIndex = 19;
            this.txtTotalAlDia.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(21, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 23);
            this.label8.TabIndex = 18;
            this.label8.Text = "Total Al día";
            // 
            // txtTotalIntereses
            // 
            this.txtTotalIntereses.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalIntereses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalIntereses.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalIntereses.Location = new System.Drawing.Point(869, 87);
            this.txtTotalIntereses.Name = "txtTotalIntereses";
            this.txtTotalIntereses.Size = new System.Drawing.Size(205, 29);
            this.txtTotalIntereses.TabIndex = 17;
            this.txtTotalIntereses.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(865, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Total Intereses";
            // 
            // txtTotalPrincipal
            // 
            this.txtTotalPrincipal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalPrincipal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrincipal.Location = new System.Drawing.Point(591, 87);
            this.txtTotalPrincipal.Name = "txtTotalPrincipal";
            this.txtTotalPrincipal.Size = new System.Drawing.Size(205, 29);
            this.txtTotalPrincipal.TabIndex = 15;
            this.txtTotalPrincipal.Text = "0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(587, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Total Principal";
            // 
            // txtTotalProrroga
            // 
            this.txtTotalProrroga.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalProrroga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalProrroga.Enabled = false;
            this.txtTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalProrroga.Location = new System.Drawing.Point(872, 165);
            this.txtTotalProrroga.Name = "txtTotalProrroga";
            this.txtTotalProrroga.Size = new System.Drawing.Size(205, 29);
            this.txtTotalProrroga.TabIndex = 13;
            this.txtTotalProrroga.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(868, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total en Prórroga";
            // 
            // txtTotalRetirados
            // 
            this.txtTotalRetirados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalRetirados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalRetirados.Enabled = false;
            this.txtTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRetirados.Location = new System.Drawing.Point(586, 165);
            this.txtTotalRetirados.Name = "txtTotalRetirados";
            this.txtTotalRetirados.Size = new System.Drawing.Size(205, 29);
            this.txtTotalRetirados.TabIndex = 11;
            this.txtTotalRetirados.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(582, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Retirados";
            // 
            // txtTotalVencido
            // 
            this.txtTotalVencido.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalVencido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalVencido.Enabled = false;
            this.txtTotalVencido.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVencido.Location = new System.Drawing.Point(302, 165);
            this.txtTotalVencido.Name = "txtTotalVencido";
            this.txtTotalVencido.Size = new System.Drawing.Size(205, 29);
            this.txtTotalVencido.TabIndex = 9;
            this.txtTotalVencido.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(298, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 23);
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
            this.btnDelete.Location = new System.Drawing.Point(249, 230);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 45);
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
            this.txtEmpleado.Location = new System.Drawing.Point(302, 87);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(205, 29);
            this.txtEmpleado.TabIndex = 6;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmpleado.Location = new System.Drawing.Point(298, 61);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(113, 23);
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
            this.btnCancelar.Location = new System.Drawing.Point(128, 230);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 45);
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
            this.btnImprimir.Location = new System.Drawing.Point(24, 230);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(100, 45);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = " Imprimir";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnGuardarCierreCaja_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFecha.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(24, 87);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(205, 29);
            this.txtFecha.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.DimGray;
            this.lblFecha.Location = new System.Drawing.Point(20, 61);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(72, 23);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(20, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
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
            // lblTotalPrincipal
            // 
            this.lblTotalPrincipal.AutoSize = true;
            this.lblTotalPrincipal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrincipal.ForeColor = System.Drawing.Color.Black;
            this.lblTotalPrincipal.Location = new System.Drawing.Point(738, 54);
            this.lblTotalPrincipal.Name = "lblTotalPrincipal";
            this.lblTotalPrincipal.Size = new System.Drawing.Size(26, 30);
            this.lblTotalPrincipal.TabIndex = 22;
            this.lblTotalPrincipal.Text = "0";
            // 
            // lblTotalAlDia
            // 
            this.lblTotalAlDia.AutoSize = true;
            this.lblTotalAlDia.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAlDia.ForeColor = System.Drawing.Color.Black;
            this.lblTotalAlDia.Location = new System.Drawing.Point(149, 132);
            this.lblTotalAlDia.Name = "lblTotalAlDia";
            this.lblTotalAlDia.Size = new System.Drawing.Size(26, 30);
            this.lblTotalAlDia.TabIndex = 23;
            this.lblTotalAlDia.Text = "0";
            // 
            // lblTotalVencidos
            // 
            this.lblTotalVencidos.AutoSize = true;
            this.lblTotalVencidos.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVencidos.ForeColor = System.Drawing.Color.Black;
            this.lblTotalVencidos.Location = new System.Drawing.Point(458, 132);
            this.lblTotalVencidos.Name = "lblTotalVencidos";
            this.lblTotalVencidos.Size = new System.Drawing.Size(26, 30);
            this.lblTotalVencidos.TabIndex = 24;
            this.lblTotalVencidos.Text = "0";
            // 
            // lblTotalRetirados
            // 
            this.lblTotalRetirados.AutoSize = true;
            this.lblTotalRetirados.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRetirados.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRetirados.Location = new System.Drawing.Point(741, 132);
            this.lblTotalRetirados.Name = "lblTotalRetirados";
            this.lblTotalRetirados.Size = new System.Drawing.Size(26, 30);
            this.lblTotalRetirados.TabIndex = 25;
            this.lblTotalRetirados.Text = "0";
            // 
            // lblTotalProrroga
            // 
            this.lblTotalProrroga.AutoSize = true;
            this.lblTotalProrroga.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProrroga.ForeColor = System.Drawing.Color.Black;
            this.lblTotalProrroga.Location = new System.Drawing.Point(1039, 132);
            this.lblTotalProrroga.Name = "lblTotalProrroga";
            this.lblTotalProrroga.Size = new System.Drawing.Size(26, 30);
            this.lblTotalProrroga.TabIndex = 26;
            this.lblTotalProrroga.Text = "0";
            // 
            // frmArqueo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1423, 843);
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
        private System.Windows.Forms.DataGridView dgvDetalles;
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
    }
}