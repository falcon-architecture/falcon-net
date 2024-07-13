namespace LightningArc.Infrastructure.EntityFramework;

public static class ModuleExtensions
{
    public static IServiceCollection AddInfrastructureEntityFrameworksModule(this IServiceCollection services, IConfiguration configuration, string configPath)
    {
        services.Configure<SqlDbOptions>(configuration.GetSection(configPath));
        return services.AddInfrastructureEntityFrameworksModule();
    }

    public static IServiceCollection AddInfrastructureEntityFrameworksModule(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<SqlDbOptions>(section);
        return services.AddInfrastructureEntityFrameworksModule();
    }

    public static IServiceCollection AddInfrastructureEntityFrameworksModule(this IServiceCollection services, Action<SqlDbOptions> configure)
    {
        services.Configure(configure);
        return services.AddInfrastructureEntityFrameworksModule();
    }

    private static IServiceCollection AddInfrastructureEntityFrameworksModule(this IServiceCollection services)
    {
        services.AddScoped(typeof(ITenant), typeof(SingleTenant));

        services.AddDbContext<HomeDbContext>(ServiceLifetime.Singleton);
        services.AddScoped<HomeDbContextOptions>();
        services.AddScoped(typeof(HomeRepository<,>));
        services.AddScoped(typeof(IOptions<DbConfig>), typeof(HomeOptions<DbConfig>));

        services.AddDbContext<TenantDbContext>(ServiceLifetime.Scoped);
        services.AddScoped<TenantDbContextOptions>();
        services.AddScoped<TenantDbMigrationContext>();
        services.AddScoped(typeof(TenantRepository<,>));

        services.AddScoped<IRepositoryProvider, SqlRepositoryProvider>();
        services.AddSingleton<ITenantDbModelConfiguration, TenantModelConfigurations>();
        services.AddSingleton<IHomeDbModelConfiguration, HomeModelConfigurations>();

        services.AddHealthChecks().AddCheck<SqlDbHealthCheck>("SqlDb", tags: ["sqldb", "all"]);

        return services;
    }

    public static IApplicationBuilder UsePersistenceEntityFrameworkMiddelwares(this IApplicationBuilder app)
    {
        return app;
    }

    public static IApplicationBuilder UsePersistenceEntityFrameworkModule(this IApplicationBuilder app)
    {
        return app;
    }
}