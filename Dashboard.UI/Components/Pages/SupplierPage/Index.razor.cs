using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Dashboard.UI.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.SupplierPage;

public partial class Index : ComponentBase
{
    [Inject] private ISupplierRepository? SupplierRepository { get; set; }
    [Inject] private IUserSupplierRepository? UserSupplierRepository { get; set; }
    
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }

    private IEnumerable<Supplier> _listSupplier = new List<Supplier>();
    

    protected override async Task OnInitializedAsync()
    {
        _listSupplier = await SupplierRepository?.GetAllAsync()!;
        
    }

    
    private async Task DeleteRowCellClicked(Supplier? contextItem)
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
                await UserSupplierRepository?.DeleteSupplierUserAsync((int)contextItem?.Id!, 1)!;
                await SupplierRepository?.DeleteSupplierAsync((int)contextItem?.Id!)!;

                await SupplierRepository.SaveAsync();

                Snackbar?.Add("Delete Ok", Severity.Info);
            }
            catch (Exception e)
            {
                Snackbar?.Add(e.Message, Severity.Error);
            }
            
            await OnInitializedAsync();
        }
    }
    
    private async Task NewOrEditRowCellClick(Supplier? contextItem)
    {
        var parameters = new DialogParameters();
        
        parameters.Add("Title", contextItem != null ? "Update supplier" : "New supplier") ;

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
            .Show<DialogAddSupplier>(contextItem == null ? "New supplier" : "Edit supplier", parameters, options);

        var result = await dialog?.Result!;
        
        if (result is { Canceled: false, Data: not null })
        {
            if (contextItem == null)
            {
                var data = (Supplier)result?.Data!;
                var rcdData = await SupplierRepository?.AddAsync(data)!;
                await SupplierRepository.SaveAsync();

                var idSupplier = rcdData.Entity.Id;

                await UserSupplierRepository?.InsertUserSupplierAsync(1, rcdData.Entity.Id)!;

                Snackbar?.Add("Supplier create satisfactory");
            }
            else
            {
                var data = (Supplier)result?.Data!;
                var rcdData = SupplierRepository?.Update(data)!;
            }
            
            await SupplierRepository?.SaveAsync()!;
            
            await OnInitializedAsync();
        }
    }
}