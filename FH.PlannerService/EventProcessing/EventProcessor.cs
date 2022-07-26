namespace FH.PlannerService.EventProcessing
{
    public class EventProcessor : EventProcessorBase
    {
        #region Private Members

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        #endregion

        public override Task ProcessEvent(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<IRepository>();

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.TeamsPublished:
                    {
                        // Extract message
                        var teamsPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Team>>>(message);
                        if (teamsPublishedDto == null || teamsPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Team>>));

                        foreach (var team in teamsPublishedDto.Data)
                        {
                            team.FantasyId = (int)teamsPublishedDto.Source;
                            repo.SaveTeam(team);
                        }

                        repo.SaveChanges();
                        Console.WriteLine("FPL Teams: " + repo.GetAllTeamsByFantasyId(1).Count());
                        Console.WriteLine("Allsvenskan Teams: " + repo.GetAllTeamsByFantasyId(2).Count());
                        break;
                    }
                case EventType.PlayersPublished:
                    {
                        // Extract message
                        var playersPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Player>>>(message);
                        if (playersPublishedDto == null || playersPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Player>>));

                        break;
                    }
                case EventType.FixturesPublished:
                    {
                        // Extract message
                        var fixturesPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Fixture>>>(message);
                        if (fixturesPublishedDto == null || fixturesPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Fixture>>));

                        break;
                    }
            }

            return Task.CompletedTask;
        }
    }
}
