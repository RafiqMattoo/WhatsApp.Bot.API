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
            
            bool isFirstMessage =Convert.ToBoolean(HttpContext.Session.GetString("isFirstMessage")); 
            string FieldPosition = HttpContext.Session.GetString("FieldPosition");
            int botOption = Convert.ToInt16(HttpContext.Session.GetString("FieldPosition"));
            if (HttpContext.Session == null)
            {
                  isFirstMessage = true;
                  FieldPosition = "";
                  botOption = 0;
            }
            else
            {
                if (incomingMessage.Body.Length > 0)
                {
                    if (isFirstMessage && botOption == 0)
                    {
                        messagingResponse.Message  (
                        "Welcome to Emergency Ambulance System." + Environment.NewLine +
                        "Choose an option to proceed further." + Environment.NewLine +
                        "Type 1 for New Ambulance Request." + Environment.NewLine +
                        "Type 2 for status update on a running case." + Environment.NewLine +
                        "Type 3 for to get contact details of Emergency Service" + Environment.NewLine);
                        
                        isFirstMessage = false;

                    }
                    switch (incomingMessage.Body.ToString().ToLower())
                    {
                        case "1":
                            botOption = 1;
                            break;

                        case "2":
                            botOption = 2;
                            break;

                        case "3":
                            botOption = 3;
                            break;
                    }

                    if (botOption == 1)
                    {

                        if (isFirstMessage == false && FieldPosition.ToString().ToLower().Equals(""))
                        {
                            messagingResponse.Message("Please Enter your Name ");                          
                            isFirstMessage = false;
                            FieldPosition = "name";
                        }
                        else if (FieldPosition.ToString().ToLower().Equals("name") && isFirstMessage == false)
                        {
                            messagingResponse.Message("Please Enter your Address with Nearest Land Mark");
                            isFirstMessage = false;
                            FieldPosition = "address";
                        }

                        else if (FieldPosition.ToString().ToLower().Equals("address") && isFirstMessage == false)
                        {
                            messagingResponse.Message("Please Enter your Contact Number");
                            isFirstMessage = false;
                            FieldPosition = "contact";
                        }

                        else if (FieldPosition.ToString().ToLower().Equals("contact") && isFirstMessage == false)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next();
                            messagingResponse.Message(
                             "Your request for new ambulance has been recorded." + Environment.NewLine +
                            "Your reference number is "+ num.ToString() + ".Command Center will contact you shortly." + Environment.NewLine +
                            "Thank you for your patience." + Environment.NewLine +
                            "Regards," + Environment.NewLine +
                            "Command Center Team - Emergency Ambulance System" + Environment.NewLine);
                            isFirstMessage = true;
                            FieldPosition = "";
                            botOption = 0;
                        }
                    }

                    else if (botOption == 2)
                    {

                        if (isFirstMessage == false && FieldPosition.ToString().ToLower().Equals(""))
                        {
                            messagingResponse.Message("Enter case number about which you want to enquire");
                            isFirstMessage = false;
                            FieldPosition = "case#";
                        }


                        else if (FieldPosition.ToString().ToLower().Equals("case#") && isFirstMessage == false)
                        {
                            messagingResponse.Message(
                            "Ambulance is 2 Km away from your location. Estimated time will be 5-10 minutes" + Environment.NewLine +
                            "Thank you for your patience." + Environment.NewLine +
                            "Regards," + Environment.NewLine +
                            "Command Center Team - Emergency Ambulance System" + Environment.NewLine);
                            isFirstMessage = true;
                            FieldPosition = "";
                            botOption = 0;
                        }
                    }

                    else if (botOption == 3)
                    {


                        messagingResponse.Message(
                         "Dial 112 from your mobile or landline anytime." + Environment.NewLine +
                        "Thank you for your patience." + Environment.NewLine +
                        "Regards," + Environment.NewLine +
                        "Command Center Team - Emergency Ambulance System" + Environment.NewLine);
                        isFirstMessage = true;
                        FieldPosition = "";
                        botOption = 0;

                    }
                }
                else
                {
                    messagingResponse.Message("Invalid Option Entered");
                }

                //if (incomingMessage.Body.ToString().ToLower().Contains("hi"))
                //{
                //    messagingResponse.Message("Welcome to Emergency Ambulance System." + Environment.NewLine +
                //                    "Choose an option to proceed further." + Environment.NewLine +
                //                    "Type 1 for New Ambulance Request." + Environment.NewLine +
                //                    "Type 2 for status update on a running case." + Environment.NewLine +
                //                    "Type 3 for to get contact details of Emergency Service" + Environment.NewLine);
                //}
                //if (incomingMessage.Body.ToString().ToLower().Contains("1"))
                //{
                //    messagingResponse.Message("Kindly Enter ");
                //}
                //if (incomingMessage.Body.ToString().ToLower().Contains("doing well"))
                //{
                //    messagingResponse.Message("Ok thats great to hear, how can i help you");
                //}
                //if (incomingMessage.Body.ToString().ToLower().Contains("order"))
                //{
                //    messagingResponse.Message("Your order is on its way, you will recieve it in a day or two.");
                //}
            }
            return TwiML(messagingResponse);
        }
        [HttpGet]
        public string Get()
        {
            return "Welcome to API's";
        }
    }
}
