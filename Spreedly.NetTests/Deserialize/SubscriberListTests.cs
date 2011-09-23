namespace Spreedly.NetTests.Deserialize
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using NUnit.Framework;
    using Net.Entities;
    using Shouldly;

    public class SubscriberListTests
    {
        private const string xmlList = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<subscribers type=""array"">
  <subscriber>
    <created-at type=""datetime"">2011-09-07T16:34:20Z</created-at>
    <customer-id>001</customer-id>
    <screen-name>ScreenName1</screen-name>
  </subscriber>
  <subscriber>
    <created-at type=""datetime"">2011-09-02T16:10:53Z</created-at>
    <customer-id>2095</customer-id>
    <screen-name nil=""true""></screen-name>
  </subscriber>
  <subscriber>
    <created-at type=""datetime"">2011-09-02T16:09:55Z</created-at>
    <customer-id>88225</customer-id>
    <screen-name nil=""true""></screen-name>
  </subscriber>
  <subscriber>
    <created-at type=""datetime"">2011-09-06T16:59:34Z</created-at>
    <customer-id>aef789956af61024d82ec270039601b91e06262a</customer-id>
    <screen-name nil=""true""></screen-name>
  </subscriber>
</subscribers>
";
        private const string emptyXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<subscribers type=""array"">
</subscribers>
";

        private XmlSerializer _serializer;

        [SetUp]
        public void Init()
        {
            _serializer = new XmlSerializer(typeof(SubscriberList));
        }

        [Test]
        public void Deserializing_ValidSubscriberList_ThrowsNoException()
        {
            var subscriberList = (SubscriberList) _serializer.Deserialize(XmlReader.Create(new StringReader(xmlList)));

            Assert.True(true);
        }

        [Test]
        public void Deserializing_ValidSubscriberList_ReturnsNotNullList()
        {
            var subscriberList = (SubscriberList)_serializer.Deserialize(XmlReader.Create(new StringReader(xmlList)));

            Assert.NotNull(subscriberList);
        }

        [Test]
        public void Deserializing_NonEmptySubscriberList_ReturnsNonEmptyList()
        {
            var subscriberList = (SubscriberList)_serializer.Deserialize(XmlReader.Create(new StringReader(xmlList)));

            Assert.True(subscriberList.Subscribers.Count > 0);
        }

        [Test]
        public void Deserializing_EmptyList_ReturnsNotNullList()
        {
            var subscriberList = (SubscriberList)_serializer.Deserialize(XmlReader.Create(new StringReader(emptyXml)));

            Assert.NotNull(subscriberList);
        }

        [Test]
        public void Deserializing_EmptyList_ReturnsEmptyList()
        {
            var subscriberList = (SubscriberList)_serializer.Deserialize(XmlReader.Create(new StringReader(emptyXml)));

            Assert.AreEqual(0, subscriberList.Subscribers.Count);
        }

        [Test]
        public void Deserializing_NonEmptyList_ReturnsExpectedSubscribers()
        {
            var subscriberList = (SubscriberList)_serializer.Deserialize(XmlReader.Create(new StringReader(xmlList)));
            (subscriberList.Subscribers.OrderBy(s => s.CustomerId).Select(s => s.CustomerId).Aggregate(string.Empty, (accum, item) => accum + "," + item)).ShouldBe(",001,2095,88225,aef789956af61024d82ec270039601b91e06262a");
        }
    }
}