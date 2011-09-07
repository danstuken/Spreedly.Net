namespace Spreedly.Net.Entities
{
    using System.Collections.Generic;

    public class SubscriberList
    {
        public List<Subscriber> Subscribers { get; set; }

        public SubscriberList()
        {
            Subscribers = new List<Subscriber>();
        }
    }
}