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
            var message = "eWJucG9kbXF6aXBvb25wZAtrMD1WxbkMyEvxC/7zdSbjD4BS+omeJzgDId6E4lfCMzQDclf3ANpwxGI3MtU+SK5q+xlegiVgr+9lGajX1TvITSj3CP8ea1Zps2nsDdOInEIV/nvv9tSSasbjBmfyBCoJ+62KspMrcDQr5gwjpBc=";
            var key = "JDHQINFAFB12JSKD";

            var result = Decrypt(message, key);
            Console.WriteLine(result);
        }

        // assumes 16 bytes initialization vector, followed by encrypted data
        static string Decrypt(string encryptedText, string key)
        {
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
