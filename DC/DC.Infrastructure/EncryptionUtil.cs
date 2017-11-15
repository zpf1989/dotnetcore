using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DC.Infrastructure
{
    /// <summary>
    /// 加解密工具类
    /// </summary>
    public class EncryptionUtil
    {
        #region DES
        //默认密钥向量  
        private static readonly byte[] DES_Keys = { 1, 9, 8, 9, 1, 0, 0, 3 };
        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="strSrc"></param>
        /// <param name="strKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string strSrc, string strKey)
        {
            if (strSrc == null)
            {
                return strSrc;
            }
            if (strKey.Length != 8)
            {
                throw new ArgumentException("密钥长度必须为8位", "strKey");
            }
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(strKey);
                byte[] rgbIV = DES_Keys;
                byte[] srcBytes = Encoding.UTF8.GetBytes(strSrc);

                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        cs.Write(srcBytes, 0, srcBytes.Length);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                //TODO：此处应有日志记录，AOP最好
                return strSrc;
            }
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strSrc"></param>
        /// <param name="strKey">加密密钥,要求为8位</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string strSrc, string strKey)
        {
            if (strSrc == null)
            {
                return strSrc;
            }
            if (strKey.Length != 8)
            {
                throw new ArgumentException("密钥长度必须为8位", "strKey");
            }
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(strKey);
                var rgbIV = DES_Keys;
                var srcBytes = Convert.FromBase64String(strSrc);

                var dCSP = new DESCryptoServiceProvider();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, dCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        cs.Write(srcBytes, 0, srcBytes.Length);
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                //TODO：此处应有日志记录，AOP最好
                return strSrc;
            }
        }
        #endregion

        #region MD5
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strSrc"></param>
        /// <returns></returns>
        public static string EncryptMD5(string strSrc)
        {
            if (strSrc == null)
            {
                return null;
            }
            try
            {
                var srcBytes = Encoding.UTF8.GetBytes(strSrc); 
                 var md5CSP = new MD5CryptoServiceProvider();
                return Encoding.UTF8.GetString(md5CSP.ComputeHash(srcBytes));
            }
            catch (Exception ex)
            {
                //TODO异常处理

                return strSrc;
            }
        }
        #endregion
    }
}
