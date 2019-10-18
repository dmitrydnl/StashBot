using StashBot.Module.Secure.Hash;
using StashBot.Module.Secure.AesCrypto;
using StashBot.Module.Secure.RsaCrypto;

namespace StashBot.Module.Secure
{
    internal interface ISecureManager : ISecureHash, ISecureAesCrypto, ISecureRsaCrypto
    {

    }
}
