using System.Reflection;

namespace FH.UI.Blazor.Components.ViewModels
{
    /// <summary>
    /// View model for a player within a team in a plan. We add an index property to keep track of where the player is positioned in the team.
    /// </summary>
    public class PlannerPlayerInTeamViewModel : PlannerPlayerViewModel
    {
        /// <summary>
        /// The index of the player in the team. This property is used to check whether a player has changed position/been removed etc
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Map parent values in constructor to keep the selected player
        /// </summary>
        public PlannerPlayerInTeamViewModel(PlannerPlayerViewModel player)
        {
            foreach (FieldInfo prop in player.GetType().GetFields())
                GetType().GetField(prop.Name)?.SetValue(this, prop.GetValue(player));
            foreach (PropertyInfo prop in player.GetType().GetProperties())
                GetType().GetProperty(prop.Name)?.SetValue(this, prop.GetValue(player, null), null);
        }
    }

    /// <summary>
    /// View model for a team shown in a plan
    /// </summary>
    public class PlannerTeamViewModel
    {
        /// <summary>
        /// The gameweek that the team belongs to
        /// </summary>
        public int Gameweek { get; set; }

        /// <summary>
        /// The players in the team of that gameweek
        /// </summary>
        public List<PlannerPlayerInTeamViewModel> Players { get; set; }
    }
}
