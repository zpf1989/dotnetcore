using System;
using System.Data;

namespace MY.Stantard.Infrastructure.Extensions
{
    public static class DataExtensions
    {
        public static bool HasRow(this DataTable dt)
        {
            return dt != null && dt.Rows != null && dt.Rows.Count > 0;
        }
        public static bool HasTable(this DataSet ds)
        {
            return ds != null && ds.Tables != null && ds.Tables.Count > 0;
        }
        /*
        ds的第一个Table是否有行
         */
        public static bool HasRow(this DataSet ds)
        {
            return ds.HasTable() && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0;
        }
    }
}