namespace Falcon.Infrastructure.Abstractions;

public class PersistenceException : Exception
{
    public PersistenceException() { }
    public PersistenceException(string message) : base(message) { }
    public PersistenceException(string message, Exception innerException) : base(message, innerException) { }
}

public class CreateEntityException : PersistenceException
{
    public CreateEntityException() { }
    public CreateEntityException(string message) : base(message) { }
    public CreateEntityException(string message, Exception innerException) : base(message, innerException) { }
}

public class DeleteEntityException : PersistenceException
{
    public DeleteEntityException() { }
    public DeleteEntityException(string message) : base(message) { }
    public DeleteEntityException(string message, Exception innerException) : base(message, innerException) { }
}


public class QueryEntityException : PersistenceException
{
    public QueryEntityException() { }
    public QueryEntityException(string message) : base(message) { }
    public QueryEntityException(string message, Exception innerException) : base(message, innerException) { }
}


public class UpdateEntityException : PersistenceException
{
    public UpdateEntityException() { }
    public UpdateEntityException(string message) : base(message) { }
    public UpdateEntityException(string message, Exception innerException) : base(message, innerException) { }

}
