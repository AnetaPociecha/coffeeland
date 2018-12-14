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
        private static JsonSerializerSettings _querySerializerSettings;
        private static JsonSerializerSettings _commandSerializerSettings;

        public CommunicationHub()
        {

            if(_querySerializerSettings == null)
                _querySerializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    SerializationBinder = new QuerySerializationBinder()
                };

            if (_commandSerializerSettings == null)
                _commandSerializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    SerializationBinder = new CommandSerializationBinder()
                };
        }

        public dynamic SendQuery(string json)
        {
           
            dynamic query = ConvertToQuery(json);
            dynamic handler = GetQueryHandler(query);

            try
            {
                return handler.Handle(query);
            }
            catch
            {
                return new SuccessDto()
                {
                    isSuccess = false
                };
            }
        }

        public dynamic SendCommand(string json)
        {

            dynamic command = ConvertToCommand(json);
            dynamic handler = GetCommandHandler(command);

            try
            {
                return handler.Handle(command);
            }
            catch
            {
                return new SuccessDto()
                {
                    isSuccess = false
                };
            }
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

        private ICommand ConvertToCommand(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<ICommand>(json, _commandSerializerSettings);
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

        private dynamic GetCommandHandler(ICommand command)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (typeof(ICommandHandler<>).MakeGenericType(command.GetType()).IsAssignableFrom(type))
                    return Activator.CreateInstance(type);
            }
            throw new NotImplementedException();
        }
    }
}