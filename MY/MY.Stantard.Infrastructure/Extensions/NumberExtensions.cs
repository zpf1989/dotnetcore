using System;
using System.Collections.Generic;

namespace MY.Stantard.Infrastructure.Extensions
{
    public static class NumberExtensions
    {
        public static decimal ToDecimalIfNull(this object obj, decimal defaultVal = 0)
        {
            if (obj == null)
            {
                return defaultVal;
            }
            decimal val = 0;
            decimal.TryParse(obj.ToString(), out val);
            return val;
        }

        public static int ToIntIfNull(this object obj, int defaultVal = 0)
        {
            if (obj == null)
            {
                return defaultVal;
            }
            int val = 0;
            int.TryParse(obj.ToString(), out val);
            return val;
        }

        public static long ToLongIfNull(this object obj, long defaultVal = 0)
        {
            if (obj == null)
            {
                return defaultVal;
            }
            long val = 0;
            long.TryParse(obj.ToString(), out val);
            return val;
        }

        public static bool IsNumber(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            decimal val = 0;
            return decimal.TryParse(obj.ToString(), out val);
        }
    }
}