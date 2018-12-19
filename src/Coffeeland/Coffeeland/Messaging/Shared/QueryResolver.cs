using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Coffeeland.Messaging.Shared
{
    public static class QueryResolver
    {
        private static JsonSerializerSettings _querySerializerSettings;

        static QueryResolver()
        {
            _querySerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                SerializationBinder = new QuerySerializationBinder()
            };
        }

        public static IQuery ConvertToQuery(string json)
        {
            return JsonConvert.DeserializeObject<IQuery>(json, _querySerializerSettings);
        }

        public static dynamic ResolveQueryHandler(IQuery query)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (typeof(IQueryHandler<>).MakeGenericType(query.GetType()).IsAssignableFrom(type))
                    return Activator.CreateInstance(type);
            }
            throw new NotImplementedException();
        }
    }
}