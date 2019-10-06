namespace StashBot.Module.Secure
{
    internal interface ISecureManager
    {
        string CalculateHash(string input);
        bool CompareWithHash(string input, string hash);
        byte[] EncryptWithAes(string text);
        string DecryptWithAes(byte[] encrypted);
        string ByteArrayToString(byte[] bytes);
        byte[] StringToByteArray(string str);
    }
}
