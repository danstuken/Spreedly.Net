namespace Spreedly.NetTests.Deserialize
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using NUnit.Framework;
    using Net.Entities;

    public class SubscriptionPlanListTests
    {
        #region Xml Strings
        
        private const string subscriptionPlanListXml =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<subscription-plans type=""array"">
  <subscription-plan>
    <amount type=""decimal"">24.0</amount>
    <charge-after-first-period type=""boolean"">false</charge-after-first-period>
    <charge-later-duration-quantity type=""integer"" nil=""true""></charge-later-duration-quantity>
    <charge-later-duration-units nil=""true""></charge-later-duration-units>
    <created-at type=""datetime"">2011-09-02T15:42:35Z</created-at>
    <currency-code>USD</currency-code>
    <description></description>
    <duration-quantity type=""integer"">3</duration-quantity>
    <duration-units>months</duration-units>
    <enabled type=""boolean"">false</enabled>
    <feature-level>example</feature-level>
    <force-recurring type=""boolean"">false</force-recurring>
    <id type=""integer"">14141</id>
    <minimum-needed-for-charge type=""decimal"">0.0</minimum-needed-for-charge>
    <name>Example Plan</name>
    <needs-to-be-renewed type=""boolean"">true</needs-to-be-renewed>
    <plan-type>regular</plan-type>
    <return-url>http://spreedly.com/sample-return</return-url>
    <updated-at type=""datetime"">2011-09-02T16:09:35Z</updated-at>
    <terms type=""string"">3 months</terms>
    <price type=""decimal"">24.0</price>
  </subscription-plan>
  <subscription-plan>
    <amount type=""decimal"">0.0</amount>
    <charge-after-first-period type=""boolean"">false</charge-after-first-period>
    <charge-later-duration-quantity type=""integer"" nil=""true""></charge-later-duration-quantity>
    <charge-later-duration-units nil=""true""></charge-later-duration-units>
    <created-at type=""datetime"">2011-09-02T16:01:17Z</created-at>
    <currency-code>USD</currency-code>
    <description>Super Intro Freebie plan</description>
    <duration-quantity type=""integer"">30</duration-quantity>
    <duration-units>days</duration-units>
    <enabled type=""boolean"">true</enabled>
    <feature-level>Free Introduction</feature-level>
    <force-recurring type=""boolean"">false</force-recurring>
    <id type=""integer"">14142</id>
    <minimum-needed-for-charge type=""decimal"">0.0</minimum-needed-for-charge>
    <name>Free Introduction</name>
    <needs-to-be-renewed type=""boolean"">true</needs-to-be-renewed>
    <plan-type>free_trial</plan-type>
    <return-url>madupurl</return-url>
    <updated-at type=""datetime"">2011-09-02T16:01:17Z</updated-at>
    <terms type=""string"">30 days</terms>
    <price type=""decimal"">0.0</price>
  </subscription-plan>
  <subscription-plan>
    <amount type=""decimal"">7.5</amount>
    <charge-after-first-period type=""boolean"">true</charge-after-first-period>
    <charge-later-duration-quantity type=""integer"">30</charge-later-duration-quantity>
    <charge-later-duration-units>days</charge-later-duration-units>
    <created-at type=""datetime"">2011-09-02T16:05:15Z</created-at>
    <currency-code>USD</currency-code>
    <description>10 protected client instructions</description>
    <duration-quantity type=""integer"">30</duration-quantity>
    <duration-units>days</duration-units>
    <enabled type=""boolean"">true</enabled>
    <feature-level>Magnum (10)</feature-level>
    <force-recurring type=""boolean"">true</force-recurring>
    <id type=""integer"">14143</id>
    <minimum-needed-for-charge type=""decimal"">0.0</minimum-needed-for-charge>
    <name>Magnum</name>
    <needs-to-be-renewed type=""boolean"">true</needs-to-be-renewed>
    <plan-type>regular</plan-type>
    <return-url>madupurl</return-url>
    <updated-at type=""datetime"">2011-09-06T16:36:56Z</updated-at>
    <terms type=""string"">30 days</terms>
    <price type=""decimal"">7.5</price>
  </subscription-plan>
  <subscription-plan>
    <amount type=""decimal"">22.5</amount>
    <charge-after-first-period type=""boolean"">false</charge-after-first-period>
    <charge-later-duration-quantity type=""integer"" nil=""true""></charge-later-duration-quantity>
    <charge-later-duration-units nil=""true""></charge-later-duration-units>
    <created-at type=""datetime"">2011-09-02T16:07:34Z</created-at>
    <currency-code>USD</currency-code>
    <description>50 Protected client instructions</description>
    <duration-quantity type=""integer"">30</duration-quantity>
    <duration-units>days</duration-units>
    <enabled type=""boolean"">true</enabled>
    <feature-level>Imperial (50)</feature-level>
    <force-recurring type=""boolean"">true</force-recurring>
    <id type=""integer"">14144</id>
    <minimum-needed-for-charge type=""decimal"">0.0</minimum-needed-for-charge>
    <name>Imperial</name>
    <needs-to-be-renewed type=""boolean"">true</needs-to-be-renewed>
    <plan-type>regular</plan-type>
    <return-url>madupurl</return-url>
    <updated-at type=""datetime"">2011-09-02T16:07:34Z</updated-at>
    <terms type=""string"">30 days</terms>
    <price type=""decimal"">22.5</price>
  </subscription-plan>
  <subscription-plan>
    <amount type=""decimal"">37.5</amount>
    <charge-after-first-period type=""boolean"">false</charge-after-first-period>
    <charge-later-duration-quantity type=""integer"" nil=""true""></charge-later-duration-quantity>
    <charge-later-duration-units nil=""true""></charge-later-duration-units>
    <created-at type=""datetime"">2011-09-02T16:09:22Z</created-at>
    <currency-code>USD</currency-code>
    <description>150 protected client instructions</description>
    <duration-quantity type=""integer"">30</duration-quantity>
    <duration-units>days</duration-units>
    <enabled type=""boolean"">true</enabled>
    <feature-level>Sovereign (150)</feature-level>
    <force-recurring type=""boolean"">true</force-recurring>
    <id type=""integer"">14145</id>
    <minimum-needed-for-charge type=""decimal"">0.0</minimum-needed-for-charge>
    <name>Sovereign</name>
    <needs-to-be-renewed type=""boolean"">true</needs-to-be-renewed>
    <plan-type>regular</plan-type>
    <return-url>madupurl</return-url>
    <updated-at type=""datetime"">2011-09-02T16:09:22Z</updated-at>
    <terms type=""string"">30 days</terms>
    <price type=""decimal"">37.5</price>
  </subscription-plan>
</subscription-plans>
";

        private const string emptySubscriptionPlanList = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<subscription-plans type=""array"">
</subscription-plans>
";

        #endregion

        private XmlSerializer _serializer;
        private XmlReader _listReader;
        private XmlReader _emptyListReader;

        [SetUp]
        public void Init()
        {
            _serializer = new XmlSerializer(typeof(SubscriptionPlanList));
            _listReader = XmlReader.Create(new StringReader(subscriptionPlanListXml));
            _emptyListReader = XmlReader.Create(new StringReader(emptySubscriptionPlanList));
        }

        [Test]
        public void DeserializingValidXml_ThrowsNoException()
        {
            var planList = (SubscriptionPlanList) _serializer.Deserialize(_listReader);

            Assert.True(true);
        }

        [Test]
        public void DeserializingNonEmptyXml_ReturnsNonNullList()
        {
            var planList = (SubscriptionPlanList)_serializer.Deserialize(_listReader);

            Assert.NotNull(planList);
        }

        [Test]
        public void DeserializingNonEmptyXml_ReturnsNonEmptyList()
        {
            var planList = (SubscriptionPlanList)_serializer.Deserialize(_listReader);

            Assert.Greater(planList.SubscriptionPlans.Count, 0);
        }

        [Test]
        public void DeserializingNonEmptyXml_ReturnsListWithPopulatedSubscriptionPlan()
        {
            var planList = (SubscriptionPlanList)_serializer.Deserialize(_listReader);

            Assert.IsNotEmpty(planList.SubscriptionPlans.First().Name);
        }

        [Test]
        public void DeserializingEmptyListXml_ReturnsNotNullList()
        {
            var planList = (SubscriptionPlanList)_serializer.Deserialize(_emptyListReader);

            Assert.NotNull(planList);
        }

        [Test]
        public void DeserializingEmptyListXml_ReturnsEmptyList()
        {
            var planList = (SubscriptionPlanList)_serializer.Deserialize(_emptyListReader);

            Assert.AreEqual(0,planList.SubscriptionPlans.Count);
        }
    }
}