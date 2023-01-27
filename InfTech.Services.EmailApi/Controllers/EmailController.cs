using InfTech.Services.EmailApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfTech.Services.EmailApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISmtpClient _smtpClient;
        public EmailController(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            _smtpClient.Send(new List<string>() { "glodovskii@gmail.com" }, "Test", "Test");
            return Ok();
        }
    }
}
