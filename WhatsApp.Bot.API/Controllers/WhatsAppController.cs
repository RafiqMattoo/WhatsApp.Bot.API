using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace WhatsApp.Bot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Hello How are you doing");
            return TwiML(messagingResponse);
        }
        [HttpGet]
        public string Get()
        {
            return "Welcome to API's";
        }
    }
}
