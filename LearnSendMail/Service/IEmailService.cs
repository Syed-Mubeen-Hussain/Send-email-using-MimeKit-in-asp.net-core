using LearnSendMail.Helper;

namespace LearnSendMail.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
