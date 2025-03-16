namespace Falcon.WebApi.Abstractions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrasttructureModule(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseInfrastructureMiddelwares(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseInfrastructureModule(this IApplicationBuilder app)
    {
        return app;
    }
}