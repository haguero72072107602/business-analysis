using System.Reflection;
using Blazored.LocalStorage;
using Dashboard.Application.Behaviour;
using Dashboard.Application.Implementations;
using Dashboard.Application.Provider;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application.Extension;

public static class AddRegisterApplication
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<ProtectionStorage>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
        //services.AddAuthorizationCore();
        
        
        
        services.AddBlazoredLocalStorage();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof (IPipelineBehavior<,>), typeof (ValidationBehavior<,>));
        
        
        
        return services;
    }
}