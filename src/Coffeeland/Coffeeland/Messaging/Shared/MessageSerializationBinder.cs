using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace Coffeeland.Messaging.Shared
{
    public class MessageSerializationBinder : ISerializationBinder
    {
        static List<Type> messageTypes { get; set; }

        public MessageSerializationBinder()
        {
            if (messageTypes == null)
            {
                messageTypes = new List<Type>();
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                foreach (Type type in myAssembly.GetTypes())
                {
                    if (typeof(IMessage).IsAssignableFrom(type))
                        messageTypes.Add(type);
                }
            }
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return messageTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}