namespace StashBot.Module.Secure.AesCrypto
{
    internal interface ISecureAesCrypto
    {
        byte[] Encrypt(string text);
        string Decrypt(byte[] encrypted);
    }
}
