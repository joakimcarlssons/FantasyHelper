using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.UI.Blazor.Components.ViewModels
{
    public class PlannerPlanViewModel
    {
        /// <summary>
        /// The id of the plan
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// The name of the plan
        /// </summary>
        public string Name { get; set; } = "New Plan";

        /// <summary>
        /// The teams set up in the plan
        /// </summary>
        public List<PlannerTeamViewModel> Teams { get; set; }

        /// <summary>
        /// Flag inidicating if the plan is selected to be shown or not
        /// </summary>
        public bool IsActive { get; set; }
    }
}
