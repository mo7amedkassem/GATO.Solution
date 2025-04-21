using Gato.Core.Service_Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Service
{


    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string receptor, string subject, string body)
        {
            var email = _config.GetValue<string>("EMAIL_CONFIGRATION:EMAIL");
            var password = _config.GetValue<string>("EMAIL_CONFIGRATION:PASSWORD");
            var host = _config.GetValue<string>("EMAIL_CONFIGRATION:HOST");
            var port = _config.GetValue<int>("EMAIL_CONFIGRATION:PORT");

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8
            };

            mailMessage.To.Add(receptor);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
        }
    }


}
