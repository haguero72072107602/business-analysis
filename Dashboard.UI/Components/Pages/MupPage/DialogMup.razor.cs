using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;

namespace Dashboard.UI.Components.Pages.MupPage;

public partial class DialogMup : ComponentBaseDialog
{
    [Parameter] public MUP? Model { get; set; } = new MUP()
    {
        Code = string.Empty,
        Description = string.Empty,
        Price = 0
    };

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new MuPValidator().Validate(Model!);

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