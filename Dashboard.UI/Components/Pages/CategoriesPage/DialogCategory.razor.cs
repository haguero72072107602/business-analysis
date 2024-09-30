using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.CategoriesPage;

public partial class DialogCategory : ComponentBaseDialog
{
    [Parameter] public Categories? Model { get; set; } = new Categories()
    {
        /*Code = string.Empty,*/
        Description = string.Empty
    };

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new CategoryValidator().Validate(Model!);

        if (resultValid.IsValid)
        {
            SnackBar?.Add("Ok data validation");
            MudDialog?.Close(Model);
        }
        else
        {
            SnackBar?.ShowError(resultValid.Errors);
        }
    }
}