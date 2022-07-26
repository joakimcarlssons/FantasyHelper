using FH.EventProcessing.Dtos;

namespace FH.EventProcessing
{
    public interface IMessageBusPublisher
    {
        void SendMessage(string message, string exchange, string routingKey, string queueName);
        void PublishData<T>(DataPublishedDto<T> data, string routingKey = "", string queueName = null);
        void SetupQueue(string queueName, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments, string exchange, string routingKey);
    }
}
