namespace LearnSendMail.Helper
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
    }
}
