using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.UI.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.EntitiesPage;

public partial class Index : ComponentBase
{
    [Inject] private IEntitiesRepository? EntitiesRepository { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    
    private IEnumerable<Entities> _listEntities = new List<Entities>();

    protected override async Task OnInitializedAsync()
    {
        _listEntities = await EntitiesRepository?.GetAllAsync()!;
    }

    private async Task NewOrEditRowCellClick(Entities? contextItem)
    {
        var parameters = new DialogParameters();

        parameters.Add("Title", contextItem != null ? "Update entity" : "New entity"); 

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
            .Show<DialogEntity>("", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            if (contextItem == null)
            {
                await EntitiesRepository?.AddAsync((Entities)result.Data)!;
                Snackbar?.Add("Entity create satisfactory", Severity.Success);
            }
            else
            {
                EntitiesRepository?.Update((Entities)result.Data);
                Snackbar?.Add("Entity update satisfactory", Severity.Success);
            }

            await EntitiesRepository?.SaveAsync()!;
            
            await OnInitializedAsync();
        }
    }

    private async Task DeleteRowCellClicked(Entities contextItem)
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
                EntitiesRepository?.Delete(contextItem!);
                await EntitiesRepository?.SaveAsync()!;
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