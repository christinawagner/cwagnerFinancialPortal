using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

namespace cwagnerFinancialPortal.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var GmailUsername = WebConfigurationManager.AppSettings["username"];
            var GmailPassword = WebConfigurationManager.AppSettings["password"];
            var host = WebConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);

            using (var smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(GmailUsername, GmailPassword)
            })
            using (var email = new MailMessage("FinancialPortal<FinancialPortalDONOTREPLY@email.com>",
                   message.Destination)
            {
                Subject = message.Subject,
                IsBodyHtml = true,
                Body = message.Body
            })
            {
                try
                {
                    await smtp.SendMailAsync(email);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
            };
        }


        private SmtpClient GetClient()
        {
            var GmailUsername = WebConfigurationManager.AppSettings["username"];
            var GmailPassword = WebConfigurationManager.AppSettings["password"];
            var host = WebConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);

            return new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(GmailUsername, GmailPassword)
            };
        }

        public async Task SendAsync(MailMessage message)
        {
            using (var smtp = GetClient())
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
            };
        }

        public void Send(MailMessage message)
        {
            using (var smtp = GetClient())
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            };
        }
    }
}