using System;
using System.Timers;
using System.Collections.Generic;
using StashBot.Module.Message;

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
            StartClearSessionsTimer();
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

        private void StartClearSessionsTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ClearSessions;
            timer.Interval = TIMER_INTERVAL_CLEAR_SESSIONS * 1000;
            timer.Enabled = true;
        }

        private void ClearSessions(object sender, ElapsedEventArgs e)
        {
            foreach (var s in currentSessions)
            {
                ISession session = s.Value;
                DateTime endLiveTime = session.LastUserMessage()
                    .AddSeconds(SESSION_LIVE_TIME_SEC);
                if (endLiveTime <= DateTime.UtcNow)
                {
                    KillSession(session.ChatId());
                }
            }
        }

        private void KillSession(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            List<int> botMessagesId = GetSession(chatId).BotMessagesId();
            messageManager.DeleteListBotMessages(chatId, botMessagesId);
            currentSessions.Remove(chatId);
        }
    }
}
