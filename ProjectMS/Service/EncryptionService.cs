using System.Security.Cryptography;

namespace ProjectMS.Service
{
    public class EncryptionService : IEncryptionService
    {
        #region Constructor
        private readonly IConfiguration configuration;
        private byte[] _Aeskey;
        private byte[] _AesIV;

        public EncryptionService(IConfiguration _configuration)
        {
            configuration = _configuration;
            _Aeskey = Convert.FromBase64String(configuration["Encryption:AesKey"] ?? "");
            _AesIV = Convert.FromBase64String(configuration["Encryption:AesIV"] ?? "");
        }
        #endregion

        #region Password Hashing - BCrypt
        public string CreateHashBCrypt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10, BCrypt.Net.SaltRevision.Revision2X);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }

        public bool VerifyHashBCrypt(string password, string hashPassword)
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
        public string Encryption(string plainText)
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

        public string Decryption(string cipherText)
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
        #endregion

    }
}