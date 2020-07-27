﻿using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using Empeño.WindowsForms.Models;
using Empeño.WindowsForms.ViewReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Views
{
    public partial class frmTablero : Form
    {
        DataContext _context = new DataContext();
        List<Transaccion> transactionsToday = new List<Transaccion>();

        public frmTablero()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private async void frmTablero_Load(object sender, EventArgs e)
        {
            DateTime desde = DateTime.Today.AddMonths(-4);
            DateTime hasta = DateTime.Today;

            var egresos = await _context.Empenos.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();
            var ingresos = await _context.Pago.Where(x => x.Fecha >= desde && x.Fecha <= hasta).ToListAsync();

            var list = new List<IngresosEgresosReporte>();

            foreach (var item in egresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Cliente.Nombre,
                    Egresos = item.Monto,
                    Tipo = "Empeño",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Cliente.Identificacion,
                    Ingresos = null
                });
            }

            foreach (var item in ingresos)
            {
                list.Add(new IngresosEgresosReporte
                {
                    EmpeñoId = item.EmpenoId,
                    Cliente = item.Empeno.Cliente.Nombre,
                    Egresos = null,
                    Tipo = item.TipoPago == CommonEF.Enum.TipoPago.Interes ? "Interes" :
                    item.TipoPago == CommonEF.Enum.TipoPago.Principal ? "Principal" : "",
                    Empleado = item.Empleado.Nombre,
                    Fecha = item.Fecha.Date,
                    Identificacion = item.Empeno.Cliente.Identificacion,
                    Ingresos = item.Monto
                });
            }

            chartVentas.Series[0].Points.DataBindXY(list.OrderBy(i=>i.Fecha).Select(i => i.Fecha.Date.ToString("dd/MM")).ToList(), list.Select(i => i.Ingresos).ToList());
            chartVentas.Series[1].Points.DataBindXY(list.OrderBy(i => i.Fecha).Select(i => i.Fecha.Date.ToString("dd/MM")).ToList(), list.Select(i => i.Egresos).ToList());

            var listEmpeños = _context.Empenos.ToList();

            for (int i = 0; i < 5; i++)
            {
                var estado = GetEstado(i);
                chartEmpeños.Series[0].Points.AddXY(GetEstadoName(i), _context.Empenos.Where(x => x.Estado == estado).Count());
            }
            

            var empeños = _context.Empenos.Where(d => !d.IsDelete).ToList();
            lblTotalEmpeños.Text =  empeños.Count().ToString();
            lblEmpeñosNuevos.Text = empeños.Where(d=>d.Fecha.Month==DateTime.Today.Month).Count().ToString() + " nuevos empeños";
            var clientes = _context.Clientes.Where(c => c.Activo).ToList();
            lblTotalClientes.Text = clientes.Count().ToString();
            lblClientesNuevos.Text = clientes.Where(c => c.Fecha.Month == DateTime.Today.Month).Count() + " nuevos clientes";
            lblTotalVencidos.Text = empeños.Where(d=>d.Estado==Estado.Vencido).Count().ToString();
            var mesAnterior = DateTime.Today.AddDays(-30);
            lblMas30Dias.Text = empeños.Where(d => d.FechaVencimiento>mesAnterior && d.Estado == Estado.Vencido).Count().ToString() + " a más de 30 días";
            lblTotalPendientes.Text = empeños.Where(d => d.Estado == Estado.Pendiente || (d.Intereses.Where(i=>i.FechaVencimiento<=DateTime.Today && i.Monto>i.Pagado).Count()>0)).Count().ToString();
            lblPendientesHoy.Text = empeños.Where(d => d.Intereses.Where(i => i.FechaVencimiento == DateTime.Today && i.Monto>i.Pagado).Count() > 0).Count().ToString() + " para hoy";

            var tomorrow = DateTime.Today.AddDays(1);

            var ttransa = _context.Pago.Where(p => p.Fecha >= DateTime.Today && p.Fecha < tomorrow)
                .Select(x => new
                {
                    Hora = x.Fecha,
                    TipoTransaccion = "Ingresos",
                    Monto = x.Monto
                }).ToList()
                .Select(x => new Transaccion
                {
                    Hora = x.Hora,
                    TipoTransaccion = x.TipoTransaccion,
                    Monto = x.Monto
                });

            transactionsToday.AddRange(ttransa);
            transactionsToday.AddRange(_context.Empenos.Where(p=> p.Fecha >= DateTime.Today && p.Fecha < tomorrow)
                .Select(x=>new Transaccion
                {
                    Hora=x.Fecha,
                    TipoTransaccion="Egresos",
                    Monto=x.Monto*-1
                }));



            dgvTransacciones.DataSource = transactionsToday;
            chartEmpeños.Series[0].IsValueShownAsLabel = true;
            lblIngresos.Text = transactionsToday.Where(t => t.TipoTransaccion == "Ingresos").Sum(t => t.Monto).ToString("N2");
            lblSalidas.Text = transactionsToday.Where(t => t.TipoTransaccion == "Egresos").Sum(t => t.Monto).ToString("N2");
            lblTotal.Text= transactionsToday.Sum(t => t.Monto).ToString("N2");
        }

        private string GetEstadoName(int i)
        {
            switch (i)
            {
                case 0:
                    return "Activo";
                case 1:
                    return "Pendiente";
                case 2:
                    return "Vencido";
                case 3:
                    return "Cancelada";
                case 4:
                    return "Retirada";
                default:
                    return "";
            }
        }

        private Estado GetEstado(int i)
        {
            switch (i)
            {
                case 0:
                    return Estado.Activo;
                case 1:
                    return Estado.Pendiente;
                case 2:
                    return Estado.Vencido;
                case 3:
                    return Estado.Cancelada;
                case 4:
                    return Estado.Retirada;
                default:
                    return Estado.Activo;
            }
        }
    }
}
