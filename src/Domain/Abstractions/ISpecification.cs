namespace Falcon.Domain.Abstractions;
public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}
