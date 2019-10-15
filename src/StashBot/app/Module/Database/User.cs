﻿using System.Security.Cryptography;
using StashBot.Module.Secure;

namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        private readonly long chatId;
        private readonly string hashAuthCode;
        private RSACryptoServiceProvider rsaCryptoServiceProvider;

        internal User(long chatId, string authCode)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            this.chatId = chatId;
            hashAuthCode = secureManager.CalculateHash(authCode);
            rsaCryptoServiceProvider = null;
        }

        public void Login(string authCode)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            byte[] encryptedRsa = secureManager.AesStringToEncryptedData(authCode);
            string rsaXmlString = secureManager.DecryptWithAes(encryptedRsa);
            rsaCryptoServiceProvider = secureManager.RsaCryptoServiceFromXmlString(rsaXmlString);
        }

        public void Logout()
        {
            rsaCryptoServiceProvider.Clear();
            rsaCryptoServiceProvider = null;
        }

        public bool ValidateAuthCode(string authCode)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            return secureManager.CompareWithHash(authCode, hashAuthCode);
        }

        public RSACryptoServiceProvider RsaCryptoServiceProvider()
        {
            return rsaCryptoServiceProvider;
        }
    }
}
