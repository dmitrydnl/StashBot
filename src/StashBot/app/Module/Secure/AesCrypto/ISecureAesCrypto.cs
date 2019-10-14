namespace StashBot.Module.Secure.AesCrypto
{
    internal interface ISecureAesCrypto
    {
        byte[] EncryptWithAes(string text);
        string DecryptWithAes(byte[] encrypted);
        string AesEncryptedDataToString(byte[] encrypted);
        byte[] AesStringToEncryptedData(string cipherText);
    }
}
