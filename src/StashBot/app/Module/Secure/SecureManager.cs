using System;
using System.Text;
using StashBot.Module.Secure.Hash;
using StashBot.Module.Secure.AesCrypto;

namespace StashBot.Module.Secure
{
    internal class SecureManager : ISecureManager
    {
        private readonly ISecureHash secureHash;
        private readonly ISecureAesCrypto secureAesCrypto;

        internal SecureManager()
        {
            secureHash = new SecureHash();
            secureAesCrypto = new SecureAesCrypto();
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

        public string ByteArrayToString(byte[] bytes)
        {
            const char divider = ':';

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i]);
                if (i < bytes.Length - 1)
                {
                    stringBuilder.Append(divider);
                }
            }

            return stringBuilder.ToString();
        }

        public byte[] StringToByteArray(string str)
        {
            const char divider = ':';

            string[] bytesStr = str.Split(divider, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[bytesStr.Length];
            for (int i = 0; i < bytesStr.Length; i++)
            {
                bytes[i] = Convert.ToByte(bytesStr[i]);
            }

            return bytes;
        }
    }
}
