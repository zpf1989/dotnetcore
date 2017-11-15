using System;
using System.Diagnostics;
using DC.Infrastructure_Test;
using DC.MongoDB;
namespace DC.Infrastrcture_Test.MongoDB
{
    public class MongoDB_Test
    {
        public void TestClient(string ip, int? port)
        { 
            var st = new StackTrace(new StackFrame(true));
            TraceUtil.TraceWrapper(() =>
            {
                var mgUtil = new MongoDbUtil(ip, port);
                TraceUtil.WriteLine(string.Format("mgUtil.MgClient is null:{0}", mgUtil.MgClient == null), st);
                TraceUtil.WriteLine(string.Format("mgUtil.MgConnectString is:{0}", mgUtil.MgConnectString), st);
            }, st);
        }

        public void TestDb(string ip, int? port, string dbName)
        {

        }
    }
}