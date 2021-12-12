using System;

namespace AuditBot.Adapters.Interface
{
    /// <summary>
    /// Abstracts the system clock.
    /// </summary>
    public interface ISystemClock
    {
        /// <summary>
        /// Retrieves the current system time in UTC.
        /// </summary>
        DateTime UtcNow
        {
            get;
        }
    }
}
