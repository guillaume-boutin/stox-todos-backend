using Application.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services
    )
    {
        services.AddScoped<ITodosRepository, TodosRepository>();

        return services;
    }
}
