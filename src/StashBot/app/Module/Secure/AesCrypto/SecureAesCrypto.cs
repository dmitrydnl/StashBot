using System;
using System.IO;
using System.Security.Cryptography;

namespace StashBot.Module.Secure.AesCrypto
{
    public class SecureAesCrypto : ISecureAesCrypto
    {
        private readonly Aes aes;

        public SecureAesCrypto()
        {
            aes = Aes.Create();
        }

        public string EncryptWithAes(string secretMessage)
        {
            if (string.IsNullOrEmpty(secretMessage))
            {
                throw new ArgumentException("Secret message required");
            }

            byte[] encryptedData;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(secretMessage);
                    }
                    encryptedData = msEncrypt.ToArray();
                }
            }

            return AesEncryptedDataToString(encryptedData);
        }

        public string DecryptWithAes(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage))
            {
                throw new ArgumentException("Encrypted message required");
            }

            byte[] encryptedData = AesStringToEncryptedData(encryptedMessage);
            string secretMessage = null;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        secretMessage = srDecrypt.ReadToEnd();
                    }
                }
            }

            return secretMessage;
        }

        private string AesEncryptedDataToString(byte[] encryptedData)
        {
            byte[] iv = aes.IV;
            byte[] result = new byte[iv.Length + encryptedData.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedData, 0, result, iv.Length, encryptedData.Length);
            return Convert.ToBase64String(result);
        }

        private byte[] AesStringToEncryptedData(string encryptedText)
        {
            encryptedText = encryptedText.Replace(" ", "+");
            byte[] fullCipher = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[16];
            byte[] encryptedData = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, encryptedData, 0, fullCipher.Length - iv.Length);
            return encryptedData;
        }
    }
}
