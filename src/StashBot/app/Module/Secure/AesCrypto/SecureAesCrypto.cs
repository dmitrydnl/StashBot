﻿using System;
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

        public byte[] Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (aes == null)
                throw new ArgumentNullException(nameof(aes));

            byte[] encrypted;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        public string Decrypt(byte[] encrypted)
        {
            if (encrypted == null || encrypted.Length <= 0)
                throw new ArgumentNullException(nameof(encrypted));
            if (aes == null)
                throw new ArgumentNullException(nameof(aes));

            string text = null;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encrypted))
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

        public string EncryptedDataToString(byte[] encrypted)
        {
            byte[] iv = aes.IV;
            byte[] result = new byte[iv.Length + encrypted.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
            return Convert.ToBase64String(result);
        }

        public byte[] StringToEncryptedData(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return null;
            }

            cipherText = cipherText.Replace(" ", "+");
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            return cipher;
        }
    }
}
