using StashBot.Module.Secure.Hash;
using StashBot.Module.Secure.AesCrypto;
using StashBot.Module.Secure.RsaCrypto;
using System.Security.Cryptography;

namespace StashBot.Module.Secure
{
    internal class SecureManager : ISecureManager
    {
        private readonly ISecureHash secureHash;
        private readonly ISecureAesCrypto secureAesCrypto;
        private readonly ISecureRsaCrypto secureRsaCrypto;

        internal SecureManager()
        {
            secureHash = new SecureHash();
            secureAesCrypto = new SecureAesCrypto();
            secureRsaCrypto = new SecureRsaCrypto();
        }

        public string CalculateHash(string text)
        {
            return secureHash.CalculateHash(text);
        }

        public bool CompareWithHash(string text, string hash)
        {
            return secureHash.CompareWithHash(text, hash);
        }

        public byte[] EncryptWithAes(string text)
        {
            return secureAesCrypto.EncryptWithAes(text);
        }

        public string DecryptWithAes(byte[] encryptedData)
        {
            return secureAesCrypto.DecryptWithAes(encryptedData);
        }

        public string AesEncryptedDataToString(byte[] encryptedData)
        {
            return secureAesCrypto.AesEncryptedDataToString(encryptedData);
        }

        public byte[] AesStringToEncryptedData(string encryptedText)
        {
            return secureAesCrypto.AesStringToEncryptedData(encryptedText);
        }

        public RSACryptoServiceProvider CreateRsaCryptoService()
        {
            return secureRsaCrypto.CreateRsaCryptoService();
        }

        public string EncryptWithRsa(RSACryptoServiceProvider csp, string text)
        {
            return secureRsaCrypto.EncryptWithRsa(csp, text);
        }

        public string DecryptWithRsa(RSACryptoServiceProvider csp, string encryptedText)
        {
            return secureRsaCrypto.DecryptWithRsa(csp, encryptedText);
        }

        public string RsaCryptoServiceToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters)
        {
            return secureRsaCrypto.RsaCryptoServiceToXmlString(csp, includePrivateParameters);
        }

        public RSACryptoServiceProvider RsaCryptoServiceFromXmlString(string xmlString)
        {
            return secureRsaCrypto.RsaCryptoServiceFromXmlString(xmlString);
        }
    }
}
