namespace Falcon.Contracts;

[Serializable]
public class ContractException : Exception
{
    public ContractException(string message) : base(message) { }
    public ContractException(string message, Exception exception) : base(message, exception) { }
}
