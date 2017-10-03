using System;
namespace DC.Infrastructure.Serialize
{
    internal interface ISerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string str);
    }
}