using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace Coffeeland.Messaging.Shared
{
    public class QuerySerializationBinder : ISerializationBinder
    {
        static List<Type> queryTypes { get; set; }

        public QuerySerializationBinder()
        {
            if(queryTypes == null)
            {
                queryTypes = new List<Type>();
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                foreach (Type type in myAssembly.GetTypes())
                {
                    if (typeof(IQuery).IsAssignableFrom(type))
                        queryTypes.Add(type);
                }
            }
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return queryTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}