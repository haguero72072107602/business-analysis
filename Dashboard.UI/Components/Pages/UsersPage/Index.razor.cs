using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Repository;
using Dashboard.UI.Components.Shared;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.UsersPage;

public partial class Index : ComponentBase
{
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private IUserRepository? UserRepository { get; set; }


    private IEnumerable<Users> _listUsers = new List<Users>();
    
    protected override async Task OnInitializedAsync()
    {
        _listUsers = await UserRepository?.GetAllAsync()!;
    }

    private async Task NewOrEditRowCellClick(Users? contextItem)
    {
        var parameters = new DialogParameters { { "Title", contextItem != null ? "Update user" : "New user" } };

        if (contextItem != null)
        {
            contextItem.ConfirmedPswd = contextItem.Password;
                
            parameters.Add("Model", contextItem);
        }

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var dialog = DialogService?
            .Show<DialogUser>("User", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            var data = (Users)result?.Data!;
            if (contextItem == null)
            {
                var rcdData = await UserRepository?.AddAsync(data)!;
                await UserRepository.SaveAsync();
                Snackbar?.Add("User create satisfactory", Severity.Success);
            }
            else
            {
                var rcdData = UserRepository?.Update(data)!;
            }
            
            await UserRepository?.SaveAsync()!;
            await OnInitializedAsync();
        }
    }

    private async Task DeleteRowCellClicked(Users? contextItem)
    {
        var parameters = new DialogParameters();

        
        if (contextItem != null)
        {
            parameters.Add("ContentText", "Esta seguro que sea eliminarlo...");
        }

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.ExtraSmall,
            FullWidth = true,
        };

        var dialog = DialogService?
            .Show<DialogDeleteConfirmation>("Delete", parameters, options);

        var result = await dialog?.Result!;
        if (result is { Canceled: false })
        {
            try
            {
                UserRepository?.Delete(contextItem!);
                Snackbar?.Add("Delete Ok", Severity.Success);
                await UserRepository?.SaveAsync()!;
            }
            catch (Exception e)
            {
                Snackbar?.Add(e.Message, Severity.Error);
            }

            
            await OnInitializedAsync();
        }
    }
}