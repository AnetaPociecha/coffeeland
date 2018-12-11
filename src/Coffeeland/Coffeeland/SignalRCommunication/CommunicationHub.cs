using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using Coffeeland.Database;
using Coffeeland.Messaging.Shared;
using Coffeeland.Messaging.Queries.Queries;
using System.Reflection;
using Coffeeland.Messaging.Dtos;

namespace Coffeeland.SignalRCommunication
{
    public class CommunicationHub : Hub
    {
        private static DatabaseQueryProcessor _processor;
        private static JsonSerializerSettings _querySerializerSettings;
        private static JsonSerializerSettings _messageSerializerSettings;

        public CommunicationHub()
        {
            if(_processor == null)
                _processor = new DatabaseQueryProcessor();

            if(_querySerializerSettings == null)
                _querySerializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    SerializationBinder = new QuerySerializationBinder()
                };

            if (_messageSerializerSettings == null)
                _messageSerializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    SerializationBinder = new MessageSerializationBinder()
                };
        }

        public dynamic SendQuery(string json)
        {
           
            dynamic query = ConvertToQuery(json);
            dynamic handler = GetQueryHandler(query);

            return handler.Handle(query);
        }

        public dynamic SendMessage(string json)
        {

            dynamic message = ConvertToQuery(json);
            dynamic handler = GetQueryHandler(message);

            return handler.Handle(message);
        }

        private IQuery ConvertToQuery(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<IQuery>(json, _querySerializerSettings);
            }
            catch
            {
                throw new NotImplementedException();    // implement some mechanism for errors
            }
        }

        private IMessage ConvertToMessage(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<IMessage>(json, _messageSerializerSettings);
            }
            catch
            {
                throw new NotImplementedException();    // implement some mechanism for errors
            }
        }

        private dynamic GetQueryHandler(IQuery query)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (typeof(IQueryHandler<>).MakeGenericType(query.GetType()).IsAssignableFrom(type))
                    return Activator.CreateInstance(type);
            }
            throw new NotImplementedException();
        }

        private dynamic GetMessageHandler(IMessage message)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (typeof(IMessageHandler<>).MakeGenericType(message.GetType()).IsAssignableFrom(type))
                    return Activator.CreateInstance(type);
            }
            throw new NotImplementedException();
        }
    }
}