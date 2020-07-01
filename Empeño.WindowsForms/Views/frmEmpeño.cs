﻿using Empeño.WindowsForms.Data;
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
    public partial class frmEmpeño : Form
    {
        DataContext _context = new DataContext();
        public frmEmpeño()
        {
            InitializeComponent();
        }

        private void frmEmpeño_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = _context.Empleados.Select(x=>new 
            {
                Id=x.EmpleadoId,
                x.Nombre,
                x.Correo,
                x.Telefono,
            }).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
