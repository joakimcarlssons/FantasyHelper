namespace FH.PlannerService.EventProcessing
{
    public class EventProcessor : EventProcessorBase
    {
        #region Private Members

        private readonly IServiceScopeFactory _scopeFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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
                        var teamsPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Team>>>(message);
                        if (teamsPublishedDto == null || teamsPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Team>>));

                        Console.WriteLine("--> Saving teams to db...");

                        // Assign fantasy id and save data
                        foreach (var team in teamsPublishedDto.Data)
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
                        var playersPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Player>>>(message);
                        if (playersPublishedDto == null || playersPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Player>>));

                        Console.WriteLine("--> Saving players to db...");

                        // Assign fantasy id and save data
                        foreach (var player in playersPublishedDto.Data)
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
                        var fixturesPublishedDto = JsonSerializer.Deserialize<DataPublishedDto<IEnumerable<Fixture>>>(message);
                        if (fixturesPublishedDto == null || fixturesPublishedDto.Data == null) throw new NullReferenceException(nameof(DataPublishedDto<IEnumerable<Fixture>>));

                        Console.WriteLine("--> Saving fixtures to db...");

                        // Assign fantasy id and save data
                        foreach (var fixture in fixturesPublishedDto.Data)
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
