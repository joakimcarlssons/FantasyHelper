using FH.EventProcessing.Dtos;

namespace FH.EventProcessing
{
    public interface IMessageBusPublisher
    {
        void SendMessage(string message, string exchange, string routingKey, string queueName);
        void PublishData<T>(DataPublishedDto<T> dataPublishedDto);
    }
}
