using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.UI.Blazor.Components.ViewModels
{
    public class FixtureViewModel
    {
        public int GameweekId { get; set; }
        public FixtureTeamViewModel HomeTeam { get; set; }
        public FixtureTeamViewModel AwayTeam { get; set; }
        public bool IsFinished { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}
