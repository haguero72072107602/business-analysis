using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.UI.Components.Pages.UsersPage;
using Dashboard.UI.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.ProdOfferingPage;

public partial class Index : ComponentBase
{
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private IProductOfferingRepository? ProductOfferingRepository { get; set; }


    private IEnumerable<ProductOffering> _listProductOfferings = new List<ProductOffering>();
    
    protected override async Task OnInitializedAsync()
    {
        _listProductOfferings = await ProductOfferingRepository?.GetAllAsync()!;
    }

    private async Task NewOrEditRowCellClick(ProductOffering? contextItem)
    {
        var parameters = new DialogParameters();
        
        parameters.Add("Title", contextItem != null ? "Update product offering" : "New product offering");

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
            .Show<DialogProdOffering>(contextItem == null ? "New supplier" : "Edit supplier", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            if (contextItem == null)
            {
                await ProductOfferingRepository?.AddAsync((ProductOffering)result.Data)!;
                Snackbar?.Add("Supplier create satisfactory");
            }
            else
            {
                ProductOfferingRepository?.Update((ProductOffering)result.Data);
                Snackbar?.Add("Supplier update satisfactory");
            }

            await ProductOfferingRepository?.SaveAsync()!;
            
            await OnInitializedAsync();
        }
    }

    private async Task DeleteRowCellClicked(ProductOffering? contextItem)
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
                ProductOfferingRepository?.Delete(contextItem!);
                Snackbar?.Add("Delete Ok", Severity.Success);
                await ProductOfferingRepository?.SaveAsync()!;
            }
            catch (Exception e)
            {
                Snackbar?.Add(e.Message, Severity.Error);
            }

            
            await OnInitializedAsync();
        }
    }

    private void ImportRowCellClick()
    {
        NavigationManager?.NavigateTo("/Product-Offering/import");
    }
}