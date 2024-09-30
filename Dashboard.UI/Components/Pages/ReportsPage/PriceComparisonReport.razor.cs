using System.Collections;
using System.Runtime.InteropServices;
using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.ReportsPage;

public partial class PriceComparisonReport : ComponentBase
{
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] public IMUPRepository? MupRepository { get; set; }
    [Inject] public IProductOfferingRepository? ProductOfferingRepository { get; set; }
    
    
    private IEnumerable<MUP> _listMup = new List<MUP>();
    private IEnumerable<ProductOffering> _productOfferings = new List<ProductOffering>();
    private int _idMup;

    protected override async Task OnInitializedAsync()
    {
        _listMup = await MupRepository?.GetAllAsync()!;
    }

    private async Task ExecReport()
    {
        try
        {
            _productOfferings = await ProductOfferingRepository?.ReportPriceComparisonAsync(_idMup)!;
            Snackbar?.Add("Satisfactory process..", Severity.Success);
            StateHasChanged();
        }
        catch (Exception e)
        {
            Snackbar?.Add(e.Message, Severity.Error);
        }
        
    }
}