namespace StashBot.Module.Secure.AesHmacCrypto
{
    internal interface ISecureAesHmacCrypto
    {
        string EncryptWithAesHmac(string secretMessage, string password, byte[] nonSecretPayload = null);
        string DecryptWithAesHmac(string encryptedMessage, string password, int nonSecretPayloadLength = 0);
    }
}
