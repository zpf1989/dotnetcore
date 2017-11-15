using System;
using DC.Infrastrcture_Test.MongoDB;
using DC.Infrastrcture_Test.Serialize;

namespace DC.Infrastructure_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pbs_Test.Ser();
            //TestMongoDB();
            //Encryption_Test.Des_Test("");
            //Encryption_Test.Des_Test("答复");
            Encryption_Test.MD5();
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }

        static void TestMongoDB()
        {
            new MongoDB_Test().TestClient("localhost",27017);
        }
    }
}
