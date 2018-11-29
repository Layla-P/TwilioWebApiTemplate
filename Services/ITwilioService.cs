
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace WebApiTemplate.Services
{
    public interface ITwilioService
    {
        Task<string> SendMessage(MessageResource inboundMessage);
    }
}