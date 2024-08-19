using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;

namespace ProjectMS.Common
{
    public static class Encryptions
    {
        private static byte[] _Aeskey;
        private static byte[] _AesIV;

        static Encryptions()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            _Aeskey = Convert.FromBase64String(configuration["Encryption:AesKey"] ?? "");
            _AesIV = Convert.FromBase64String(configuration["Encryption:AesIV"] ?? "");

            //string AesKey = "4ejR3667Wuzc5NN3ljV6mxwfrcCKtPNa0fhrJ7Voyps=";
            //string AesIV = "BwzsNqTDYyRQtycvscPhDQ==";
        }

        #region Password Hashing - BCrypt
        public static string CreateHashBCrypt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10, BCrypt.Net.SaltRevision.Revision2X);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }

        public static bool VerifyHashBCrypt(string password, string hashPassword)
        {
            if (string.IsNullOrEmpty(hashPassword))
            {
                return password.Equals(hashPassword);
            }
            else
            {
                return BCrypt.Net.BCrypt.Verify(password, hashPassword);
            }
        }
        #endregion

        #region Encryption - AES (Advanced Encryption Standard)
        public static string Encryption(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _Aeskey;
                aesAlg.IV = _AesIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public static string Decryption(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _Aeskey;
                aesAlg.IV = _AesIV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] GenerateRandomKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                string keyss = Convert.ToBase64String(aesAlg.Key);
                return aesAlg.Key;
            }
        }

        private static byte[] GenerateRandomIV()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                string IV = Convert.ToBase64String(aesAlg.Key);
                return aesAlg.IV;
            }
        }
        #endregion
    }
}