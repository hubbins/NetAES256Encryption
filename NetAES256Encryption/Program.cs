using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetAES256Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "IOxuKVX0sQwbMSYAoSM3X2ZsuWxHIaFXDBJkAHfK70AvvCT8cFaY8V1LZvOuANcHU+Z11ZZLj9vpdCnaNDL9dAmA/WW2xm93BUuGHCRnLLEV3qvCNljRqh3ab2LvAYidJwH1HvZqB3LjkVg53MwCkw==";
            var password = "JDHQINFAFB12JSKD";
            var iv = "1234567890123456";

            var aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = UTF8Encoding.UTF8.GetBytes(password);
            byte[] ivBytes = UTF8Encoding.UTF8.GetBytes(iv);
            aes.IV = ivBytes;

            var decryptor = aes.CreateDecryptor();
            byte[] messageBytes = Convert.FromBase64String(message);
            byte[] resultBytes = decryptor.TransformFinalBlock(messageBytes, 0, messageBytes.Length);

            var s = UTF8Encoding.UTF8.GetString(resultBytes);
            Console.WriteLine(s);
        }


    }
}
