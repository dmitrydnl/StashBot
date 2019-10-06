using System.Security.Cryptography;

namespace StashBot.Module.Secure.RsaCrypto
{
    internal interface ISecureRsaCrypto
    {
        RSACryptoServiceProvider CreateCryptoService();
        string ToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters);
        RSACryptoServiceProvider FromXmlString(string xmlString);
    }
}
