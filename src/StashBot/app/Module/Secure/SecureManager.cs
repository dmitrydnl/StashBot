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
    }
}
