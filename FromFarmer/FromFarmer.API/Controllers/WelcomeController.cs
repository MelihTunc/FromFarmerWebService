using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FromFarmer.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WelcomeController : BaseController
    {

        private readonly IConfiguration _config;

        public WelcomeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public string Get()
        {
            string welcomeMessage = $"Service Name : {_config.GetValue<string>("LinuxPublishScreen:Title")}\n" +
                                    $"============================\n" +
                                    $"Service Status: {_config.GetValue<string>("LinuxPublishScreen:ServiceStatus")}\n" +
                                    $"Last Update Date: {_config.GetValue<string>("LinuxPublishScreen:LastUpdateDate")}\n" +
                                    $"Current Update Version: {_config.GetValue<string>("LinuxPublishScreen:PublishVersiyon")}\n";

            return welcomeMessage;
        }
    }
}
