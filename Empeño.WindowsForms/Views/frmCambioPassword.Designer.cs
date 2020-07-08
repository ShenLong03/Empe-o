namespace Empeño.WindowsForms.Views
{
    partial class frmCambioPassword
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
            this.txtViejo = new System.Windows.Forms.TextBox();
            this.txtNuevo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lblViejo = new System.Windows.Forms.Label();
            this.lblNuevo = new System.Windows.Forms.Label();
            this.btnClose = new FontAwesome.Sharp.IconPictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // txtViejo
            // 
            this.txtViejo.BackColor = System.Drawing.SystemColors.Control;
            this.txtViejo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtViejo.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViejo.Location = new System.Drawing.Point(16, 81);
            this.txtViejo.Name = "txtViejo";
            this.txtViejo.Size = new System.Drawing.Size(360, 29);
            this.txtViejo.TabIndex = 0;
            this.txtViejo.Text = "Viejo Password";
            this.txtViejo.Enter += new System.EventHandler(this.txtViejo_Enter);
            this.txtViejo.Leave += new System.EventHandler(this.txtViejo_Leave);
            // 
            // txtNuevo
            // 
            this.txtNuevo.BackColor = System.Drawing.SystemColors.Control;
            this.txtNuevo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNuevo.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevo.Location = new System.Drawing.Point(16, 148);
            this.txtNuevo.Name = "txtNuevo";
            this.txtNuevo.Size = new System.Drawing.Size(360, 29);
            this.txtNuevo.TabIndex = 1;
            this.txtNuevo.Text = "Nuevo Password";
            this.txtNuevo.Enter += new System.EventHandler(this.txtNuevo_Enter);
            this.txtNuevo.Leave += new System.EventHandler(this.txtNuevo_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(19)))), ((int)(((byte)(100)))));
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.iconPictureBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 40);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cambio de Password";
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Pager;
            this.iconPictureBox2.IconColor = System.Drawing.Color.White;
            this.iconPictureBox2.IconSize = 20;
            this.iconPictureBox2.Location = new System.Drawing.Point(9, 9);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(34, 20);
            this.iconPictureBox2.TabIndex = 4;
            this.iconPictureBox2.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnCancelar.IconColor = System.Drawing.Color.White;
            this.btnCancelar.IconSize = 24;
            this.btnCancelar.Location = new System.Drawing.Point(116, 198);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Rotation = 0D;
            this.btnCancelar.Size = new System.Drawing.Size(105, 41);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnGuardar.IconColor = System.Drawing.Color.White;
            this.btnGuardar.IconSize = 24;
            this.btnGuardar.Location = new System.Drawing.Point(16, 199);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Rotation = 0D;
            this.btnGuardar.Size = new System.Drawing.Size(100, 40);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(395, 264);
            this.shapeContainer1.TabIndex = 6;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 14;
            this.lineShape1.X2 = 377;
            this.lineShape1.Y1 = 112;
            this.lineShape1.Y2 = 112;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 13;
            this.lineShape2.X2 = 376;
            this.lineShape2.Y1 = 179;
            this.lineShape2.Y2 = 179;
            // 
            // lblViejo
            // 
            this.lblViejo.AutoSize = true;
            this.lblViejo.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViejo.Location = new System.Drawing.Point(16, 58);
            this.lblViejo.Name = "lblViejo";
            this.lblViejo.Size = new System.Drawing.Size(110, 17);
            this.lblViejo.TabIndex = 7;
            this.lblViejo.Text = "Viejo Password";
            // 
            // lblNuevo
            // 
            this.lblNuevo.AutoSize = true;
            this.lblNuevo.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuevo.Location = new System.Drawing.Point(16, 128);
            this.lblNuevo.Name = "lblNuevo";
            this.lblNuevo.Size = new System.Drawing.Size(117, 17);
            this.lblNuevo.TabIndex = 8;
            this.lblNuevo.Text = "Nuevo Password";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(360, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmCambioPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(395, 264);
            this.Controls.Add(this.lblNuevo);
            this.Controls.Add(this.lblViejo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtNuevo);
            this.Controls.Add(this.txtViejo);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCambioPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambio Password";
            this.Load += new System.EventHandler(this.frmCambioPassword_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtViejo;
        private System.Windows.Forms.TextBox txtNuevo;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label lblViejo;
        private System.Windows.Forms.Label lblNuevo;
        private FontAwesome.Sharp.IconPictureBox btnClose;
    }
}