using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Utilities;

public class ComponentBaseDialog : ComponentBase
{
    [Inject] public ISnackbar? SnackBar { get; set; }
    [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }
    
    [Parameter] public string? Title { get; set; }
}