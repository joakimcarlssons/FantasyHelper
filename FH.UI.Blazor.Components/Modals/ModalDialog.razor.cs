namespace FH.UI.Blazor.Components.Modals
{
    public partial class ModalDialog
    {
        #region Parameters

        [Parameter]
        public bool IsVisible { get; set; } = false;

        [Parameter]
        public string BackgroundColor { get; set; } = "#FFFFFF";

        [Parameter]
        public RenderFragment ModalContent { get; set; }

        #endregion
    }
}
