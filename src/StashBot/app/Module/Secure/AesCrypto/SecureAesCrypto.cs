using System;
using System.IO;
using System.Security.Cryptography;

namespace StashBot.Module.Secure.AesCrypto
{
    internal class SecureAesCrypto : ISecureAesCrypto
    {
        private readonly Aes aes;

        internal SecureAesCrypto()
        {
            aes = Aes.Create();
        }

        public byte[] EncryptWithAes(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (aes == null)
                throw new ArgumentNullException(nameof(aes));

            byte[] encryptedData;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    encryptedData = msEncrypt.ToArray();
                }
            }

            return encryptedData;
        }

        public string DecryptWithAes(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length <= 0)
                throw new ArgumentNullException(nameof(encryptedData));
            if (aes == null)
                throw new ArgumentNullException(nameof(aes));

            string text = null;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }

            return text;
        }

        public string AesEncryptedDataToString(byte[] encryptedData)
        {
            byte[] iv = aes.IV;
            byte[] result = new byte[iv.Length + encryptedData.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedData, 0, result, iv.Length, encryptedData.Length);
            return Convert.ToBase64String(result);
        }

        public byte[] AesStringToEncryptedData(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return null;
            }

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
