using System.Security.Cryptography;

namespace StashBot.Module.Database
{
    internal interface IUser
    {
        void Authorize(string rsaXmlString);
        string HashAuthCode();
        RSACryptoServiceProvider RsaCryptoServiceProvider();
    }
}
