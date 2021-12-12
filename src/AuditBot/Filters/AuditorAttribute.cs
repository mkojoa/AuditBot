using AuditBot.Adapters.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace AuditBot.Filters
{
    public class AuditorAttribute : ActionFilterAttribute
    {
        private AuditBotAdapter _adapter = new();

        public bool IncludeResponseHeaders { get; set; }
        public bool IncludeHeaders { get; set; }
        public bool IncludeModelState { get; set; }
        public bool IncludeResponseBody { get; set; }

        public HttpStatusCode[] IncludeResponseBodyFor { get; set; }
        public HttpStatusCode[] ExcludeResponseBodyFor { get; set; }

        public bool IncludeRequestBody { get; set; }
        public string EventTypeName { get; set; }
        public bool SerializeActionParameters { get; set; }

        public AuditorAttribute()
        {
            this.Order = int.MinValue;
        }
    }
}
