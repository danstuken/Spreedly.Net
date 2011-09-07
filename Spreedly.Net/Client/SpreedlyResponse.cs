namespace Spreedly.Net.Client
{
    using System;

    public class SpreedlyResponse<TEntity>
    {
        public TEntity Entity { get; set; }
        public SpreedlyStatus Status { get; set; }
        public Exception Error { get; set; }
    }
}