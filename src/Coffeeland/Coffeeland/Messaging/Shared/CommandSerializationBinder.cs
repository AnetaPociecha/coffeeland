using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Coffeeland.Messaging.Shared
{
    public class CommandSerializationBinder : ISerializationBinder
    {
        static List<Type> commandTypes { get; set; }

        public CommandSerializationBinder()
        {
            if (commandTypes == null)
            {
                commandTypes = new List<Type>();
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                foreach (Type type in myAssembly.GetTypes())
                {
                    if (typeof(ICommand).IsAssignableFrom(type))
                        commandTypes.Add(type);
                }
            }
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return commandTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}