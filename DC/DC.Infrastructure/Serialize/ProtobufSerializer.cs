using System;
using System.IO;
using DC.Infrastructure.Extensions;

namespace DC.Infrastructure.Serialize
{
    internal class ProtobufSerializer : ISerializer
    {
        T ISerializer.Deserialize<T>(string str)
        {
            if (str.IsEmptyByString())
            {
                return default(T);
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            using (var ms = new MemoryStream(bytes))
            {
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }

        string ISerializer.Serialize(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
            }
        }
    }
}