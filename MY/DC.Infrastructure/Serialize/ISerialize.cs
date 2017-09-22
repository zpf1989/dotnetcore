using System;
namespace MY.Stantard.Infrastructure.Serialize
{
    public interface ISerialize
    {
        string Serialize(object obj);
        T Deserialize<T>(string str);
    }
}