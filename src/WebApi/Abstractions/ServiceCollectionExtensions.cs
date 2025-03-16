namespace Falcon.WebApi.Abstractions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApiAbstraction(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseWebApiAbstractionMiddelwares(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseWebApiAbstractionModule(this IApplicationBuilder app)
    {
        return app;
    }
}
