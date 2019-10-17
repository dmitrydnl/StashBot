using System.Security.Cryptography;

namespace StashBot.Module.Secure.RsaCrypto
{
    internal interface ISecureRsaCrypto
    {
        RSACryptoServiceProvider CreateRsaCryptoService();
        string EncryptWithRsa(RSACryptoServiceProvider csp, string text);
        string DecryptWithRsa(RSACryptoServiceProvider csp, string encryptedText);
        string RsaCryptoServiceToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters);
        RSACryptoServiceProvider RsaCryptoServiceFromXmlString(string xmlString);
    }
}
