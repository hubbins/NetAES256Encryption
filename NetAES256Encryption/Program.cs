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
            var message = "ZWtsd2t0d3FlZmNqc3ZyZqbv19g+cRgIqxwiC1KhQ3xKeGclGVNdOtOjV/ebhRpzchPBrWVB8eqgGrxPORcRheqKa3vcKeyMSkaWybX4qFVQJ55HhfjrNEY+i/FO1DHduTxGyM0hCjwFi2MRLG1rFWHa25rz33i1l6IP1/Az2Lk=";
            var key = "JDHQINFAFB12JSKDFOWOW023@3432FKDSF";

            var result = Decrypt(message, key);
            Console.WriteLine(result);
        }

        // assumes 16 bytes initialization vector, followed by encrypted data
        static string Decrypt(string encryptedText, string key)
        {
            using var hash = SHA256.Create();
            var encryptedTextBytes = Convert.FromBase64String(encryptedText);
            var aes = new AesManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = hash.ComputeHash(Encoding.UTF8.GetBytes(key)),
                IV = encryptedTextBytes.Take(16).ToArray()
            };

            var encryptedData = encryptedTextBytes.Skip(16).ToArray();
            var decryptedBytes = aes.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return UTF8Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
