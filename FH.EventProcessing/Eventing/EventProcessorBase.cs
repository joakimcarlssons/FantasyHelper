using FH.EventProcessing.Dtos;
using RabbitMQ.Client;
using System.Text.Json;


namespace FH.EventProcessing
{
    public abstract class EventProcessorBase : IEventProcessor
    {
        public virtual EventType DetermineEvent(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
            return eventType.Event switch
            {
                "Data_Load_Request" => EventType.DataLoadRequest,
                "Teams_Published" => EventType.TeamsPublished,
                "Players_Published" => EventType.PlayersPublished,
                "Fixtures_Published" => EventType.FixturesPublished,
                _ => EventType.Undetermined,
            };
        }

        public abstract Task ProcessEvent(string message);
    }
}
