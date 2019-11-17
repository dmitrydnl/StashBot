using NUnit.Framework;
using StashBot.Module.Secure.AesCrypto;

namespace StashBotTest.Secure.AesCrypto
{
    public class SecureAesCryptoTest
    {
        private SecureAesCrypto secureAesCrypto;

        [SetUp]
        public void Setup()
        {
            secureAesCrypto = new SecureAesCrypto();
        }

        [Test]
        public void EncryptWithAesTest()
        {
            const string secureText = "SecureText";
            string encrypted1 = secureAesCrypto.EncryptWithAes(secureText);
            string encrypted2 = secureAesCrypto.EncryptWithAes(secureText);
            Assert.AreEqual(encrypted1, encrypted2);
        }

        [Test]
        public void DecryptWithAesTest()
        {
            const string secureText = "SecureText";
            string encrypted = secureAesCrypto.EncryptWithAes(secureText);
            string decryped = secureAesCrypto.DecryptWithAes(encrypted);
            Assert.AreEqual(secureText, decryped);
        }
    }
}
