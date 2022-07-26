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
            if (repo == null) throw new NullReferenceException(nameof(IRepository));

            Console.WriteLine("--> Determining incoming event...");
            var eventType = base.DetermineEvent(message);
            switch (eventType)
            {
                case EventType.TeamsPublished:
                    {
                        // Extract message
                        var teamsPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<TeamPublishedDto>>>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (teamsPublishedDto == null || teamsPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<TeamPublishedDto>>));

                        Console.WriteLine("--> Saving teams to db...");
                        
                        // Assign fantasy id and save data
                        foreach (var team in _mapper.Map<IEnumerable<Team>>(teamsPublishedDto.Data))
                        {
                            team.FantasyId = (int)teamsPublishedDto.Source;
                            repo.SaveTeam(team);
                        }

                        repo.SaveChanges();

                        Console.WriteLine("--> All teams were saved!");
                        break;
                    }
                case EventType.PlayersPublished:
                    {
                        // Extract message
                        var playersPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<PlayerPublishedDto>>>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (playersPublishedDto == null || playersPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<PlayerPublishedDto>>));

                        Console.WriteLine("--> Saving players to db...");

                        // Assign fantasy id and save data
                        foreach (var player in _mapper.Map<IEnumerable<Player>>(playersPublishedDto.Data))
                        {
                            player.FantasyId = (int)playersPublishedDto.Source;
                            repo.SavePlayer(player);
                        }

                        repo.SaveChanges();

                        Console.WriteLine("--> All players were saved!");
                        break;
                    }
                case EventType.FixturesPublished:
                    {
                        // Extract message
                        var fixturesPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<FixturePublishedDto>>>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (fixturesPublishedDto == null || fixturesPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<FixturePublishedDto>>));

                        Console.WriteLine("--> Saving fixtures to db...");

                        // Assign fantasy id and save data
                        foreach (var fixture in _mapper.Map<IEnumerable<Fixture>>(fixturesPublishedDto.Data))
                        {
                            fixture.FantasyId = (int)fixturesPublishedDto.Source;
                            repo.SaveFixture(fixture);
                        }

                        repo.SaveChanges();

                        Console.WriteLine("--> All fixtures were saved!");
                        break;
                    }
            }

            return Task.CompletedTask;
        }
    }
}
