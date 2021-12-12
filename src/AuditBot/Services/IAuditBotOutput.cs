using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditBot.Services
{
    public interface IAuditBotOutput
    {
        /// <summary>
        /// Extension fields
        /// </summary>
        Dictionary<string, object> CustomFields { get; set; }
        /// <summary>
        /// Serialize to JSON string
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
