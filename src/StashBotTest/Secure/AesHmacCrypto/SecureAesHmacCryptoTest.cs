using NUnit.Framework;
using StashBot.Module.Secure.AesHmacCrypto;

namespace StashBotTest.Secure.AesHmacCrypto
{
    public class SecureAesHmacCryptoTest
    {
        private SecureAesHmacCrypto secureAesHmacCrypto;

        [SetUp]
        public void Setup()
        {
            secureAesHmacCrypto = new SecureAesHmacCrypto();
        }

        [Test]
        public void DecryptWithAesHmacTest()
        {
            const string secureText = "SecureText";
            const string password = "secure_password_228";
            string encrypted = secureAesHmacCrypto.EncryptWithAesHmac(secureText, password);
            string decryped = secureAesHmacCrypto.DecryptWithAesHmac(encrypted, password);
            Assert.AreEqual(secureText, decryped);
        }
    }
}
