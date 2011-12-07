namespace Spreedly.Entities
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName="lifetime-complimentary-subscription")]
    public class LifetimeComplimentarySubscription
    {
        [XmlElement(ElementName = "feature-level")]
        public string FeatureLevel { get; set; }
    }
}