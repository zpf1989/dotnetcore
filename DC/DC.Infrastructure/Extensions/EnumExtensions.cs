using System;
using System.Runtime.Serialization;

namespace DC.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// ��ȡö�ٵ�˵��
        /// </summary>
        /// <typeparam name="T">ö��</typeparam>
        /// <param name="n">ö��int?ֵ</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int? n) where T : struct
        {
            if (n.HasValue)
            {
                return n.Value.GetDescription<T>();
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡö�ٵ�˵��
        /// </summary>
        /// <typeparam name="T">ö��</typeparam>
        /// <param name="n">ö��intֵ</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int n) where T : struct
        {
            try
            {
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = Enum.GetName(enumType, n);
                    EnumMemberAttribute customAttribute = (
                        EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
                    return ((customAttribute == null) ? name : customAttribute.Value);
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡö�ٵ�˵��
        /// </summary>
        /// <typeparam name="T">ö��</typeparam>
        /// <param name="n">ö��long?ֵ</param>
        /// <returns></returns>
        public static string GetDescription<T>(this long? n) where T : struct
        {
            if (n.HasValue)
            {
                return n.Value.GetDescription<T>();
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡö�ٵ�˵��
        /// </summary>
        /// <typeparam name="T">ö��</typeparam>
        /// <param name="n">ö��longֵ</param>
        /// <returns></returns>
        public static string GetDescription<T>(this long n) where T : struct
        {
            try
            {
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = Enum.GetName(enumType, n);
                    EnumMemberAttribute customAttribute =
                        (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
                    return ((customAttribute == null) ? name : customAttribute.Value);
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡö�ٵ�˵��
        /// </summary>
        /// <param name="o">ö��ֵ</param>
        /// <returns></returns>
        public static string GetDescription(this Enum o)
        {
            Type enumType = o.GetType();
            string name = Enum.GetName(enumType, o);
            EnumMemberAttribute customAttribute =
                (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
            return ((customAttribute == null) ? name : customAttribute.Value);
        }

        public static int ToInt(this Enum o)
        {
            return Convert.ToInt32(o);
        }

        public static long ToLong(this Enum o)
        {
            return Convert.ToInt64(o);
        }

        public static string GetEnumName<T>(this Enum o)
        {
            Type enumType = typeof(T);
            if (enumType.IsEnum)
            {
                return o.GetEnumName(enumType);
            }
            return "";
        }

        public static string GetEnumName(this Enum o, Type enumType)
        {
            try
            {
                string name = Enum.GetName(enumType, o);
                return name;
            }
            catch
            {

            }
            return null;
        }

        public static T ToEnum<T>(this int n) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), n);
        }

        public static T ToEnum<T>(this string s) where T : struct
        {
            return (T)Enum.Parse(typeof(T), s, false);
        }
    }
}