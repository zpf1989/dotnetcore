using DC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DC.Infrastructure_Test
{
    public class Encryption_Test
    {
        public static void Des_Test(string src)
        {
            var key = "88899900";
            var enc = EncryptionUtil.EncryptDES(src, key);
            Debug.WriteLine("src:{0},key:{1},src_enc:{2},src_dec:{3}", src, key, enc, EncryptionUtil.DecryptDES(enc, key));
        }

        public static void MD5()
        {
            var str = "";
            Debug.WriteLine("src:{0},md5:{1}", str, EncryptionUtil.EncryptMD5(str));

            str = "asdf撒地方";
            Debug.WriteLine("src:{0},md5:{1}", str, EncryptionUtil.EncryptMD5(str));
        }
    }
}
