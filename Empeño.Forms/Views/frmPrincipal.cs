//using Empeño.Common;
//using Empeño.Common.Entities;
using Empeño.Forms.Data;
using Empeño.Forms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empeño.Forms
{
    public partial class frmPrincipal : Form
    {
        private readonly DataContext _context= new DataContext();

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }

        private async void Principal_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            BorrarPanelOrdenes();
            if (txtNombre.TextLength>=3)
            {
                BorrarPanelOrdenes();
                //var list= _context.Clientes.Where(c => c.Nombre.ToUpper().Contains(txtNombre.Text.ToUpper())).ToList();
                TableLayoutPanel panel = tableLayoutPanel1;
       
                panel.RowCount = 0;
                this.tableLayoutPanel1.ColumnCount = 1;                
                bool color = true;
                //foreach (var item in list)
                //{
                //    Panel p = new Panel 
                //    {
                //        Height=50,
                //        Width=1400,
                //        BorderStyle=BorderStyle.FixedSingle,
                //    };
                //    if (color)
                //    {
                //        p.BackColor = Color.LightGray;
                //        color = false;
                //    }
                //    else
                //    {
                //        p.BackColor = Color.White;
                //        color = true;
                //    }
                //    p.Controls.Add(new Label() 
                //    { 
                //        Text = item.Nombre, 
                //        Location = new Point(0, 10), 
                //        ForeColor=Color.Blue,
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                //    });
                //    p.Controls.Add(new Label()
                //    {
                //        Text = item.Telefono,
                //        Location = new Point(300, 10),
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                //    });
                //    p.Controls.Add(new Label()
                //    {
                //        Width = 200,
                //        Text = item.Correo,
                //        Location = new Point(650, 10),
                //        ForeColor = Color.Blue,
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                //    });                   
           
                //    p.Controls.Add(new PictureBox()
                //    {
                //        Location = new Point(1060, 13),
                //        Height = 25,
                //        Width = 25,
                //        BackgroundImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\073-dollar-3.png"),                    
                //        BackgroundImageLayout=ImageLayout.Stretch,
                //        BackColor = Color.Gold
                //    });
                //    p.Controls.Add(new Button()
                //    {
                //        Text = "Empeño",
                //        Location = new Point(1050, 5),
                //        Height = 40,
                //        Width = 120,
                //        TextAlign = ContentAlignment.MiddleRight,
                //        BackColor = Color.Gold,
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                //        ForeColor=Color.White
                //    });

                //    p.Controls.Add(new PictureBox()
                //    {
                //        Location = new Point(1190, 13),
                //        Height = 25,
                //        Width = 25,
                //        BackgroundImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\154-dollar-2.png"),
                //        BackgroundImageLayout = ImageLayout.Stretch,
                //        BackColor = Color.Blue
                //    });
                //    p.Controls.Add(new Button()
                //    {
                //        Text = "Ver",
                //        Location = new Point(1180, 5),
                //        Height = 40,
                //        Width = 80,
                //        TextAlign = ContentAlignment.MiddleRight,
                //        BackColor = Color.Blue,
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                //        ForeColor = Color.White
                //    });

                //    p.Controls.Add(new PictureBox()
                //    {
                //        Location = new Point(1280, 13),
                //        Height = 25,
                //        Width = 25,
                //        BackgroundImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\017-id-card.png"),
                //        BackgroundImageLayout = ImageLayout.Stretch,
                //        BackColor = Color.Black
                //    });
                //    p.Controls.Add(new Button()
                //    {
                //        Text = "Perfil",
                //        Location = new Point(1270, 5),
                //        Height = 40,
                //        Width = 100,
                //        TextAlign = ContentAlignment.MiddleRight,
                //        BackColor = Color.Black,
                //        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                //        ForeColor = Color.White
                //    });
                   
                //    panel.RowCount = panel.RowCount + 1;
                //    panel.Controls.Add(p, 1, panel.RowCount - 1);                    
                //}
                TableLayoutRowStyleCollection styles = tableLayoutPanel1.RowStyles;
                foreach (RowStyle style in styles)
                {
                    style.SizeType = SizeType.Absolute;
                    if (style== styles[0])
                    {
                        style.Height = 60;
                    }
                    else
                    {
                        style.Height = 50;
                    }
                }
            }
        }

        private void BorrarPanelOrdenes()
        {
            //Program.SetDoubleBuffered(tlpOrdenesClientes);
            this.tableLayoutPanel1.SuspendLayout();
            var totaldos = tableLayoutPanel1.Controls.Count;
            var litdos = tableLayoutPanel1.Controls.OfType<Button>();
            for (int i = 0; i < totaldos; i++)
            {
                tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.Controls[0]);
            }
            this.tableLayoutPanel1.ResumeLayout();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Views.frmOscuro oscuro = new Views.frmOscuro();

            try
            {                
                oscuro.Show();
                Views.frmConfiguracion frm = new Views.frmConfiguracion();
                frm.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                oscuro.Close();
            }            
        }

        private void btnEmpeno_Click(object sender, EventArgs e)
        {
            frmOscuro oscuro = new Views.frmOscuro();           
            oscuro.Show();
            frmEmpeno frmEmpeno = new frmEmpeno();
            frmEmpeno.ShowDialog();
            oscuro.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
