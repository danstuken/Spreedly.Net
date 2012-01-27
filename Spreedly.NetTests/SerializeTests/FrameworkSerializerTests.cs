using Spreedly.Xml;

namespace Spreedly.NetTests.SerializeTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class FrameworkSerializerTests
    {
         
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializingNullObject_ThrowsArgumentNullException()
        {
            var serialize = new FrameworkSerializer();

            serialize.Serialize(null);
        }
    }
}