namespace Falcon.Domain.Abstractions.Interfaces.Entities;

public interface IConcurrencyEntity
{
    int ConcurrencyStamp { get; set; }
}
