using System.Security.Cryptography;

namespace StashBot.Module.Database
{
    internal interface IUser
    {
        void Authorize(string authCode);
        bool ValidateAuthCode(string authCode);
        RSACryptoServiceProvider RsaCryptoServiceProvider();
    }
}
