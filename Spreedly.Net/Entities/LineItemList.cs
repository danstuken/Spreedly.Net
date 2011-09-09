namespace Spreedly.Net.Entities
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "line-items")]
    public class LineItemList
    {
        [XmlElement(ElementName = "line-item")]
        public List<LineItem> LineItems { get; set; }

        public LineItemList()
        {
            LineItems = new List<LineItem>();
        }
    }
}