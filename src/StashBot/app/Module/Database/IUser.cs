using System.Security.Cryptography;

namespace StashBot.Module.Database
{
    internal interface IUser
    {
        void Login(string authCode);
        void Logout();
        bool ValidateAuthCode(string authCode);
        RSACryptoServiceProvider RsaCryptoServiceProvider();
    }
}
