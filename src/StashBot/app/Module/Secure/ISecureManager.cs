using StashBot.Module.Secure.Hash;
using StashBot.Module.Secure.AesCrypto;
using StashBot.Module.Secure.AesHmacCrypto;

namespace StashBot.Module.Secure
{
    internal interface ISecureManager : ISecureHash, ISecureAesCrypto, ISecureAesHmacCrypto
    {

    }
}
