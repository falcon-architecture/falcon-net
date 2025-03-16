namespace Falcon.Application.Abstractions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration, string configPath)
    {
        return services.AddTransient<IIdentityService, IdentityService>()
                       .AddFluentValidationAutoValidation();
    }

    public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder app)
    {
        return app;
    }

    public static IApplicationBuilder UseInfrastructureModule(this IApplicationBuilder app)
    {
        return app;
    }
}