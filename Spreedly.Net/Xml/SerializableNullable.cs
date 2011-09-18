namespace Spreedly.Net.Xml
{
    using System;
    using System.ComponentModel;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public struct SerializableNullable<T> : IXmlSerializable, IEquatable<SerializableNullable<T>> where T : struct
    {
        private T _value;
        private bool _hasValue;

        private SerializableNullable(T value)
        {
            _hasValue = true;
            _value = value;
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public T Value
        {
            get { return _value; }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.GetAttribute("nil") == "true")
            {
                ReadNullValue();
                return;
            }
            ReadNonNullValue(reader);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if(HasValue)
                writer.WriteValue(_value);
        }

        public bool ShouldSerialize()
        {
            return HasValue;
        }

        public string ToString()
        {
            if (HasValue)
                return Value.ToString();
            return string.Empty;
        }

        private void ReadNullValue()
        {
            _hasValue = false;
        }

        private void ReadNonNullValue(XmlReader reader)
        {
            reader.ReadStartElement();
            var s = reader.ReadString();
            _value = GetValue(s);
            reader.ReadEndElement();
            _hasValue = true;
        }

        private T GetValue(string readValue)
        {
            var descriptor = TypeDescriptor.GetConverter(typeof (T));
            if (descriptor.CanConvertFrom(typeof(string)))
                return (T)descriptor.ConvertFromInvariantString(readValue);
            return default(T);
        }

        public static implicit operator SerializableNullable<T>(T value)
        {
            return new SerializableNullable<T>(value);
        }

        public bool Equals(SerializableNullable<T> other)
        {
            return HasValue == other.HasValue && Value.Equals(other.Value);
        }
    }
}