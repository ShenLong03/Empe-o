using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Empeño.Forms.Views
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmConfiguracionGeneral frmConfiguracionGeneral = new frmConfiguracionGeneral();
            frmConfiguracionGeneral.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEmpleado frmEmpleado = new frmEmpleado();

            frmEmpleado.ShowDialog();
        }
    }
}
