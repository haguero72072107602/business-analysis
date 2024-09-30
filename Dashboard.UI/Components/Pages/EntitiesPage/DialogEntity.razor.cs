using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;

namespace Dashboard.UI.Components.Pages.EntitiesPage;

public partial class DialogEntity : ComponentBaseDialog
{
    [Parameter] public Entities? Model { get; set; } = new Entities()
    {
        Code = string.Empty,
        Description = string.Empty
    };

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new EntityValidator().Validate(Model!);

        if (resultValid.IsValid)
        {
            MudDialog?.Close(Model);
        }
        else
        {
            SnackBar?.ShowError(resultValid.Errors);
        }
    }
}