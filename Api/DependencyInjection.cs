using System.Reflection;
using Infrastructure.Persistence;
using Mapster;
using MapsterMapper;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddCorsPolicy(config)
            .AddMappings();
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        string[] allowedCorsOrigins =
            config.GetSection("AllowedCorsOrigins").Get<string[]>() ?? new string[] { };

        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "MyAllowSpecificOrigins",
                policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(
                            origin => allowedCorsOrigins.Any(aco => origin.ToLower() == aco)
                        );
                }
            );
        });

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddDbContext<PostgresContext>();

        return services;
    }
}
