using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Sessions;
using Lab5.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ISessionApplicationService, SessionApplicationService>();
        collection.AddScoped<IAccountApplicationService, AccountApplicationService>();

        return collection;
    }
}