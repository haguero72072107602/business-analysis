using Dashboard.Application.Interface;
using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;

namespace Dashboard.UI.Components.Pages.ProdOfferingPage;

public partial class DialogProdOffering : ComponentBaseDialog
{
    [Inject] public IMUPRepository? MupRepository { get; set; }
    [Inject] public ISupplierRepository? SupplierRepository { get; set; }
    [Parameter] public ProductOffering? Model { get; set; } = new ProductOffering();
    
    
    private IEnumerable<MUP> _listMup = new List<MUP>();
    private IEnumerable<Supplier> _listSuppliers = new List<Supplier>();

    protected override async Task OnInitializedAsync()
    {
        _listMup = await MupRepository?.GetAllAsync()!;
        _listSuppliers = await SupplierRepository?.GetAllAsync()!;
    }

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new ProdOfferingValidator().Validate(Model!);

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