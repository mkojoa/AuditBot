using AuditBot.Adapters.Interface;
using AuditBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuditBot.Adapters.Service
{
    public class JsonAdapter : IJsonAdapter
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, AuditBotOptions.JsonSettings);
        }
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, AuditBotOptions.JsonSettings);
        }
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, AuditBotOptions.JsonSettings);
        }

        public async Task SerializeAsync(Stream stream, object value)
        {
            var json = JsonConvert.SerializeObject(value, AuditBotOptions.JsonSettings);
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
                    return jObject.ToObject<T>(JsonSerializer.Create(AuditBotOptions.JsonSettings));
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
				return (value as JContainer).ToObject<T>(JsonSerializer.Create(AuditBotOptions.JsonSettings));
			}
            return default(T);
        }

    }
}
