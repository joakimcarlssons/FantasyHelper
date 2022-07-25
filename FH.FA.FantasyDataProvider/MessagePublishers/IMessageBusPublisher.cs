namespace FH.FA.FantasyDataProvider.MessagePublishers
{
    public interface IMessageBusPublisher
    {
        void SendMessage(string message, string exchange, string routingKey, string queueName);
        void PublishData<T>(DataPublishedDto<T> dataPublishedDto);
    }
}
