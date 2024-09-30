using System.Security.Claims;
using Dashboard.Application.Implementations;
using Dashboard.Application.Interface;
using Dashboard.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dashboard.Application.Provider;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly ProtectionStorage _protectionStorage;
    private readonly IUserRepository _userRepository;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthProvider(ProtectionStorage protectionStorage, IUserRepository userRepository)
    {
        _protectionStorage = protectionStorage;
        _userRepository = userRepository;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var sessionStorageResult = await _protectionStorage.LoadDataAsync<DtoUser>("UserSession");
            
            if (sessionStorageResult == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));
            
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity((new List<Claim>()
            {
                new Claim(ClaimTypes.Name, sessionStorageResult?.UserName!),
                new Claim(ClaimTypes.Role, sessionStorageResult?.Role!)
            }), "CustomAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch( Exception e)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(DtoUser? userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _protectionStorage.SaveDataAsync("UserSession", userSession);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity((new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userSession?.UserName!),
                new Claim(ClaimTypes.Role, userSession?.Role!)
            }), "CustomAuth"));
        }
        else
        {
            await _protectionStorage.EraseDataAsync("UserSession");
            claimsPrincipal = _anonymous;
        }
        
        NotifyAuthenticationStateChanged( Task.FromResult( new AuthenticationState(claimsPrincipal)));
    }

    public async Task<DtoUser?> GetUserAuth()
    {
        if (await _protectionStorage.ContainKey("UserSession"))
        {
            return await _protectionStorage.LoadDataAsync<DtoUser>("UserSession");
        }
        else
        {
            return null;
        }
    }

}