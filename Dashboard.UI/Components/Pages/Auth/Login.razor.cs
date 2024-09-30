using System.Security.Claims;
using Dashboard.Application.Interface;
using Dashboard.Application.Provider;
using Dashboard.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Dashboard.UI.Components.Pages.Auth 
{
    public partial class Login : ComponentBase
    {
        [Inject] private ISnackbar? SnackBar { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        [Inject] private IUserRepository? UserRepository { get; set; }

        public DtoUser Model { get; set; } = new();
        public InputType PasswordInput = InputType.Password;

        private async Task SubmitLogin()
        {
            var userAuth = await UserRepository?.LoginAsync(Model)!;
            
            if (userAuth != null)
            {
                try
                {
                    var customAuthStateProvider = (CustomAuthProvider)AuthenticationStateProvider!;

                    await customAuthStateProvider.UpdateAuthenticationState(new DtoUser(){
                        UserName = userAuth.UserName,
                        Password = userAuth.Password,
                        Role = userAuth.Role,
                        IdUser = userAuth.Id
                    });
                    
                    SnackBar?.Add("Successfully registered user");
                    NavigationManager?.NavigateTo("/");
                }
                catch (Exception e)
                {
                    SnackBar?.Add(e.Message, Severity.Error);    
                }
            }
            else
            {
                SnackBar?.Add("Login fail...");
            }
            
        }
    }
    
}