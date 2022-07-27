using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.UI.Blazor.Components.ViewModels
{
    public class PlannerTeamCurrentTeamViewModel
    {
        public int Index { get; set; }
        public PlannerPlayerViewModel Player { get; set; }

        public PlannerTeamCurrentTeamViewModel(int index, PlannerPlayerViewModel player)
        {
            Index = index;
            Player = player;
        }
    }

    public class PlannerTeamViewModel
    {
        public int Gameweek { get; set; }
        public List<PlannerTeamCurrentTeamViewModel> Players { get; set; }
    }
}
