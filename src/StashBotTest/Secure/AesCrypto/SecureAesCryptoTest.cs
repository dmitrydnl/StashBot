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
            string encrypted1 = secureAesCrypto.EncryptWithAes("SecureText");
            string encrypted2 = secureAesCrypto.EncryptWithAes("SecureText");
            Assert.AreEqual(encrypted1, encrypted2);
        }

        [Test]
        public void DecryptWithAesTest()
        {
            string encrypted = secureAesCrypto.EncryptWithAes("SecureText");
            string decryped = secureAesCrypto.DecryptWithAes(encrypted);
            Assert.AreEqual("SecureText", decryped);
        }
    }
}
