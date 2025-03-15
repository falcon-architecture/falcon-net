namespace Falcon.Infrastructure.Abstractions;

[Serializable]
public class QueryException : Exception
{
    public QueryException(string message) : base(message) { }
    public QueryException(string message, Exception exception) : base(message, exception) { }

}
