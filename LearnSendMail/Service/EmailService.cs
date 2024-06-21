using LearnSendMail.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearnSendMail.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Email);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;
            var builder = new BodyBuilder();
            if (!string.IsNullOrEmpty(request.Attachment))
            {
                byte[] bytes; 
                var fileStream = new FileStream(request.Attachment, FileMode.Open, FileAccess.Read);
                using(var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                builder.Attachments.Add("Attachment.pdf", bytes, ContentType.Parse("application/octet-stream"));
            }
            builder.HtmlBody = request.Body;
            email.Body= builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email,_mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
