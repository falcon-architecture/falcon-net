namespace Falcon.Contracts;

public class CompositeRequest : ICompositeRequest
{
    public CompositeRequest()
    {
        Requests = [];
    }
    public Dictionary<string, IApiRequest> Requests { get; set; }
}

public static partial class Extensions
{
    #region CompositeRequest
    public static ICompositeRequest Add(this ICompositeRequest source, string key, IApiRequest value)
    {
        var dictionary = source.Requests ??= [];
        dictionary.Add(key, value);
        return source;
    }
    #endregion
}