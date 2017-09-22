using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DC.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     �ж�ָ�������Ƿ�Ϊ��ֵ����
        /// </summary>
        /// <param name="type">Ҫ��������</param>
        /// <returns>�Ƿ�����ֵ����</returns>
        public static bool IsNumeric(this Type type)
        {
            return type == typeof(Byte)
                || type == typeof(Int16)
                || type == typeof(Int32)
                || type == typeof(Int64)
                || type == typeof(SByte)
                || type == typeof(UInt16)
                || type == typeof(UInt32)
                || type == typeof(UInt64)
                || type == typeof(Decimal)
                || type == typeof(Double)
                || type == typeof(Single);
        }

        /// <summary>
        ///  ��ȡ��ԱԪ���ݵ�Description����������Ϣ
        /// </summary>
        /// <param name="member">��ԱԪ���ݶ���</param>
        /// <param name="inherit">�Ƿ�������Ա�ļ̳����Բ�����������</param>
        /// <returns>����Description����������Ϣ���粻�����򷵻س�Ա������</returns>
        public static string ToDescription(this MemberInfo member, bool inherit = false)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? null : desc.Description;
        }

        /// <summary>
        /// ���ָ��ָ�����ͳ�Ա���Ƿ����ָ����Attribute����
        /// </summary>
        /// <typeparam name="T">Ҫ����Attribute��������</typeparam>
        /// <param name="memberInfo">Ҫ�������ͳ�Ա</param>
        /// <param name="inherit">�Ƿ�Ӽ̳��в���</param>
        /// <returns>�Ƿ����</returns>
        public static bool AttributeExists<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Any(m => (m as T) != null);
        }

        /// <summary>
        /// �����ͳ�Ա��ȡָ��Attribute����
        /// </summary>
        /// <typeparam name="T">Attribute��������</typeparam>
        /// <param name="memberInfo">�������ͳ�Ա</param>
        /// <param name="inherit">�Ƿ�Ӽ̳��в���</param>
        /// <returns>���ڷ��ص�һ���������ڷ���null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).SingleOrDefault() as T;
        }

        /// <summary>
        /// �����ͳ�Ա��ȡָ��Attribute����
        /// </summary>
        /// <typeparam name="T">Attribute��������</typeparam>
        /// <param name="memberInfo">�������ͳ�Ա</param>
        /// <param name="inherit">�Ƿ�Ӽ̳��в���</param>
        /// <returns>���ڷ��ص�һ���������ڷ���null</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        /// <summary>
        /// �жϵ�ǰ���͵Ķ����ܷ�����ָ����������
        /// </summary>
        /// <param name="givenType">��������</param>
        /// <param name="genericType">��������</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                return false;
            }
            var interfaceTypes = givenType.GetInterfaces();
            if (interfaceTypes.Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            Type baseType = givenType.BaseType;
            if (baseType == null)
            {
                return false;
            }
            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}