namespace Falcon.Infrastructure.Abstractions;

[Serializable]
public class DeleteException : Exception
{
    public DeleteException(string message) : base(message) { }
    public DeleteException(string message, Exception exception) : base(message, exception) { }
}
