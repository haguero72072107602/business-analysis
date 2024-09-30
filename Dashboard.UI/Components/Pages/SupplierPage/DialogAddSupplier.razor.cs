using Dashboard.Application.Interface;
using Dashboard.Application.Validators;
using Dashboard.Domain.Entities;
using Dashboard.UI.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.SupplierPage;

public partial class DialogAddSupplier : ComponentBaseDialog
{
    [Inject] private IEntitiesRepository? EntitiesRepository { get; set; }
    [Parameter] public Supplier? Model { get; set; } = new Supplier
    {
        Code = string.Empty,
        Description = string.Empty
    };
    
    private IEnumerable<Entities> _entitiesList = new List<Entities>();

    protected override async Task OnInitializedAsync()
    {
        _entitiesList = await EntitiesRepository?.GetAllAsync()!;
    }

    private void Cancel()
    {
        MudDialog?.Cancel();
    }

    private void Submit()
    {
        var resultValid = new SupplierValidator().Validate(Model!);

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