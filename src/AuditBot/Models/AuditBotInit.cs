using AuditBot.Adapters.Interface;
using AuditBot.Adapters.Service;
using Newtonsoft.Json;

namespace AuditBot.Models
{
    public static class AuditBotInit
    {
        /// <summary>
        /// Gets or Sets the System Clock implementation. By default DateTime.UtcNow is used to get the current date and time.
        /// </summary>
        public static ISystemClock SystemClock { get; set; }

        /// <summary>
        /// Gets or sets the json adapter that controls the JSON serialization mechanism.
        /// </summary>
        public static IJsonAdapter JsonAdapter { get; set; } = new JsonAdapter();



        /// <summary>
        /// Global switch to disable audit logging. Default is false.
        /// </summary>
        public static bool AuditDisabled { get; set; }
        /// <summary>
        /// Global json serializer settings for serializing the audit event on the data providers or by calling the ToJson() method on the AuditEvent.
        /// </summary>
        public static JsonSerializerSettings JsonSettings { get; set; }


        static AuditBotInit()
        {
            AuditDisabled = false;

            JsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            SystemClock = new DefaultSystemClock();
        }


    }
}
