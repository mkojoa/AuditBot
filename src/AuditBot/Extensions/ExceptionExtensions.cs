using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditBot.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Returns a string representation of the given exception.
        /// </summary>
        /// <param name="exception">The exception</param>
        public static string GetExceptionInfo(this Exception exception)
        {
            if (exception == null)
            {
                return null;
            }
            // Using ex.ToString since, by default, it contains the stacktrace and inner exceptions.
            return exception.ToString();
        }
    }
}
