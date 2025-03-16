namespace Falcon.Application.Abstractions;

public abstract class Endpoints<TId, TEntity> : IEndpoints where TEntity : class, IEntity<TId>, IAggregateRoot, new()
{
    public abstract string Route { get; }
    public IEndpointRouteBuilder MapApis(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup(Route);
        return MapDefaultApis(group);
    }
    public virtual IEndpointRouteBuilder MapDefaultApis(IEndpointRouteBuilder group)
    {
        group.MapPost("/", PostAsync);
        group.MapGet("/{id}", GetAsync);
        // group.MapPut("/{id}", PutAsync);
        // group.MapDelete("/{id}", DeleteAsync);
        // group.MapPatch("/{id}", PatchAsync);
        return group;
    }

    [HttpGet("{id}")]
    public async Task<Results<Ok<TEntity>, NotFound>> GetAsync(TId id, [FromServices] QueryService<TId, TEntity> service, CancellationToken cancellationToken)
    {
        var item = await service.GetAsync(id, cancellationToken);
        return item is null ? TypedResults.NotFound() : TypedResults.Ok(item);
    }

    [HttpPost]
    public async Task<IResult> PostAsync(CommandRequest<TEntity> request, [FromServices] CommandService<TId, TEntity> service, [FromServices] LinkGenerator linkGenerator, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        if (item is not null)
        {
            var locationUri = linkGenerator.GetPathByRouteValues(
                httpContext,
                routeName: "/{id}",
                values: new { id = item.Id }
            );
            if (!string.IsNullOrEmpty(locationUri))
            {
                return TypedResults.Created(locationUri, item);
            }
            return Results.Problem(new ProblemDetails
            {
                Title = "URI Generation Failed",
                Detail = "Unable to generate location URI for the created resource."
            });
        }
        return Results.Problem(new ProblemDetails
        {
            Title = "Resource Creation Failed",
            Detail = "Failed to create the requested item. Check input data and try again."
        });
    }

    
}
