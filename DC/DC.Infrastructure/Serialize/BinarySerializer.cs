using System;
namespace DC.Infrastructure.Serialize
{
    internal class BinarySerializer : ISerializer
    {
        T ISerializer.Deserialize<T>(string str)
        {
            throw new NotImplementedException();
        }

        string ISerializer.Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}