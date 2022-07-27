using FH.EventProcessing.Dtos;
using System.Text.Json;

namespace FH.FA.FixturesProvider.EventProcessing
{
    public class EventProcessor : EventProcessorBase
    {
        #region Private Members

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMessageBusPublisher _messagePublisher;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventProcessor(IServiceScopeFactory scopeFactory, IMessageBusPublisher messagePublisher, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _messagePublisher = messagePublisher;
            _mapper = mapper;
        }

        #endregion

        public override async Task ProcessEvent(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var dataLoader = scope.ServiceProvider.GetService<IDataLoader>();
            var repo = scope.ServiceProvider.GetService<IRepository>();

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.DataLoadRequest:
                    {
                        // Extract message
                        var dataLoadingRequestDto = JsonSerializer.Deserialize<DataPublishedDto<DataLoadingRequestDto>>(message);

                        Console.WriteLine($"--> Data_Load_Request event detected from { dataLoadingRequestDto.Data.QueueName }");

                        if (dataLoadingRequestDto.Source != EventSource.FantasyAllsvenskan) break;
                        if (dataLoadingRequestDto.Data.EventType == EventType.FixturesPublished)
                        {
                            // If there is a request for fixtures
                            await PublishFixtures(repo, dataLoader);
                        }
                        else
                        {
                            Console.WriteLine("--> No data load requested from service..");
                        }

                        break;
                    }
                case EventType.TeamsPublished:
                    {
                        Console.WriteLine("--> Teams_Published event detected!");

                        // Extract message
                        var teamsPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Team>>>(message);
                        if (teamsPublishedDto.Source != EventSource.FantasyAllsvenskan) break;

                        // Handle data from message
                        AddOrUpdateTeams(teamsPublishedDto.Data, repo);

                        // Publish event with handled data
                        await PublishFixtures(repo, dataLoader);

                        break;
                    }
                default:
                    Console.WriteLine("--> No expected event was found..");
                    break;
            }
        }

        #region Private Methods

        private async Task PublishFixtures(IRepository repo, IDataLoader dataLoader)
        {
            // Run preparations
            await dataLoader.LoadFixtureData();
            await dataLoader.LoadLeagueData();
            dataLoader.UpdateFixtureDifficulties();

            // Publish fixtures
            var fixtures = repo.GetAllFixtures();
            _messagePublisher.PublishData(_mapper.Map<IEnumerable<FixtureReadDto>>(fixtures).ToPublishDataDto(EventType.FixturesPublished, EventSource.FantasyAllsvenskan));
        }

        private void AddOrUpdateTeams(IEnumerable<Team> teams, IRepository repo)
        {
            try
            {
                foreach (var team in teams)
                {
                    repo.SaveTeam(team);
                }

                repo.SaveChanges();
                Console.WriteLine("--> Provided teams was added to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add or update provided team to database: { ex.Message }");
            }
        }

        #endregion
    }
}
