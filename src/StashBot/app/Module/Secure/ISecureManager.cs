using System.Security.Cryptography;

namespace StashBot.Module.Secure
{
    internal interface ISecureManager
    {
        string CalculateHash(string input);
        bool CompareWithHash(string input, string hash);
        byte[] EncryptWithAes(string text);
        string DecryptWithAes(byte[] encrypted);
        string AesEncryptedDataToString(byte[] encrypted);
        byte[] AesStringToEncryptedData(string cipherText);
        RSACryptoServiceProvider CreateRsaCryptoService();
        string RsaCryptoServiceToXmlString(RSACryptoServiceProvider csp, bool includePrivateParameters);
        RSACryptoServiceProvider RsaCryptoServiceFromXmlString(string xmlString);
    }
}
