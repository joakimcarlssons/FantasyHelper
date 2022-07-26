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

        public override async Task<string> ProcessEvent(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var dataLoader = scope.ServiceProvider.GetService<IDataLoader>();
            var repo = scope.ServiceProvider.GetService<IRepository>();

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.TeamsPublished:
                    {
                        Console.WriteLine("--> Teams_Published event detected!");

                        // Extract message
                        var teamsPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Team>>>(message);

                        // Handle data from message
                        AddOrUpdateTeams(teamsPublishedDto.Data, repo);
                        await dataLoader.LoadFixtureData();
                        await dataLoader.LoadLeagueData();
                        dataLoader.UpdateFixtureDifficulties();

                        // Publish event with handled data
                        var fixtures = repo.GetAllFixtures();
                        _messagePublisher.PublishData(_mapper.Map<IEnumerable<FixtureReadDto>>(fixtures).ToPublishDataDto(EventType.FixturesPublished, EventSource.FantasyAllsvenskan));

                        return teamsPublishedDto.Event;
                    }
                default:
                    Console.WriteLine("--> No expected event was found..");
                    return null;
            }
        }

        #region Private Methods

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
