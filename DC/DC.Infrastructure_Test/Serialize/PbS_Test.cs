
using System;
using System.Collections.Generic;
using DC.Infrastructure.Serialize;
using ProtoBuf;
using DC.Infrastructure.Extensions;
namespace DC.Infrastrcture_Test.Serialize
{
    public class Pbs_Test
    {
        public static void Ser()
        {
            var per = new Person { Name = "好几款", Age = 25, Birthday = DateTime.Now };
            // List<Person> pers = new List<Person>{
            //     new Person { Name = "好几款", Age = 25, Birthday = DateTime.Now },
            //     new Person { Name = "ajfkdaf", Age = 28, Birthday = DateTime.Now },
            // };
            var str = SerializeUtil.ToProtobuf(per);
            // var str = SerializeUtil.ToProtobuf(pers);
            Console.WriteLine("protobuf ser result:" + str);
            // var lstDes = SerializeUtil.FromProtobuf<List<Person>>(str);
            // Console.WriteLine("protubuf des per cnt:" + (lstDes.HasItems() ? lstDes.Count : 0));
            var newPer = SerializeUtil.FromProtobuf<Person>(str);
            Console.WriteLine("protobuf des result:" + newPer.ToString());

        }
    }

    [ProtoContract]
    public class Person
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int Age { get; set; }
        [ProtoMember(3)]
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return string.Format("name:{0},age:{1},birth:{2}", Name, Age, Birthday.ToString("yyy-MM-dd HH:mm:ss"));
        }
    }
}