using LearnSendMail.Helper;
using LearnSendMail.Service;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnSendMail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailingController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailingController(IEmailService emailService, IWebHostEnvironment webHostEnvironment)
        {
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "mubeenworking@gmail.com";
                mailRequest.Subject = "Learning Email Sending Course";
                string html = System.IO.File.ReadAllText(_webHostEnvironment.WebRootPath + "\\EmailTemplates\\Welcome.cshtml");
                mailRequest.Body = html;
                mailRequest.Attachment = _webHostEnvironment.WebRootPath + "\\Attachments\\API Full Course.pptx";
                await _emailService.SendEmailAsync(mailRequest);
                return Ok("Email sent successfully..");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
