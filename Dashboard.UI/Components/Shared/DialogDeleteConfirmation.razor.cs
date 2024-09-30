using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Shared
{
    public partial class DialogDeleteConfirmation : ComponentBase
    {
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        [Parameter] public string? ContentText { get; set; }

        void Submit()
        {
            MudDialog?.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog?.Cancel();
    }
}