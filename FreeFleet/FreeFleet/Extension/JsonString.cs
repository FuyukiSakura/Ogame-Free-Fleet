using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FreeFleet.Extension
{
    public static class JsonString
    {
        /// <summary>
        /// Deserialize JSON string using camel case naming strategy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
        }

        /// <summary>
        /// Serialize object into JSON string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.None
            });
        }
    }
}
