using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.UsersPage;

public partial class DialogUser : ComponentBaseDialog
{
    [Parameter] public Users? Model { get; set; } = new Users
    {
        //Email = string.Empty,
        //Password = string.Empty,
        //Role = string.Empty,
        //UserName = string.Empty
    };

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new UserValidator().Validate(Model!);

        if (resultValid.IsValid)
        {
            //SnackBar?.Add("Ok data validation");
            MudDialog?.Close(Model);
        }
        else
        {
            SnackBar?.ShowError(resultValid.Errors);
        }
    }
}