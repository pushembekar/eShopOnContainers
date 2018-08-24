using EventBus.Events;
using Newtonsoft.Json;
using System;

namespace IntegrationEventLogEF
{
    public class IntegrationEventLogEntry
    {
        public Guid EventId { get; set; }
        public string EventTypeName { get; set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }

        private IntegrationEventLogEntry() { }

        public IntegrationEventLogEntry(IntegrationEvent @event)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.NotPusblished;
            TimesSent = 0;
        }
    }
}
