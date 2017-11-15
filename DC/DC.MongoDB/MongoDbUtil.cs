using System;
using MongoDB.Bson;
using MongoDB.Driver;
using DC.Infrastructure.Extensions;
namespace DC.MongoDB
{
    public class MongoDbUtil
    {
        #region fields
        private IMongoClient _mgClient;
        private IMongoDatabase _mgDb;
        private MongoUrl _mgUrl;
        private string _mgIp;
        private int? _mgPort;
        private const int _mgDefaultPort = 27017;
        #endregion fields
        #region properties
        public IMongoClient MgClient
        {
            get
            {
                return _mgClient;
            }
        }
        public string MgConnectString
        {
            get
            {
                return string.Format("mongodb://{0}:{1}", _mgIp.ToStringIfNull(), _mgPort.ToStringIfNull(_mgDefaultPort.ToString()));
            }
        }
        public MongoUrl MgUrl
        {
            get
            {
                return _mgUrl;
            }
        }

        public string MgIp { get { return _mgIp; } }
        public int? MgPort { get { return _mgPort; } }
        #endregion properties

        #region  .ctor
        private MongoDbUtil()
        {

        }
        public MongoDbUtil(string mgIp, int? mgPort = _mgDefaultPort) : this()
        {
            if (mgIp.IsEmptyByString())
            {
                //TODO：参数名称改进？？新特性
                throw new ArgumentNullException("mgIp", "MongoDB服务地址不能为空");
            }
            if (mgPort.IsEmptyByString())
            {
                //TODO：参数名称改进？？新特性
                throw new ArgumentNullException("mgPort", "MongoDB服务端口号不能为空");
            }
            _mgIp = mgIp;
            if (mgPort.HasValue)
            {
                _mgPort = mgPort;
            }

            _mgClient = new MongoClient(MgConnectString);
        }

        public MongoDbUtil(MongoUrl mgUrl) : this()
        {
            if (mgUrl == null)
            {
                //TODO：参数名称改进？？新特性
                throw new ArgumentNullException("mgUrl", "MongoDB连接Url不能为空");
            }
            _mgClient = new MongoClient(mgUrl);
            _mgUrl = mgUrl;
        }
        #endregion .ctor
        public void Test()
        {

        }
    }
}
