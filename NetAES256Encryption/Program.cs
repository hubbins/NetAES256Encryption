using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace NetAES256Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "Z2t6a25yZW1mc2xvYWt4cI2d5CaTVi/z+rshVHluNaQGmqimzRgmufzuNpTrRMbzCkSRucKyYrOIqV4qWXs7fkkwpUdB3nBQtj8nNmVRd5d0mZDvphRCsLY4nsGXCifw1fQGHY61oeJ5LEPD0mwHfTyqynbQoFrQZad0xftjmKE=";
            var key = "JDHQINFAFB12JSKDJDHQINFAFB12JSKD";

            var result = Decrypt(message, key);
            Console.WriteLine(result);
        }

        // assumes 16 bytes initialization vector, followed by encrypted data
        static string Decrypt(string encryptedText, string key)
        {
            if (key.Length != 32)
                throw new ArgumentException("Key must be 32 characters");

            var encryptedTextBytes = Convert.FromBase64String(encryptedText);
            var aes = new AesManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = UTF8Encoding.UTF8.GetBytes(key),
                IV = encryptedTextBytes.Take(16).ToArray()
            };

            var encryptedData = encryptedTextBytes.Skip(16).ToArray();
            var decryptedBytes = aes.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return UTF8Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
