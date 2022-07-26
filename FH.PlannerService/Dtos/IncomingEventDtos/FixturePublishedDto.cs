namespace FH.PlannerService.Dtos.IncomingEventDtos
{
    public class FixturePublishedDto
    {
        public int FixtureId { get; set; }
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public FixturePublishedTeamDto HomeTeam { get; set; }
        public FixturePublishedTeamDto AwayTeam { get; set; }
    }
}
