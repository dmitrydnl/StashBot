using StashBot.Module.Secure.Hash;
using StashBot.Module.Secure.AesCrypto;
using StashBot.Module.Secure.AesHmacCrypto;

namespace StashBot.Module.Secure
{
    internal class SecureManager : ISecureManager
    {
        private readonly ISecureHash secureHash;
        private readonly ISecureAesCrypto secureAesCrypto;
        private readonly ISecureAesHmacCrypto secureAesHmacCrypto;

        internal SecureManager()
        {
            secureHash = new SecureHash();
            secureAesCrypto = new SecureAesCrypto();
            secureAesHmacCrypto = new SecureAesHmacCrypto();
        }

        public string CalculateHash(string text)
        {
            return secureHash.CalculateHash(text);
        }

        public bool CompareWithHash(string text, string hash)
        {
            return secureHash.CompareWithHash(text, hash);
        }

        public string EncryptWithAes(string secretMessage)
        {
            return secureAesCrypto.EncryptWithAes(secretMessage);
        }

        public string DecryptWithAes(string encryptedMessage)
        {
            return secureAesCrypto.DecryptWithAes(encryptedMessage);
        }

        public string EncryptWithAesHmac(string secretMessage, string password, byte[] nonSecretPayload = null)
        {
            return secureAesHmacCrypto.EncryptWithAesHmac(
                secretMessage,
                password,
                nonSecretPayload);
        }

        public string DecryptWithAesHmac(
            string encryptedMessage,
            string password,
            int nonSecretPayloadLength = 0)
        {
            return secureAesHmacCrypto.DecryptWithAesHmac(
                encryptedMessage,
                password,
                nonSecretPayloadLength);
        }
    }
}
