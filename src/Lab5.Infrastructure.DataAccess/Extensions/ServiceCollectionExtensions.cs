using Lab5.Application.Abstractions.Repositories;
using Lab5.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(this IServiceCollection collection)
    {
        collection.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
        collection.AddSingleton<IAccountOperationRepository, InMemoryAccountOperationRepository>();
        collection.AddSingleton<ISessionRepository, InMemorySessionRepository>();

        return collection;
    }
}