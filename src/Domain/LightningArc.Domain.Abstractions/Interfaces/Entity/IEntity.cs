namespace LightningArc.Domain.Abstractions;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}