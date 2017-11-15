using System;
using System.Diagnostics;

namespace DC.Infrastructure_Test
{
    public class TraceUtil
    {
        private static string GetCodeInfo(StackTrace st)
        {
            if (st == null || st.FrameCount < 1)
            {
                return "";
            }
            var sf = st.GetFrame(0);
            return string.Format("File:{0},Method:{1}" + Environment.NewLine, sf.GetFileName(), sf.GetMethod().Name);
        }
        public static void WriteLine(string msg, StackTrace st = null)
        {
            System.Diagnostics.Debug.WriteLine(GetCodeInfo(st) + "\tOutput：" + msg);
        }

        public static void TraceWrapper(Action body, StackTrace st = null)
        {
            WriteLine("start", st);
            try
            {
                body();
            }
            catch (Exception ex)
            {
                WriteLine("err," + ex.Message, st);
            }
            finally
            {
                WriteLine("stop", st);
            }
        }
    }
}