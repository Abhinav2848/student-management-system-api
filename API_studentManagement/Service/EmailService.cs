using API_studentManagement.Interface;
using API_studentManagement.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace API_studentManagement.Service
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            this._emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email);
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName,_emailSettings.Email));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject=mailRequest.Subject;


            var builer = new BodyBuilder();
            builer.HtmlBody= mailRequest.Body;
            email.Body=builer.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host,_emailSettings.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);


        }

    }
}
