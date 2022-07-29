using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace FH.UI.Blazor.Components.Buttons
{
    public partial class PlannerNavButton
    {
        [Inject]
        public IJSRuntime JS { get; set; }

        #region Parameters

        [Parameter]
        public PlannerPlanViewModel Plan { get; set; }


        #region Events

        [Parameter]
        public EventCallback<PlannerPlanViewModel> Activate { get; set; }

        [Parameter]
        public EventCallback<PlannerPlanViewModel> UpdateName { get; set; }

        [Parameter]
        public EventCallback<PlannerPlanViewModel> Remove { get; set; }

        #endregion

        #endregion

        #region Properties
        private bool IsRemovable => Plan?.PlanId > 1;
        private bool IsEditable { get; set; }
        private ElementReference TextInput { get; set; }

        #endregion

        #region Actions

        public async Task ActivatePlan()
        {
            if (Plan.IsActive) return;
            await Activate.InvokeAsync(Plan);
        }

        public async Task EditContent(MouseEventArgs args)
        {
            IsEditable = true;
            await JS.InvokeAsync<string>("clearSelection", ".planner-nav-btn__content input", Plan.PlanId - 1);
        }

        public async Task UpdateNameOfPlan()
        {
            await UpdateName.InvokeAsync(Plan);
        }

        public async Task RemovePlan()
        {
            await Remove.InvokeAsync(Plan);
        }

        #endregion
    }
}
