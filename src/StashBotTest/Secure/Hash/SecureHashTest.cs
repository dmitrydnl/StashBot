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
            const string secureText = "SecureText";
            string hash = secureHash.CalculateHash(secureText);
            bool equal = secureHash.CompareWithHash(secureText, hash);
            Assert.AreEqual(equal, true);
        }
    }
}
