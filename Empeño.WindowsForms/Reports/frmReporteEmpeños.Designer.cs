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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.rvEmpeños = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ckbBorrados = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Size = new System.Drawing.Size(1413, 202);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 198);
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
            this.panel2.Controls.Add(this.rvEmpeños);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1413, 702);
            this.panel2.TabIndex = 6;
            // 
            // rvEmpeños
            // 
            this.rvEmpeños.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvEmpeños.Location = new System.Drawing.Point(0, 0);
            this.rvEmpeños.Name = "rvEmpeños";
            this.rvEmpeños.ServerReport.BearerToken = null;
            this.rvEmpeños.Size = new System.Drawing.Size(1413, 702);
            this.rvEmpeños.TabIndex = 5;
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
            // frmReporteEmpeños
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.panel2.ResumeLayout(false);
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
        private Microsoft.Reporting.WinForms.ReportViewer rvEmpeños;
        private System.Windows.Forms.CheckBox chbPerdidos;
        private System.Windows.Forms.CheckBox chbRetirados;
        private System.Windows.Forms.CheckBox chbVencidos;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ckbBorrados;
    }
}