using System;
using System.Text;
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

        public string CalculateHash(string input)
        {
            return secureHash.CalculateHash(input);
        }

        public bool CompareWithHash(string input, string hash)
        {
            return secureHash.CompareWithHash(input, hash);
        }

        public byte[] EncryptWithAes(string text)
        {
            return secureAesCrypto.Encrypt(text);
        }

        public string DecryptWithAes(byte[] encrypted)
        {
            return secureAesCrypto.Decrypt(encrypted);
        }

        public string AesEncryptedDataToString(byte[] encrypted)
        {
            return secureAesCrypto.EncryptedDataToString(encrypted);
        }

        public byte[] AesStringToEncryptedData(string cipherText)
        {
            return secureAesCrypto.StringToEncryptedData(cipherText);
        }

        public RSACryptoServiceProvider CreateRsaCryptoService()
        {
            return secureRsaCrypto.CreateCryptoService();
        }

        public string RsaCryptoServiceToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters)
        {
            return secureRsaCrypto.ToXmlString(csp, includePrivateParameters);
        }

        public RSACryptoServiceProvider RsaCryptoServiceFromXmlString(string xmlString)
        {
            return secureRsaCrypto.FromXmlString(xmlString);
        }
    }
}
