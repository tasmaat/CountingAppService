using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Login
{
    class HashCoding
    {

        private const string cHashCheckKey = "fdgsdfg$#^%FdfhHHHdhdh";

        /// <summary>
        /// Функция расчета HASH для введенного пароля
        /// </summary>
        /// <param name="hashCode">Введенный пароль</param>
        /// <param name="hashAlgorithm">Алгоритм шифрования пароля</param>
        /// <param name="hashKey">Ключ для шифрования пароля в массиве байт</param>
        /// <returns></returns>
        public string ComputeHash(string hashCode)
        {
            Byte[] hashKey = Encoding.UTF8.GetBytes(cHashCheckKey);
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            Byte[] hashBytes = Encoding.UTF8.GetBytes(hashCode);
            Byte[] hashedInput = new Byte[hashKey.Length + hashBytes.Length];
            hashKey.CopyTo(hashedInput, 0);
            hashBytes.CopyTo(hashedInput, hashKey.Length);
            HMACSHA256 hash = new HMACSHA256(hashKey);
            Byte[] hashedBytes = hash.ComputeHash(hashBytes);
            //Byte[] hashedBytes = HashHMAC(hashKey,hashBytes);

            //Byte[] hashedBytes = hashAlgorithm.ComputeHash(hashedInput);
            //string strUTF = Encoding.UTF8.GetString(hashedBytes).ToLower().Replace("-", String.Empty);
            return BitConverter.ToString(hashedBytes).ToLower().Replace("-", String.Empty);
        }

        
        private string ComputeHash(string hashCode, HashAlgorithm hashAlgorithm)
        {
            Byte[] hashBytes = Encoding.UTF8.GetBytes(hashCode);
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(hashBytes);
            return BitConverter.ToString(hashBytes);
        }



        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
        
    }
}
