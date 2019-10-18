namespace StashBot.Module.Secure.AesCrypto
{
    internal interface ISecureAesCrypto
    {
        byte[] EncryptWithAes(string text);
        string DecryptWithAes(byte[] encryptedData);
        string AesEncryptedDataToString(byte[] encryptedData);
        byte[] AesStringToEncryptedData(string encryptedText);
    }
}
