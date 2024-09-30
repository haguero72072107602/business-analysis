using Dashboard.Application.Provider;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dashboard.UI.Components.Pages.Auth;

public partial class Logout : ComponentBase
{
    [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var customAuthStateProvider = (CustomAuthProvider)AuthenticationStateProvider!;

        await customAuthStateProvider.UpdateAuthenticationState(null);
        
        NavigationManager!.NavigateTo("/");
    }
}