using NUnit.Framework;
using StashBot.Module.Session;

namespace StashBotTest.Session
{
    public class SessionManagerTest
    {
        private SessionManager sessionManager;

        [SetUp]
        public void Setup()
        {
            sessionManager = new SessionManager();
        }

        [Test]
        public void CreateChatSessionTest()
        {
            bool exist1 = sessionManager.IsChatSessionExist(12345);
            sessionManager.CreateChatSession(12345);
            bool exist2 = sessionManager.IsChatSessionExist(12345);
            Assert.AreEqual(exist1, false);
            Assert.AreEqual(exist2, true);
        }

        [Test]
        public void GetChatSessionTest()
        {
            IChatSession chatSession1 = sessionManager.GetChatSession(123456);
            sessionManager.CreateChatSession(123456);
            IChatSession chatSession2 = sessionManager.GetChatSession(123456);
            Assert.AreEqual(chatSession1, null);
            Assert.AreNotEqual(chatSession2, null);
        }
    }
}
