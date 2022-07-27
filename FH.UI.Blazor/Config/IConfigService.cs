namespace FH.UI.Blazor.Config
{
    public interface IConfigService
    {
        string GetAllGameweeksURL();
        string GetAllPlayersURL();
        string GetAllTeamsURL();
        string GetAllFixturesURL();
        string GetFixtureByTeamURL(int teamId);
        string GetPlannerPlayers();
    }
}
