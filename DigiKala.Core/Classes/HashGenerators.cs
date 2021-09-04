using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DigiKala.Core.Classes
{
    public static class HashGenerators
    {
        public static string MD5Encrypt(this string plaintxt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] plainBytes = Encoding.Default.GetBytes(plaintxt);
            byte[] cipherBytes = md5.ComputeHash(plainBytes);
            return BitConverter.ToString(cipherBytes);
        }

    }
}
