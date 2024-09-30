using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.UI.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.CategoriesPage;

public partial class Index : ComponentBase
{
    [Inject] private ICategoriesRepository? CategoriesRepository { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
        
    private IEnumerable<Categories> _listCategories = new List<Categories>();


    protected override async Task OnInitializedAsync()
    {
        _listCategories = await CategoriesRepository?.GetAllAsync()!;
    }

    private async Task NewOrEditRowCellClick(Categories? contextItem)
    {
        var parameters = new DialogParameters();
        
        parameters.Add("Title", contextItem != null ? "Update category" : "New category");

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
            .Show<DialogCategory>(contextItem == null ? "New supplier" : "Edit supplier", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            if (contextItem == null)
            {
                await CategoriesRepository?.AddAsync((Categories)result.Data)!;
                Snackbar?.Add("Supplier create satisfactory");
            }
            else
            {
                CategoriesRepository?.Update((Categories)result.Data);
                Snackbar?.Add("Supplier update satisfactory");
            }

            await CategoriesRepository?.SaveAsync()!;
            
            await OnInitializedAsync();
        }
    }

    private async Task DeleteRowCellClicked(Categories? contextItem)
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
                /* Eliminar las ofertas */
                CategoriesRepository?.Delete(contextItem!);
                await CategoriesRepository?.SaveAsync()!;

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