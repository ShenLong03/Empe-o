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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleteCierreCaja = new System.Windows.Forms.Button();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.btnCancelarCierreCaja = new System.Windows.Forms.Button();
            this.btnGuardarCierreCaja = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDetalleCierre = new System.Windows.Forms.Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteDetalle = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarDetalle = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.elipseDetalleCierre = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelCierreInformacion.SuspendLayout();
            this.panelDetalleCierre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
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
            this.panelCierreInformacion.Controls.Add(this.textBox1);
            this.panelCierreInformacion.Controls.Add(this.label4);
            this.panelCierreInformacion.Controls.Add(this.txtTotal);
            this.panelCierreInformacion.Controls.Add(this.label3);
            this.panelCierreInformacion.Controls.Add(this.btnDeleteCierreCaja);
            this.panelCierreInformacion.Controls.Add(this.txtEmpleado);
            this.panelCierreInformacion.Controls.Add(this.lblEmpleado);
            this.panelCierreInformacion.Controls.Add(this.btnCancelarCierreCaja);
            this.panelCierreInformacion.Controls.Add(this.btnGuardarCierreCaja);
            this.panelCierreInformacion.Controls.Add(this.txtFecha);
            this.panelCierreInformacion.Controls.Add(this.lblFecha);
            this.panelCierreInformacion.Controls.Add(this.label1);
            this.panelCierreInformacion.Location = new System.Drawing.Point(25, 26);
            this.panelCierreInformacion.Name = "panelCierreInformacion";
            this.panelCierreInformacion.Size = new System.Drawing.Size(1087, 203);
            this.panelCierreInformacion.TabIndex = 1;
            this.panelCierreInformacion.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCierreInformacion_Paint_1);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(585, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 29);
            this.textBox1.TabIndex = 11;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(581, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "Saldo Inicial";
            this.label4.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Enabled = false;
            this.txtTotal.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(863, 87);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(205, 29);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(862, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total";
            this.label3.Visible = false;
            // 
            // btnDeleteCierreCaja
            // 
            this.btnDeleteCierreCaja.BackColor = System.Drawing.Color.Crimson;
            this.btnDeleteCierreCaja.FlatAppearance.BorderSize = 0;
            this.btnDeleteCierreCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCierreCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteCierreCaja.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCierreCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCierreCaja.Image")));
            this.btnDeleteCierreCaja.Location = new System.Drawing.Point(271, 133);
            this.btnDeleteCierreCaja.Name = "btnDeleteCierreCaja";
            this.btnDeleteCierreCaja.Size = new System.Drawing.Size(100, 45);
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
            this.txtEmpleado.Location = new System.Drawing.Point(300, 87);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(205, 29);
            this.txtEmpleado.TabIndex = 6;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmpleado.Location = new System.Drawing.Point(296, 61);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(113, 23);
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
            this.btnCancelarCierreCaja.Location = new System.Drawing.Point(150, 133);
            this.btnCancelarCierreCaja.Name = "btnCancelarCierreCaja";
            this.btnCancelarCierreCaja.Size = new System.Drawing.Size(117, 45);
            this.btnCancelarCierreCaja.TabIndex = 4;
            this.btnCancelarCierreCaja.Text = " Cancelar";
            this.btnCancelarCierreCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelarCierreCaja.UseVisualStyleBackColor = false;
            this.btnCancelarCierreCaja.Click += new System.EventHandler(this.btnCancelarCierreCaja_Click);
            // 
            // btnGuardarCierreCaja
            // 
            this.btnGuardarCierreCaja.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGuardarCierreCaja.FlatAppearance.BorderSize = 0;
            this.btnGuardarCierreCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCierreCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCierreCaja.ForeColor = System.Drawing.Color.White;
            this.btnGuardarCierreCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarCierreCaja.Image")));
            this.btnGuardarCierreCaja.Location = new System.Drawing.Point(25, 133);
            this.btnGuardarCierreCaja.Name = "btnGuardarCierreCaja";
            this.btnGuardarCierreCaja.Size = new System.Drawing.Size(119, 45);
            this.btnGuardarCierreCaja.TabIndex = 3;
            this.btnGuardarCierreCaja.Text = " Guardar";
            this.btnGuardarCierreCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarCierreCaja.UseVisualStyleBackColor = false;
            this.btnGuardarCierreCaja.Click += new System.EventHandler(this.btnGuardarCierreCaja_Click);
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
            this.label1.Size = new System.Drawing.Size(149, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cierre Caja";
            // 
            // panelDetalleCierre
            // 
            this.panelDetalleCierre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalleCierre.BackColor = System.Drawing.Color.Gainsboro;
            this.panelDetalleCierre.Controls.Add(this.dgvDetalles);
            this.panelDetalleCierre.Controls.Add(this.txtCantidad);
            this.panelDetalleCierre.Controls.Add(this.label2);
            this.panelDetalleCierre.Controls.Add(this.btnDeleteDetalle);
            this.panelDetalleCierre.Controls.Add(this.btnCancelar);
            this.panelDetalleCierre.Controls.Add(this.btnGuardarDetalle);
            this.panelDetalleCierre.Controls.Add(this.panel2);
            this.panelDetalleCierre.Controls.Add(this.txtValor);
            this.panelDetalleCierre.Controls.Add(this.lblValor);
            this.panelDetalleCierre.Controls.Add(this.txtConcepto);
            this.panelDetalleCierre.Controls.Add(this.label5);
            this.panelDetalleCierre.Controls.Add(this.label6);
            this.panelDetalleCierre.Location = new System.Drawing.Point(25, 256);
            this.panelDetalleCierre.Name = "panelDetalleCierre";
            this.panelDetalleCierre.Size = new System.Drawing.Size(1087, 554);
            this.panelDetalleCierre.TabIndex = 8;
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
            this.dgvDetalles.Location = new System.Drawing.Point(25, 203);
            this.dgvDetalles.Margin = new System.Windows.Forms.Padding(4);
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
            this.dgvDetalles.Size = new System.Drawing.Size(1043, 325);
            this.dgvDetalles.TabIndex = 24;
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCantidad.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(585, 86);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(205, 29);
            this.txtCantidad.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(581, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Cantidad";
            // 
            // btnDeleteDetalle
            // 
            this.btnDeleteDetalle.BackColor = System.Drawing.Color.Crimson;
            this.btnDeleteDetalle.FlatAppearance.BorderSize = 0;
            this.btnDeleteDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDetalle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteDetalle.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDetalle.Image")));
            this.btnDeleteDetalle.Location = new System.Drawing.Point(280, 130);
            this.btnDeleteDetalle.Name = "btnDeleteDetalle";
            this.btnDeleteDetalle.Size = new System.Drawing.Size(100, 45);
            this.btnDeleteDetalle.TabIndex = 10;
            this.btnDeleteDetalle.Text = " Eliminar";
            this.btnDeleteDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteDetalle.UseVisualStyleBackColor = false;
            this.btnDeleteDetalle.Click += new System.EventHandler(this.btnDeleteDetalle_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gold;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(159, 130);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 45);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarDetalle
            // 
            this.btnGuardarDetalle.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGuardarDetalle.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnGuardarDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarDetalle.Image")));
            this.btnGuardarDetalle.Location = new System.Drawing.Point(25, 130);
            this.btnGuardarDetalle.Name = "btnGuardarDetalle";
            this.btnGuardarDetalle.Size = new System.Drawing.Size(129, 45);
            this.btnGuardarDetalle.TabIndex = 8;
            this.btnGuardarDetalle.Text = " Agregar";
            this.btnGuardarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarDetalle.UseVisualStyleBackColor = false;
            this.btnGuardarDetalle.Click += new System.EventHandler(this.btnGuardarDetalle_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(16, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1062, 1);
            this.panel2.TabIndex = 7;
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtValor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValor.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Location = new System.Drawing.Point(301, 86);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(205, 29);
            this.txtValor.TabIndex = 6;
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.ForeColor = System.Drawing.Color.DimGray;
            this.lblValor.Location = new System.Drawing.Point(296, 60);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(63, 23);
            this.lblValor.TabIndex = 5;
            this.lblValor.Text = "Valor";
            // 
            // txtConcepto
            // 
            this.txtConcepto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtConcepto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConcepto.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcepto.Location = new System.Drawing.Point(24, 86);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(205, 29);
            this.txtConcepto.TabIndex = 5;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(20, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Concepto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(20, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(227, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Detalles de Cierre";
            // 
            // elipseDetalleCierre
            // 
            this.elipseDetalleCierre.ElipseRadius = 20;
            this.elipseDetalleCierre.TargetControl = this.panelDetalleCierre;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this.btnGuardarCierreCaja;
            // 
            // frmCierreCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1161, 834);
            this.Controls.Add(this.panelDetalleCierre);
            this.Controls.Add(this.panelCierreInformacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCierreCaja";
            this.Text = "Cierre Caja";
            this.Load += new System.EventHandler(this.frmCierreCaja_Load);
            this.panelCierreInformacion.ResumeLayout(false);
            this.panelCierreInformacion.PerformLayout();
            this.panelDetalleCierre.ResumeLayout(false);
            this.panelDetalleCierre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuElipse elipsePanelCierreInformacion;
        private System.Windows.Forms.Panel panelCierreInformacion;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Button btnCancelarCierreCaja;
        private System.Windows.Forms.Button btnGuardarCierreCaja;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDetalleCierre;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDeleteCierreCaja;
        private Bunifu.Framework.UI.BunifuElipse elipseDetalleCierre;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Button btnDeleteDetalle;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardarDetalle;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDetalles;
    }
}