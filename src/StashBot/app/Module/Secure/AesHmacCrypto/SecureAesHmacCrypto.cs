using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StashBot.Module.Secure.AesHmacCrypto
{
    internal class SecureAesHmacCrypto : ISecureAesHmacCrypto
    {
        private const int BLOCK_BIT_SIZE = 128;
        private const int KEY_BIT_SIZE = 256;
        private const int SALT_BIT_SIZE = 64;
        private const int ITERATIONS = 10000;
        private const int MIN_PASSWORD_LENGTH = 12;

        internal SecureAesHmacCrypto()
        {
            
        }

        public string EncryptWithAesHmac(string secretMessage, string password, byte[] nonSecretPayload = null)
        {
            if (string.IsNullOrEmpty(secretMessage))
            {
                throw new ArgumentException("Secret Message Required!", nameof(secretMessage));
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(string.Format("Must have a password of at least {0} characters!", MIN_PASSWORD_LENGTH), nameof(password));
            }

            byte[] plainText = Encoding.UTF8.GetBytes(secretMessage);
            byte[] cipherText = EncryptWithPassword(plainText, password, nonSecretPayload);
            return Convert.ToBase64String(cipherText);
        }

        public string DecryptWithAesHmac(string encryptedMessage, string password, int nonSecretPayloadLength = 0)
        {
            if (string.IsNullOrWhiteSpace(encryptedMessage))
            {
                throw new ArgumentException("Encrypted Message Required!", nameof(encryptedMessage));
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(string.Format("Must have a password of at least {0} characters!", MIN_PASSWORD_LENGTH), nameof(password));
            }

            byte[] cipherText = Convert.FromBase64String(encryptedMessage);
            byte[] plainText = DecryptWithPassword(cipherText, password, nonSecretPayloadLength);
            return plainText == null ? null : Encoding.UTF8.GetString(plainText);
        }

        private byte[] EncryptWithPassword(byte[] secretMessage, string password, byte[] nonSecretPayload = null)
        {
            nonSecretPayload = nonSecretPayload ?? new byte[] { };

            byte[] payload = new byte[(SALT_BIT_SIZE / 8 * 2) + nonSecretPayload.Length];

            Array.Copy(nonSecretPayload, payload, nonSecretPayload.Length);
            int payloadIndex = nonSecretPayload.Length;

            byte[] cryptKey;
            byte[] authKey;
            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, SALT_BIT_SIZE / 8, ITERATIONS))
            {
                byte[] salt = generator.Salt;
                cryptKey = generator.GetBytes(KEY_BIT_SIZE / 8);
                Array.Copy(salt, 0, payload, payloadIndex, salt.Length);
                payloadIndex += salt.Length;
            }

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, SALT_BIT_SIZE / 8, ITERATIONS))
            {
                byte[] salt = generator.Salt;
                authKey = generator.GetBytes(KEY_BIT_SIZE / 8);
                Array.Copy(salt, 0, payload, payloadIndex, salt.Length);
            }

            return Encrypt(secretMessage, cryptKey, authKey, payload);
        }

        private byte[] DecryptWithPassword(byte[] encryptedMessage, string password, int nonSecretPayloadLength = 0)
        {
            byte[] cryptSalt = new byte[SALT_BIT_SIZE / 8];
            byte[] authSalt = new byte[SALT_BIT_SIZE / 8];

            Array.Copy(encryptedMessage, nonSecretPayloadLength, cryptSalt, 0, cryptSalt.Length);
            Array.Copy(encryptedMessage, nonSecretPayloadLength + cryptSalt.Length, authSalt, 0, authSalt.Length);

            byte[] cryptKey;
            byte[] authKey;

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, cryptSalt, ITERATIONS))
            {
                cryptKey = generator.GetBytes(KEY_BIT_SIZE / 8);
            }

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, authSalt, ITERATIONS))
            {
                authKey = generator.GetBytes(KEY_BIT_SIZE / 8);
            }

            return Decrypt(encryptedMessage, cryptKey, authKey, cryptSalt.Length + authSalt.Length + nonSecretPayloadLength);
        }

        private byte[] Encrypt(byte[] secretMessage, byte[] cryptKey, byte[] authKey, byte[] nonSecretPayload = null)
        {
            nonSecretPayload = nonSecretPayload ?? new byte[] { };

            byte[] cipherText;
            byte[] iv;

            using (AesManaged aes = new AesManaged
            {
                KeySize = KEY_BIT_SIZE,
                BlockSize = BLOCK_BIT_SIZE,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                aes.GenerateIV();
                iv = aes.IV;

                using (ICryptoTransform encrypter = aes.CreateEncryptor(cryptKey, iv))
                using (MemoryStream cipherStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (BinaryWriter binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        binaryWriter.Write(secretMessage);
                    }

                    cipherText = cipherStream.ToArray();
                }

            }

            using (HMACSHA256 hmac = new HMACSHA256(authKey))
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(encryptedStream))
                {
                    binaryWriter.Write(nonSecretPayload);
                    binaryWriter.Write(iv);
                    binaryWriter.Write(cipherText);
                    binaryWriter.Flush();

                    byte[] tag = hmac.ComputeHash(encryptedStream.ToArray());
                    binaryWriter.Write(tag);
                }

                return encryptedStream.ToArray();
            }
        }

        private byte[] Decrypt(byte[] encryptedMessage, byte[] cryptKey, byte[] authKey, int nonSecretPayloadLength = 0)
        {
            using (HMACSHA256 hmac = new HMACSHA256(authKey))
            {
                byte[] sentTag = new byte[hmac.HashSize / 8];
                byte[] calcTag = hmac.ComputeHash(encryptedMessage, 0, encryptedMessage.Length - sentTag.Length);
                int ivLength = (BLOCK_BIT_SIZE / 8);

                if (encryptedMessage.Length < sentTag.Length + nonSecretPayloadLength + ivLength)
                    return null;

                Array.Copy(encryptedMessage, encryptedMessage.Length - sentTag.Length, sentTag, 0, sentTag.Length);

                int compare = 0;
                for (int i = 0; i < sentTag.Length; i++)
                    compare |= sentTag[i] ^ calcTag[i];

                if (compare != 0)
                    return null;

                using (AesManaged aes = new AesManaged
                {
                    KeySize = KEY_BIT_SIZE,
                    BlockSize = BLOCK_BIT_SIZE,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    byte[] iv = new byte[ivLength];
                    Array.Copy(encryptedMessage, nonSecretPayloadLength, iv, 0, iv.Length);

                    using (ICryptoTransform decrypter = aes.CreateDecryptor(cryptKey, iv))
                    using (MemoryStream plainTextStream = new MemoryStream())
                    {
                        using (CryptoStream decrypterStream = new CryptoStream(plainTextStream, decrypter, CryptoStreamMode.Write))
                        using (BinaryWriter binaryWriter = new BinaryWriter(decrypterStream))
                        {
                            binaryWriter.Write(
                                encryptedMessage,
                                nonSecretPayloadLength + iv.Length,
                                encryptedMessage.Length - nonSecretPayloadLength - iv.Length - sentTag.Length
                            );
                        }

                        return plainTextStream.ToArray();
                    }
                }
            }
        }
    }
}
