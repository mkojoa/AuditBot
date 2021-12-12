using AuditBot.Adapters.Interface;
using AuditBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AuditBot.Adapters.Service
{
    public class JsonAdapter : IJsonAdapter
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, AuditBotInit.JsonSettings);
        }
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, AuditBotInit.JsonSettings);
        }
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, AuditBotInit.JsonSettings);
        }

        public async Task SerializeAsync(Stream stream, object value)
        {
            var json = JsonConvert.SerializeObject(value, AuditBotInit.JsonSettings);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                await sw.WriteAsync(json);
            }
        }

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    var jObject = JObject.Load(jr);
                    return jObject.ToObject<T>(JsonSerializer.Create(AuditBotInit.JsonSettings));
                }
            }

        }

        public T ToObject<T>(object value)
        {
            if (value == null)
            {
                return default(T);
            }
            if (value is T || typeof(T).GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
            {
                return (T)value;
            }

            if (value is JContainer)
            {
                return (value as JContainer).ToObject<T>(JsonSerializer.Create(AuditBotInit.JsonSettings));
            }
            return default(T);
        }

    }
}
