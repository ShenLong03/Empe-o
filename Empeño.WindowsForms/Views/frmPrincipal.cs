using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms
{
    public partial class frmPrincipal : System.Windows.Forms.Form
    {

        DataContext _context = new DataContext();
        
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
