using AuditBot.Adapters.Interface;
using System;

namespace AuditBot.Adapters.Service
{
    /// <summary>
    /// The default system clock implementation using DateTime.UtcNow
    /// </summary>
    public class DefaultSystemClock : ISystemClock
    {
        public virtual DateTime UtcNow => DateTime.UtcNow;
    }
}
