namespace Spreedly.Client
{
    using RestSharp;

    public interface IStatusResolver
    {
        SpreedlyStatus Resolve(Parameter statusHeader);
    }
}