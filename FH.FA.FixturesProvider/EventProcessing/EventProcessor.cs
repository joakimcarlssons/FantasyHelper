using FH.EventProcessing.Dtos;
using System.Text.Json;

namespace FH.FA.FixturesProvider.EventProcessing
{
    public class EventProcessor : EventProcessorBase
    {
        #region Private Members

        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        #endregion

        public override async Task ProcessEvent(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var dataLoader = scope.ServiceProvider.GetService<IDataLoader>();

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.TeamsPublished:
                    {
                        Console.WriteLine("--> TeamsPublished event detected!");

                        AddOrUpdateTeams(message);
                        await dataLoader.LoadFixtureData();
                        await dataLoader.LoadLeagueData();
                        dataLoader.UpdateFixtureDifficulties();

                        break;
                    }
                default:
                    Console.WriteLine("--> No expected event was found..");
                    break;
            }
        }

        #region Private Methods

        private void AddOrUpdateTeams(string message)
        {
            // Get repository
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<IRepository>();

            // Extract DTO
            var teamsPublishedDto = JsonSerializer.Deserialize<TeamsPublishedDto>(message);

            try
            {
                foreach (var team in teamsPublishedDto.Teams)
                {
                    repo.SaveTeam(_mapper.Map<Team>(team));
                    repo.SaveChanges();
                }

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
