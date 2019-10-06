using StashBot.Module.Secure.Hash;

namespace StashBot.Module.Secure
{
    internal class SecureManager : ISecureManager
    {
        private readonly ISecureHash secureHash;

        internal SecureManager()
        {
            secureHash = new SecureHash();
        }

        public string CalculateHash(string input)
        {
            return secureHash.CalculateHash(input);
        }

        public bool CompareWithHash(string input, string hash)
        {
            return secureHash.CompareWithHash(input, hash);
        }
    }
}
