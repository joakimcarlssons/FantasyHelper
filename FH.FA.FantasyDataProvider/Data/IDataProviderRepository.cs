﻿namespace FH.FA.FantasyDataProvider.Data
{
    public interface IDataProviderRepository
    {
        bool SaveChanges();

        // Gameweeks
        void SaveGameweek(Gameweek gameweek);
        IEnumerable<Gameweek> GetAllGameweeks();
        Gameweek GetGameweekById(int id);

        // Players
        void SavePlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(int id);

        // Teams
        void SaveTeam(Team team);
        IEnumerable<Team> GetAllTeams();
        Team GetTeamById(int id);
    }
}
