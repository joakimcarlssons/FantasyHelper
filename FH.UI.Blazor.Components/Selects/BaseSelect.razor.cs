namespace FH.UI.Blazor.Components.Selects
{
    public partial class BaseSelect
    {
        [Parameter]
        public List<string> Options { get; set; } = new();

        [Parameter]
        public string DefaultValue { get; set; } = "";

        [Parameter]
        public EventCallback<string> OnSelect { get; set; }

        private async Task OnItemSelected(string value)
        {
            await OnSelect.InvokeAsync(value);
        }
    }
}
