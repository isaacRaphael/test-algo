using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace testalgo
{
    class Program
    {
        static void Main(string[] args)
        {
            string testHash = "this is a test for hashing";
           
            SHA512 hashSource = SHA512.Create();
                 
            // Sha 512 returns 512 bits (8 bits/byte, 64 bytes) for the hash
            byte[] hash = hashSource.ComputeHash(Encoding.UTF8.GetBytes(testHash));


            //Console.WriteLine(TimeFormat.GetReadableTime(3599));
            var hex = BitConverter.ToString(hash).Replace("-", "");
            Console.WriteLine(hex);


            //Symmetric Encryption
            Aes cipherSource = CreateCipher();
            var IV = Convert.ToBase64String(cipherSource.IV);

            //Create the encryptor, convert to byte
            ICryptoTransform cryptoTransform = cipherSource.CreateEncryptor();
            var testEncryptString = "emeka and tolumide apple apple";
            byte[] plaintext = Encoding.UTF8.GetBytes(testEncryptString);
            byte[] cipherText = cryptoTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

            var CipherTextToDisplay = Convert.ToBase64String(cipherText);
            Console.WriteLine("\n\n");
            Console.WriteLine($"Cipher : {CipherTextToDisplay}");
            Console.WriteLine($"IV : {IV}");


            var ct = Console.ReadLine();
            var iv = Console.ReadLine();

            Console.WriteLine(ct);
            Console.WriteLine(iv);


            cipherSource.IV = Convert.FromBase64String(iv);

            ICryptoTransform cryptoTransform2 = cipherSource.CreateDecryptor();
            byte[] DcipherText = Convert.FromBase64String(ct);
            byte[] result = cryptoTransform2.TransformFinalBlock(DcipherText, 0, DcipherText.Length );

            Console.WriteLine("\n\n");
            Console.WriteLine("Original text : " + Encoding.UTF8.GetString(result));

        }

        private static Aes CreateCipher()
        {
            Aes cipher = Aes.Create();
            cipher.Padding = PaddingMode.ISO10126;

            //cipher.Padding = PaddingMode.Zeros;
            //cipher.Mode = CipherMoode.ECB;

            //Create() Makes a new key eash time,
            cipher.Key = Encoding.UTF8.GetBytes("NURGTHDVBTIOKGNMD78GJIOJMNDOPHMJ");
            return cipher;
        }

    }


    public static class TimeFormat
    {
        public static string GetReadableTime(int seconds)
        {

            var resultList = new List<string>();
            var count = 0;
            while (seconds - 3600 >= 0)
            {
                seconds = seconds - 3600;
                count++;
            }
            resultList.Add(count.ToString("00"));
           
            var count2 = 0;
            while (seconds - 60 >= 0)
            { 
                seconds = seconds - 60;
                count2++;
            }
            resultList.Add(count2.ToString("00"));
            resultList.Add(seconds.ToString("00"));
            return string.Join(":", resultList);
        }
    }
}
