using System.Text.RegularExpressions;
using Dashboard.Application.Interface;
using Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.ProdOfferingPage;

public partial class ImportProdOffering : ComponentBase
{
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] public IProductOfferingRepository? ProductOfferingRepository { get; set; }
    [Inject] public ISupplierRepository? SupplierRepository { get; set; }


    private IList<ProductOffering>?  _productOfferings = new List<ProductOffering>();
    private IEnumerable<Supplier> _suppliers = new List<Supplier>();
    private int _idSupplier;
    private bool _disableBtn = true;
    

    protected override async Task OnInitializedAsync()
    {
        _suppliers = await SupplierRepository?.GetAllAsync()!;
    }


    private async Task UploadFiles2(IBrowserFile file)
    {
        try
        {
            _productOfferings = new List<ProductOffering>();
        
            var stream = file.OpenReadStream();
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();
            var outputFileString = System.Text.Encoding.UTF8.GetString(ms.ToArray());


            var productRows = outputFileString.Split("\n").ToList();
        
            foreach (var product in productRows )
            {
                var productStringList = product.Split(",").ToList();

                 
                if (productStringList[0].Contains('P'))
                {
                    var priceProduct = productStringList[14].Replace('"', ' ');
                    _productOfferings.Add( new ProductOffering()
                    {
                        Supc = productStringList[1].Replace('"', ' '),
                        Pack = productStringList[7].Replace('"', ' '),
                        Size = productStringList[8].Replace('"', ' '),
                        Unit = productStringList[9].Replace('"', ' '),
                        Brand = productStringList[10].Replace('"', ' '),
                        Desc = productStringList[12].Replace('"', ' '),
                        Cat = productStringList[13].Replace('"', ' '),
                        Price = Convert.ToDecimal(priceProduct.Trim() == "" ? "0" : priceProduct),
                        Stock = productStringList[23].Replace('"', ' '),
                    });
                }
            }

            _disableBtn = _productOfferings.Count == 0;
            
            Snackbar?.Add("Importation success..", Severity.Success);
            
            StateHasChanged();

        }
        catch (Exception e)
        {
            Snackbar?.Add(e.Message, Severity.Error);
        }
        
    }

    private string[] SplitCSV(string input)
    {
        //Excludes commas within quotes  
        var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
        List<string> list = new List<string>();
        string curr = null;
        foreach (Match match in csvSplit.Matches(input))
        {
            curr = match.Value;
            if (0 == curr.Length) list.Add("");

            list.Add(curr.TrimStart(','));
        }

        return list.ToArray();
    }

    private async Task ImportData()
    {
        try
        {
            await ProductOfferingRepository?.LoadDataCsvAsync(_idSupplier, _productOfferings!)!;
            
            Snackbar?.Add("Importacion de datos satisfactorio...", Severity.Success);
        }
        catch (Exception e)
        {
            Snackbar?.Add(e.Message, Severity.Error);
        }
    }
}