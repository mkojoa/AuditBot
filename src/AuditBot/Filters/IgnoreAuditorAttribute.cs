using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditBot.Filters
{
    /// <summary>
    /// Ignore controllers, action methods or method parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = true, AllowMultiple = false)]
    public sealed class IgnoreAuditorAttribute : Attribute
    {
    }
}
