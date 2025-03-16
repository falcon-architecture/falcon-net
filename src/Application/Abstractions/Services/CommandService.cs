namespace Falcon.Application.Abstractions.Services;

public class CommandService<TId, TEntity> : ApplicationService<TId, TEntity> where TEntity : class, IEntity<TId>, new()
{
    public CommandService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public async Task<TEntity> CreateAsync(ICommandRequest<TEntity> request, CancellationToken cancellationToken)
    {
        ValidateAsync<TEntity>(request.Data);
        return await GetRepository().CreateAsync(request, cancellationToken);
    }

    public Task<TEntity> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        return GetRepository().DeleteAsync(id, cancellationToken);
    }

    public Task<TEntity> PatchAsync(TId id, ICommandRequest<JsonPatchDocument<TEntity>> request, CancellationToken cancellationToken)
    {
        return GetRepository().PatchAsync(id, request, cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TId id, ICommandRequest<TEntity> request, CancellationToken cancellationToken)
    {
        ValidateAsync<TEntity>(request.Data);
        return GetRepository().UpdateAsync(id, request, cancellationToken);
    }

    protected void ValidateAsync<T>(T entity)
    {
        var _validator = GetService<IValidator<T>>();
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            throw new ValidationException(errors);
        }
    }
}