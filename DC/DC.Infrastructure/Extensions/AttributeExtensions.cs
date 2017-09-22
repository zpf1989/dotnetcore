using System;
using System.Linq;

namespace DC.Infrastructure.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// 获取类型 Attribute
        /// </summary>
        /// <typeparam name="TAttr"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="type"></param>
        /// <param name="valSelector"></param>
        /// <returns></returns>
        public static TVal GetAttributeValue<TAttr, TVal>(this Type type, Func<TAttr, TVal> valSelector) where TAttr : Attribute
        {
            if (valSelector == null)
            {
                return default(TVal);
            }

            var att = type
                .GetCustomAttributes(typeof(TAttr), true)
                .FirstOrDefault()
                as TAttr;
            if (att != null)
            {
                return valSelector(att);
            }
            return default(TVal);
        }
    }
}