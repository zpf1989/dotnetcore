using System;
namespace DC.Infrastructure.Serialize
{
    public class BinarySerialize : ISerialize
    {
        public T Deserialize<T>(string str)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}