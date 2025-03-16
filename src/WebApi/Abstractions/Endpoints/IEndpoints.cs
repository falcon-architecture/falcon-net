namespace Falcon.WebApi.Abstractions;

public interface IEndpoints
{
    IEndpointRouteBuilder MapApis(IEndpointRouteBuilder route);
}
