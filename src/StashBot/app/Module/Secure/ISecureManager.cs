namespace StashBot.Module.Secure
{
    internal interface ISecureManager
    {
        string CalculateHash(string input);
        bool CompareWithHash(string input, string hash);
    }
}
