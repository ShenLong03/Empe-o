using Empeño.CommonEF.Entities;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empeño.WindowsForms.Funciones
{
    public class EmailFuncion
    {
        DataContext _context = new DataContext();

        public async Task SendMail(string to, string subject, Empeno empeño)
        {
            var cliente = _context.Clientes.Find(empeño.ClienteId);
            var empleado = _context.Empleados.Find(empeño.EmpleadoId);
            var interes = _context.Interes.Find(empeño.InteresId);
            var configuracion = _context.Configuraciones.FirstOrDefault();

            var str = "Estimado " + cliente.Nombre + ", <br /><br />"
                + "Se a creado un Empeño <b>#" + empeño.EmpenoId + "</b> por : <br /><i>" + empeño.Descripcion + "</i>. <br /><br />"
                + "<b>Fecha</b> : " + empeño.Fecha.ToString("dd/MM/yyyy") + "<br />"
                + "<b>Realizado por</b>  : " + empleado.Nombre + "<br />"
                + "<b>Monto del Empeño</b> : " + empeño.Monto.ToString("N2") + "<br />"
                + "<b>Interes Mensual</b> : " + (empeño.Monto * ((double)interes.Porcentaje / 100)).ToString("N2") + "<br />"
                + "<b>Estado</b> : " + empeño.Estado.ToString() + "<br />"
                + "<b>Fecha de Vencimiento</b> : " + empeño.FechaVencimiento.ToString("dd/MM/yyyy")
                + "<br /><br />Saludos.";

            await SendMail(to,subject, str);

        }

        public async Task SendMail(string to, string subject, string body, Empeno empeño)
        {
            var cliente = _context.Clientes.Find(empeño.ClienteId);
            var empleado = _context.Empleados.Find(empeño.EmpleadoId);
            var interes = _context.Interes.Find(empeño.InteresId);
            var configuracion = _context.Configuraciones.FirstOrDefault();

            var str = "Estimado " + cliente.Nombre + ", <br /><br />"
                + body
                + "<b>Fecha</b> : " + empeño.Fecha.ToString("dd/MM/yyyy") + "<br />"
                + "<b>Realizado por</b>  : " + empleado.Nombre + "<br />"
                + "<b>Monto del Empeño</b> : " + empeño.Monto.ToString("N2") + "<br />"
                + "<b>Interes Mensual</b> : " + (empeño.Monto * ((double)interes.Porcentaje / 100)).ToString("N2") + "<br />"
                + "<b>Estado</b> : " + empeño.Estado.ToString() + "<br />"
                + "<b>Fecha de Vencimiento</b> : " + empeño.FechaVencimiento.ToString("dd/MM/yyyy")
                + "<br /><br />Saludos.";

            await SendMail(to, subject, str);

        }

        public async Task SendMail(string to, string subject, string body, List<DetalleCierreCaja> detalle)
        {
            var cierreCaja = _context.CierreCajas.Find(detalle.FirstOrDefault().CierreCajaId);
            var empleado = _context.Empleados.Find(cierreCaja.EmpleadoId);
            var configuracion = _context.Configuraciones.FirstOrDefault();

            var str = "Estimado " + configuracion.Nombre + ", <br /><br />"
                + body
                + "<b>Fecha</b> : " + cierreCaja.Fecha.ToString("dd/MM/yyyy") + "<br />"
                + "<b>Realizado por</b>  : " + empleado.Nombre + "<br />"
                + "<table><thead><tr><th>Concepto</th><th>Valor</th></tr></thead><tbody>";
                foreach (var item in detalle)
                {
                    str += "<tr><td>" + item.Concepto + "</td>" 
                     + "<td>" + item.Valor + "</td>";
                }
                str+="</tbody></table>"               
                + "<br /><br />Saludos.";

            await SendMail(to, subject, str);

        }

        public async Task SendMailArqueo(string to, string subject, string body, DataGridView detalle, string totalesStr)
        {
            var empleado = _context.Empleados.Find(Program.EmpleadoId);
            var configuracion = _context.Configuraciones.FirstOrDefault();

            var str = "<p>Estimado " + configuracion.Nombre + ", <br /><br />"
                + body + "</p><p>"
                + "<b>Fecha</b> : " + DateTime.Today.ToString("dd/MM/yyyy") + "<br />"
                + "<b>Realizado por</b>  : " + empleado.Nombre + "<br /></p>"
                + "<h3>Detalles</h3><hr />"
                + "<table><thead><tr><th>EmpeñoId</th><th>Identificación</th><th>Nombre</th><th>Descripción</th><th>Estado</th><th>SubTotal</th></tr></thead><tbody>";
            foreach (DataGridViewRow item in detalle.Rows)
            {
                str += "<tr><td>" + item.Cells[0].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[2].Value.ToString()  + "</td>" 
                    + "<td>" + item.Cells[3].Value.ToString() + "</td>" 
                    + "<td>" + item.Cells[1].Value.ToString() + "</td>" 
                    + "<td>" + item.Cells[4].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[9].Value.ToString() + "</td>";
            }
            str += "</tbody></table><hr />" 
                + totalesStr
                + "<br /><br />Saludos.";

            await SendMail(to, subject, str);
        }

        public async Task SendMailVencidos(string to, string subject, string body, DataGridView detalle, string totalesStr)
        {
            var empleado = _context.Empleados.Find(Program.EmpleadoId);
            var configuracion = _context.Configuraciones.FirstOrDefault();

            var str = "<p>Estimado " + configuracion.Nombre + ", <br /><br />"
                + body + "</p><p>"
                + "<b>Fecha</b> : " + DateTime.Today.ToString("dd/MM/yyyy") + "<br />"
                + "<b>Realizado por</b>  : " + empleado.Nombre + "<br /></p>"
                + "<h3>Detalles</h3><hr />"
                + "<table><thead><tr><th>EmpeñoId</th><th>Identificación</th><th>Nombre</th><th>Descripción</th><th>Estado</th><th>Prorroga</th><th>Retirado</th><th>SubTotal</th></tr></thead><tbody>";
            foreach (DataGridViewRow item in detalle.Rows)
            {
                str += "<tr><td>" + item.Cells[0].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[2].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[3].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[1].Value.ToString() + "</td>"
                    + "<td>" + item.Cells[4].Value.ToString() + "</td>";

                if ((bool)item.Cells[8].Value==true)
                {
                    str += "<td>X</td>";                    
                }
                else
                {
                    str += "<td></td>";
                }
                if ((bool)item.Cells[9].Value == true)
                {
                    str += "<td>X</td>";
                }
                else
                {
                    str += "<td></td>";
                }
                str += "<td>" + item.Cells[11].Value.ToString() + "</td></tr>";                                               
            }
            str += "</tbody></table><hr />"
                + totalesStr
                + "<br /><br />Saludos.";

            await SendMail(to, subject, str);
        }

        public async Task SendMail(string to, string subject, string body)
        {
            try
            {
                if (_context.Configuraciones.Count()>0)
                {
                    var configuracion = _context.Configuraciones.FirstOrDefault();

                    if (!string.IsNullOrEmpty(configuracion.Email) && !string.IsNullOrEmpty(configuracion.Password) && !string.IsNullOrEmpty(configuracion.SMTP))
                    {
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(to));
                        message.From = new MailAddress(configuracion.Email);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = configuracion.Email,
                                Password = configuracion.Password
                            };

                            smtp.Credentials = credential;
                            smtp.Host =configuracion.SMTP;
                            smtp.Port = configuracion.Puerto;
                            smtp.EnableSsl = configuracion.SSL;
                            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //smtp.UseDefaultCredentials = false;
                            await smtp.SendMailAsync(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void SendMailNoAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress("donovanjarquin1@outlook.com");
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "donovanjarquin1@outlook.com",
                        Password = "Shenlong'123"
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = int.Parse("587");
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendMail(List<string> mails, string subject, string body)
        {
            var message = new MailMessage();

            foreach (var to in mails)
            {
                message.To.Add(new MailAddress(to));
            }

            message.From = new MailAddress("donovanjarquin1@outlook.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "donovanjarquin1@outlook.com",
                    Password = "Shenlong'123"
                };

                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = int.Parse("587");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}
