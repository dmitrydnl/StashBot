using System.Collections.Generic;

namespace StashBot.Module.ChatSession
{
    internal class SessionsManager : ISessionsManager
    {
        private const int TIMER_INTERVAL_CLEAR_SESSIONS = 10;
        private const int SESSION_LIVE_TIME_SEC = 60;

        private readonly Dictionary<long, ISession> currentSessions;

        internal SessionsManager()
        {
            currentSessions = new Dictionary<long, ISession>();
        }

        public void CreateSession(long chatId)
        {
            Session newSession = new Session(chatId);
            currentSessions.Add(chatId, newSession);
        }

        public bool ContainsSession(long chatId)
        {
            return currentSessions.ContainsKey(chatId);
        }

        public ISession GetSession(long chatId)
        {
            if (!ContainsSession(chatId))
            {
                return null;
            }

            return currentSessions[chatId];
        }

        public void UserSentMessage(long chatId, int messageId)
        {
            ISession session = currentSessions[chatId];
            session.UserSentMessage(messageId);
        }

        public void BotSentMessage(long chatId, int messageId)
        {
            ISession session = currentSessions[chatId];
            session.BotSentMessage(messageId);
        }
    }
}
