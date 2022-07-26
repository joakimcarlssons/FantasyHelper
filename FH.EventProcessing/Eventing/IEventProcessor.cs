using FH.EventProcessing.Dtos;
using RabbitMQ.Client;
using System.Text.Json;

namespace FH.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
        EventType DetermineEvent(string message);
    }
}
