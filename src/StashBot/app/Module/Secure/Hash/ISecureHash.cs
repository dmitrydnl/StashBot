namespace StashBot.Module.Secure.Hash
{
    internal interface ISecureHash
    {
        string CalculateHash(string text);
        bool CompareWithHash(string text, string hash);
    }
}
