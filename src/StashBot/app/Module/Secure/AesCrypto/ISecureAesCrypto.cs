namespace StashBot.Module.Secure.AesCrypto
{
    internal interface ISecureAesCrypto
    {
        string EncryptWithAes(string secretMessage);
        string DecryptWithAes(string encryptedMessage);
    }
}
