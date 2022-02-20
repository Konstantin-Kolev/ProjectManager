using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProjectManagerWeb.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string jsonData = session.GetString(key);

            return string.IsNullOrEmpty(jsonData)
                        ? default(T)
                        : JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
