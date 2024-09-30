using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.Infrastructure.Repository;
using Dashboard.UI.Components.Shared;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.MupPage;

public partial class Index : ComponentBase
{
    [Inject] private IMUPRepository? MupRepository { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    
    private IEnumerable<MUP> _listMup = new List<MUP>();

    protected override async Task OnInitializedAsync()
    {
        _listMup = await MupRepository?.GetAllAsync()!;
    }

    private async Task NewOrEditRowCellClick(MUP? contextItem)
    {
        var parameters = new DialogParameters();

        parameters.Add("Title", contextItem != null ? "Update master product" : "New master product"); 

        if (contextItem != null)
        {
            parameters.Add("Model", contextItem);
        }

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var dialog = DialogService?
            .Show<DialogMup>("", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            if (contextItem == null)
            {
                await MupRepository?.AddAsync((MUP)result.Data)!;
                Snackbar?.Add("Entity create satisfactory", Severity.Success);
            }
            else
            {
                MupRepository?.Update((MUP)result.Data);
                Snackbar?.Add("Entity update satisfactory", Severity.Success);
            }

            await MupRepository?.SaveAsync()!;
            
            await OnInitializedAsync();
        }
    }

    private async Task DeleteRowCellClicked(MUP? contextItem)
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
                MupRepository?.Delete(contextItem!);
                await MupRepository?.SaveAsync()!;
                Snackbar?.Add("Delete Ok", Severity.Info);
                
            }
            catch (Exception e)
            {
                Snackbar?.Add(e.Message, Severity.Error);
            }

            await OnInitializedAsync();
        }
    }
}