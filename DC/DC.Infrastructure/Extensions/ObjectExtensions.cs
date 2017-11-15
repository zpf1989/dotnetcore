using System;
using DC.Infrastructure.Serialize;
namespace DC.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object o)
        {
            if(o==null)
            {
                return null;
            }
            return SerializeUtil.ToJson(o);
        }
    }
}