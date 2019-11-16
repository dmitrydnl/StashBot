using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace StashBot.Module.Secure.Hash
{
    public class SecureHash : ISecureHash
    {
        public string CalculateHash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text for hashing shouldn't be empty");
            }

            byte[] salt = GenerateSalt(16);
            byte[] bytes = KeyDerivation.Pbkdf2(text, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
        }

        public bool CompareWithHash(string text, string hash)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text for comparing with hash shouldn't be empty");
            }

            if (string.IsNullOrEmpty(hash))
            {
                throw new ArgumentException("Hash shouldn't be empty");
            }

            try
            {
                string[] parts = hash.Split(':');
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] bytes = KeyDerivation.Pbkdf2(text, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }

        private byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }
            return salt;
        }
    }
}
