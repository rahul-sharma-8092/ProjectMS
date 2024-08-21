namespace ProjectMS.Service
{
    public interface IEncryptionService
    {
        string CreateHashBCrypt(string password);
        bool VerifyHashBCrypt(string password, string hashPassword);
        string Encryption(string plainText);
        string Decryption(string cipherText);
    }
}
