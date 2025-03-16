namespace Falcon.WebApi.Abstractions;

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
        group.MapPost("/", this.PostAsync);
        group.MapGet("/{id}", this.GetAsync);
        group.MapPut("/{id}", this.UpdateAsync);
        group.MapDelete("/{id}", this.DeleteAsync);
        group.MapPatch("/{id}", this.PatchAsync);
        return group;
    }


    [HttpPost]
    public async Task<IResult> PostAsync(CommandRequest<TEntity> request, [FromServices] CommandService<TId, TEntity> service, [FromServices] LinkGenerator linkGenerator, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        if (item is null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Resource Creation Failed",
                Detail = "Failed to create the requested item. Check input data and try again."
            });
        }
        var locationUri = linkGenerator.GetPathByRouteValues(
                httpContext,
                routeName: "/{id}",
                values: new { id = item.Id }
            );
        if (string.IsNullOrEmpty(locationUri))
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "URI Generation Failed",
                Detail = "Unable to generate location URI for the created resource."
            });
        }
        return TypedResults.Created(locationUri, item);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetAsync(TId id, [FromServices] QueryService<TId, TEntity> service, CancellationToken cancellationToken)
    {
        var entity = await service.GetAsync(id, cancellationToken);
        if (entity is null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Entity Not Found",
                Detail = $"No resource was found with the ID '{id}'.",
                Status = StatusCodes.Status404NotFound,
                Instance = $"/{id}"
            });
        }
        return TypedResults.Ok(entity);
    }


    [HttpPut("{id}")]
    public async Task<IResult> UpdateAsync(TId id, CommandRequest<TEntity> request, [FromServices] CommandService<TId, TEntity> service, [FromServices] LinkGenerator linkGenerator, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var updatedItem = await service.UpdateAsync(id, request, cancellationToken);
        if (updatedItem is null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Resource Update Failed",
                Detail = "Failed to update the requested item. Check input data and try again.",
                Status = StatusCodes.Status400BadRequest
            });
        }
        var locationUri = linkGenerator.GetPathByRouteValues(
            httpContext,
            routeName: "/{id}",
            values: new { id = updatedItem.Id }
        );

        if (string.IsNullOrEmpty(locationUri))
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "URI Generation Failed",
                Detail = "Unable to generate location URI for the updated resource.",
                Status = StatusCodes.Status500InternalServerError
            });
        }
        return TypedResults.Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync(TId id, [FromServices] CommandService<TId, TEntity> service, CancellationToken cancellationToken)
    {
        var entity = await service.DeleteAsync(id, cancellationToken);
        if (entity is null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Entity Deletion Failed",
                Detail = $"The entity with ID '{id}' could not be deleted. Please try again later.",
                Status = StatusCodes.Status500InternalServerError
            });
        }
        return TypedResults.Ok(entity);
    }

    [HttpPatch("{id}")]
    public async Task<IResult> PatchAsync(TId id, [FromBody] CommandRequest<JsonPatchDocument<TEntity>> request, [FromServices] CommandService<TId, TEntity> service, CancellationToken cancellationToken)
    {
        if (request.Data is not { Operations.Count: > 0 })
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Invalid Patch Document",
                Detail = "The provided patch document is either null or contains no operations.",
                Status = StatusCodes.Status400BadRequest
            });
        }
        var result = await service.PatchAsync(id, request, cancellationToken);
        if (result == null)
        {
            return Results.Problem(new ProblemDetails
            {
                Title = "Patch Application Failed",
                Detail = "The patch could not be applied and the resource could not be updated.",
                Status = StatusCodes.Status500InternalServerError
            });
        }
        return TypedResults.Ok(result);
    }
}
