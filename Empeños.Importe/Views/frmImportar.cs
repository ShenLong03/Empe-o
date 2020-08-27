using Empeño.CommonEF.Entities;
using Empeños.Importe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeños.Importe
{
    public partial class frmImportar : System.Windows.Forms.Form
    {
        OldEmpeños _oldContext = new OldEmpeños();

        public frmImportar()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          
        }

    }
}
