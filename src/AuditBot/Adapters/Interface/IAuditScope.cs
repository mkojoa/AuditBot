using System;
using System.Threading.Tasks;

namespace AuditBot.Adapters.Interface
{
    public interface IAuditScope : IAsyncDisposable
    {
        object EventId { get; }
        string EventType { get; set; }
        void Comment(string text);
        void Comment(string format, params object[] args);
        void Discard();
        void Save();
        Task SaveAsync();
        void SetCustomField<TC>(string fieldName, TC value, bool serialize = false);
        void SetTargetGetter(Func<object> targetGetter);
    }
}
