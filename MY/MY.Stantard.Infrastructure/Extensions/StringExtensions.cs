using System;
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
    }
}