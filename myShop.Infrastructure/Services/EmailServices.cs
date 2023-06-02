using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using myShop.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IOptions<SMTPSettings> _SMTPSettings;

        public EmailServices(IOptions<SMTPSettings> SMTPSettings)
        {
            _SMTPSettings = SMTPSettings;
        }
        public async Task sendEmailAsync(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to, subject, body);

            using (var emailClient = new SmtpClient(_SMTPSettings.Value.Host, _SMTPSettings.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                    _SMTPSettings.Value.user,
                    _SMTPSettings.Value.Password
                    );

                await emailClient.SendMailAsync(message);
            }
        }
    }
}
