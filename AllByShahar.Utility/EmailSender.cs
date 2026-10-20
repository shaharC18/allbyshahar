using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace AllByShahar.Utility
{
    public class EmailSender : IEmailSender
    {
        // פרטי ההתחברות לשרת המייל נקראים מההגדרות (משתני סביבה / User Secrets) ולא כתובים בקוד
        private readonly string? _username;
        private readonly string? _password;

        public EmailSender(IConfiguration configuration)
        {
            _username = configuration["Email:Username"];
            _password = configuration["Email:Password"];
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // בלי פרטי התחברות לא שולחים מייל
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                return Task.CompletedTask;
            }
            try
            {
				var emailToSend = new MimeMessage();
				emailToSend.From.Add(MailboxAddress.Parse(_username));
				emailToSend.To.Add(MailboxAddress.Parse(email));
				emailToSend.Subject = subject;
				emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


				//send email
				using (var emailClient = new SmtpClient())
				{
					emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
					emailClient.Authenticate(_username, _password);
					emailClient.Send(emailToSend);
					emailClient.Disconnect(true);
				}
				return Task.CompletedTask;
			}
            catch (Exception)
            {
				return Task.CompletedTask;
			}
        }
    }
}
