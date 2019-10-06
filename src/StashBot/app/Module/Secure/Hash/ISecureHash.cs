namespace StashBot.Module.Secure.Hash
{
    internal interface ISecureHash
    {
        string CalculateHash(string input);
        bool CompareWithHash(string input, string hash);
    }
}
