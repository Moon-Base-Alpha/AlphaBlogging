using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace AlphaBlogging.Services
{
    public class SMSServices : ISMSServices
    {
        public void SendSMS(string smsMess)
        {
            // Fetch ID data from appsettings.json
            string accountSid = "ACa204ed90fd4ca0b52b6dedcebba2dc1c"; //ConfigurationManager.AppSettings["TwilioAccountSID"];
            string authToken = "9967649a88796ae78ac29c10b668ab49"; //ConfigurationManager.AppSettings["TwilioAuthToken"];

            // Initilize the Twilio Client with odentification parameters
            TwilioClient.Init(accountSid, authToken);

            // Setting up the Free Twilio from phone number and the only to phone number we can use freely at no cost
            var from = new PhoneNumber("+16075249476");
            var to = new PhoneNumber("+46733968322");

            // Create the message
            var message = MessageResource.Create( 
                to: to,
                from: from,
                body: smsMess);

            Console.WriteLine(message.Sid);
        }
    }
}
