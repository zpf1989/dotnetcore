using System;
using System.Security.Cryptography;
using System.Text;

namespace MY.Stantard.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmptyByString(this object obj)
        {
            return obj == null || string.IsNullOrWhiteSpace(obj.ToString());
        }

        public static string ToStringIfNull(this object obj, string defaultStr = "")
        {
            return obj == null ? (defaultStr == null ? "" : defaultStr) : obj.ToString();
        }

        //32ŒªMD5º”√‹
        public static string MD5(this string str)
        {
            if (str.IsEmptyByString())
            {
                return "";
            }
            var md5Provider = new MD5CryptoServiceProvider();
            byte[] data = md5Provider.ComputeHash(Encoding.Default.GetBytes(str));
            var sb = new StringBuilder();
            for (int idx = 0; idx < data.Length; idx++)
            {
                sb.Append(data[idx].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}