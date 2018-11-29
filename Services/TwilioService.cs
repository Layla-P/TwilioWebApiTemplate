using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WebApiTemplate.Models;
using Twilio.Exceptions;
using Microsoft.Extensions.Logging;

namespace WebApiTemplate.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly ILogger _logger;
        private readonly TwilioAccount _twilioAccount;
        public TwilioService(IOptions<TwilioAccount> options,
                                ILogger logger)
        {
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }
            _twilioAccount = options.Value;

            TwilioClient.Init(
                _twilioAccount.AccountSid,
                _twilioAccount.AuthToken
            );
        }

        public async Task<string> SendMessage(MessageResource inboundMessage)
        {
            try
            {
                var outboundMessage = await MessageResource
                                                .CreateAsync(
                                                    to: inboundMessage.From,
                                                    from: inboundMessage.To,
                                                    body: $"This was what was received: {inboundMessage.Body}");
                
                return outboundMessage.Sid;
            }
            catch(ApiConnectionException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}