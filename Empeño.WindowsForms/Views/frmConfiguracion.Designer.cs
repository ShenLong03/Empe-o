namespace Empeño.WindowsForms.Views
{
    partial class frmConfiguracion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfiguracionGeneral = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnIntereses = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnIntereses);
            this.panel1.Controls.Add(this.btnEmpleados);
            this.panel1.Controls.Add(this.btnConfiguracionGeneral);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-2, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 726);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(-1, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 62);
            this.button1.TabIndex = 1;
            this.button1.Text = "Menu Confirguración";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnConfiguracionGeneral
            // 
            this.btnConfiguracionGeneral.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConfiguracionGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracionGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracionGeneral.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracionGeneral.Location = new System.Drawing.Point(-1, 65);
            this.btnConfiguracionGeneral.Name = "btnConfiguracionGeneral";
            this.btnConfiguracionGeneral.Size = new System.Drawing.Size(224, 62);
            this.btnConfiguracionGeneral.TabIndex = 2;
            this.btnConfiguracionGeneral.Text = "Confirguración General";
            this.btnConfiguracionGeneral.UseVisualStyleBackColor = true;
            this.btnConfiguracionGeneral.Click += new System.EventHandler(this.btnConfiguracionGeneral_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleados.ForeColor = System.Drawing.Color.White;
            this.btnEmpleados.Location = new System.Drawing.Point(-1, 189);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(224, 62);
            this.btnEmpleados.TabIndex = 3;
            this.btnEmpleados.Text = "Empleados";
            this.btnEmpleados.UseVisualStyleBackColor = true;
            // 
            // btnIntereses
            // 
            this.btnIntereses.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnIntereses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntereses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntereses.ForeColor = System.Drawing.Color.White;
            this.btnIntereses.Location = new System.Drawing.Point(-1, 127);
            this.btnIntereses.Name = "btnIntereses";
            this.btnIntereses.Size = new System.Drawing.Size(224, 62);
            this.btnIntereses.TabIndex = 4;
            this.btnIntereses.Text = "Intéreses";
            this.btnIntereses.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Location = new System.Drawing.Point(1388, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 718);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConfiguracion";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIntereses;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnConfiguracionGeneral;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label btnClose;
    }
}