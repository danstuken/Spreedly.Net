namespace Spreedly.Net.Client
{
    using RestSharp;

    public interface IStatusResolver
    {
        SpreedlyStatus Resolve(Parameter statusHeader);
    }
}