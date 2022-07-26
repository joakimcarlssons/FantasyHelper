namespace FH.FA.FantasyDataProvider.EventProcessing
{
    public class EventProcessor : EventProcessorBase
    {
        #region Private Members

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IDataLoader _dataLoader;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventProcessor(IServiceScopeFactory scopeFactory, IDataLoader dataLoader)
        {
            _scopeFactory = scopeFactory;
            _dataLoader = dataLoader;
        }

        public override Task ProcessEvent(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<IDataProviderRepository>();

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.DataLoadRequest:
                    {
                        // Extract message
                        var dataLoadingRequestDto = JsonSerializer.Deserialize<DataPublishedDto<DataLoadingRequestDto>>(message);
                        Console.WriteLine($"--> Data_Load_Request event detected from { dataLoadingRequestDto.Data.QueueName }");

                        switch (dataLoadingRequestDto.Data.EventType)
                        {
                            case EventType.TeamsPublished:
                                {
                                    _dataLoader.PublishTeams(repo);
                                    break;
                                }
                            case EventType.PlayersPublished:
                                {
                                    _dataLoader.PublishPlayers(repo);
                                    break;
                                }
                            default:
                                Console.WriteLine("--> No data load requested from service..");
                                break;
                        }

                        break;
                    }
                default:
                    Console.WriteLine("--> No expected event was found..");
                    break;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
