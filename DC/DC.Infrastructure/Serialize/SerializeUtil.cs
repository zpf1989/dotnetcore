using System;
using DC.Infrastructure.Extensions;

namespace DC.Infrastructure.Serialize
{
    public class SerializeUtil
    {
        public static string ToJson(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            ISerializer serializer = new JsonSerializer();
            return serializer.Serialize(obj);
        }

        public static TResult FromJson<TResult>(string json)
        {
            if (json.IsEmptyByString())
            {
                return default(TResult);
            }
            ISerializer serializer = new JsonSerializer();
            return serializer.Deserialize<TResult>(json);
        }

        public static string ToXml(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            ISerializer serializer = new XmlSerializer();
            return serializer.Serialize(obj);
        }

        public static TResult FromXml<TResult>(string xml)
        {
            if (xml.IsEmptyByString())
            {
                return default(TResult);
            }
            ISerializer serializer = new XmlSerializer();
            return serializer.Deserialize<TResult>(xml);
        }

        public static string ToBinary(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            ISerializer serializer = new BinarySerializer();
            return serializer.Serialize(obj);
        }

        public static TResult FromBinary<TResult>(string str)
        {
            if (str.IsEmptyByString())
            {
                return default(TResult);
            }
            ISerializer serializer = new BinarySerializer();
            return serializer.Deserialize<TResult>(str);
        }

        public static string ToProtobuf(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            ISerializer serializer = new ProtobufSerializer();
            return serializer.Serialize(obj);
        }

        public static TResult FromProtobuf<TResult>(string str)
        {
            if (str.IsEmptyByString())
            {
                return default(TResult);
            }
            ISerializer serializer = new ProtobufSerializer();
            return serializer.Deserialize<TResult>(str);
        }
    }
}