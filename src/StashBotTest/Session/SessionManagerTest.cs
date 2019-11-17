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
            const int chatSessionId = 1;
            bool exist1 = sessionManager.IsChatSessionExist(chatSessionId);
            sessionManager.CreateChatSession(chatSessionId);
            bool exist2 = sessionManager.IsChatSessionExist(chatSessionId);
            Assert.AreEqual(exist1, false);
            Assert.AreEqual(exist2, true);
        }

        [Test]
        public void GetChatSessionTest()
        {
            const int chatSessionId = 12;
            IChatSession chatSession1 = sessionManager.GetChatSession(chatSessionId);
            sessionManager.CreateChatSession(chatSessionId);
            IChatSession chatSession2 = sessionManager.GetChatSession(chatSessionId);
            Assert.AreEqual(chatSession1, null);
            Assert.AreNotEqual(chatSession2, null);
        }

        [Test]
        public void KillChatSessionTest()
        {
            const int chatSessionId = 12;
            bool exist1 = sessionManager.IsChatSessionExist(chatSessionId);
            sessionManager.CreateChatSession(chatSessionId);
            bool exist2 = sessionManager.IsChatSessionExist(chatSessionId);
            sessionManager.KillChatSession(chatSessionId);
            bool exist3 = sessionManager.IsChatSessionExist(chatSessionId);
            Assert.AreEqual(exist1, false);
            Assert.AreEqual(exist2, true);
            Assert.AreEqual(exist3, false);
        }
    }
}
