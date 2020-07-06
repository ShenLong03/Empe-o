using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmCargando : Form
    {
        public frmCargando(Size size, Point location)
        {
            InitializeComponent();

            this.Size = size;
            this.Location = location;
            this.TopMost = true;
        }

        private void frmCargando_Load(object sender, EventArgs e)
        {
        }
    }
}
