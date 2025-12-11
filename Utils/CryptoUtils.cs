using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class CryptoUtils
    {
        public static string Hash(string value)
        {
            var md5 = MD5.Create();
            byte[] data = SHA256.HashData(md5.ComputeHash(Encoding.UTF8.GetBytes(value)));

            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();

        }
        public static bool VerifyHash(string value, string hash)
        {
            string hashOfInput = Hash(value);
            return StringUtils.CompareStrings(hash, hashOfInput);

        }
    }
}
