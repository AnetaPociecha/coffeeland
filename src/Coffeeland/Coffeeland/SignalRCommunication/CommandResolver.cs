using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Coffeeland.Messaging.Shared
{
    public class CommandResolver
    {
        private static JsonSerializerSettings _commandSerializerSettings;

        static CommandResolver()
        {
            _commandSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                SerializationBinder = new CommandSerializationBinder()
            };
        }

        public static ICommand ConvertToCommand(string json)
        {
            return JsonConvert.DeserializeObject<ICommand>(json, _commandSerializerSettings);
        }

        public static dynamic ResolveCommandHandler(ICommand command)
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