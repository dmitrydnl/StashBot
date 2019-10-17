using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace StashBot.Module.Secure.Hash
{
    internal class SecureHash : ISecureHash
    {
        public string CalculateHash(string text)
        {
            byte[] salt = GenerateSalt(16);
            byte[] bytes = KeyDerivation.Pbkdf2(text, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
            return $"{Convert.ToBase64String(salt)}:" + $"{Convert.ToBase64String(bytes)}";
        }

        public bool CompareWithHash(string text, string hash)
        {
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
