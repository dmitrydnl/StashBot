using NUnit.Framework;
using StashBot.Module.Secure.Hash;

namespace StashBotTest.Secure.Hash
{
    public class SecureHashTest
    {
        private SecureHash secureHash;

        [SetUp]
        public void Setup()
        {
            secureHash = new SecureHash();
        }

        [Test]
        public void CompareWithHashTest()
        {
            string hash = secureHash.CalculateHash("SecureText");
            bool equal = secureHash.CompareWithHash("SecureText", hash);
            Assert.AreEqual(equal, true);
        }
    }
}
