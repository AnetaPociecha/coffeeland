using Microsoft.AspNet.SignalR;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.SignalRCommunication
{
    public class CommunicationHub : Hub
    {
        public dynamic SendQuery(string json)
        {
           try
            {
                dynamic query = QueryResolver.ConvertToQuery(json);
                dynamic handler = QueryResolver.ResolveQueryHandler(query);
                
                return handler.Handle(query);
            }
            catch
            {
                return new SuccessInfoDto()
                {
                    isSuccess = false
                };
            }
        }

        public dynamic SendCommand(string json)
        {
            try
            {
                dynamic command = CommandResolver.ConvertToCommand(json);
                dynamic handler = CommandResolver.ResolveCommandHandler(command);
                
                return handler.Handle(command);
            }
            catch
            {
                return new SuccessInfoDto()
                {
                    isSuccess = false
                };
            }
        }
    }
}