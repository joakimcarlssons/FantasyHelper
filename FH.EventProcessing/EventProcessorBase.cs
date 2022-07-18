using FH.EventProcessing.Dtos;
using System.Text.Json;


namespace FH.EventProcessing
{
    public abstract class EventProcessorBase : IEventProcessor
    {
        public virtual EventType DetermineEvent(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
            switch (eventType.Event)
            {
                case "Teams_Published": return EventType.TeamsPublished;
                case "Players_Published": return EventType.PlayersPublished;
                default: return EventType.Undetermined;
            }
        }

        public abstract Task ProcessEvent(string message);
    }
}
