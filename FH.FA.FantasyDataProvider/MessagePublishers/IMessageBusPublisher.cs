namespace FH.FA.FantasyDataProvider.MessagePublishers
{
    public interface IMessageBusPublisher
    {
        void SendMessage(string message, string exchange, string routingKey);
        void PublishTeams(TeamsPublishedDto teamsPublishedDto);
    }
}
