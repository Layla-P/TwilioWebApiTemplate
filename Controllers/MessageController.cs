using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twilio.Rest.Api.V2010.Account;
using WebApiTemplate.Models;
using WebApiTemplate.Services;

namespace WebApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITwilioService _twilioService;
        public MessageController(ILogger logger,
                                    ITwilioService twilioService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _twilioService = twilioService ?? throw new ArgumentNullException(nameof(twilioService));
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            // this GET method will alow you to check that your application is running correctly 
            // feel free to delete it or repurpose it.
            return Content("Message Controller is online!");
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageResource message)
        {
            var messageSid = await _twilioService.SendMessage(message);            
            _logger.LogInformation($"Message Sid: {messageSid} sent.");
            // _twilioService.SendMessage will create the response and send it, however
            // the Twilio webhook will still expect a response, so we return an empty 
            // <Response></Response> 
            return Content("<Response></Response>", "text/xml");
        }
    }
}
