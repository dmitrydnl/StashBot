using System.Security.Cryptography;

namespace StashBot.Module.Secure.RsaCrypto
{
    internal interface ISecureRsaCrypto
    {
        RSACryptoServiceProvider CreateRsaCryptoService();
        string RsaCryptoServiceToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters);
        RSACryptoServiceProvider RsaCryptoServiceFromXmlString(string xmlString);
    }
}
