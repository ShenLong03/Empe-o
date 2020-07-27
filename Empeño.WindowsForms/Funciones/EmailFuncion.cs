using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.Funciones
{
    public class EmailFuncion
    {
        DataContext _context = new DataContext();
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
