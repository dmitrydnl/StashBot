using System;
using System.Timers;
using System.Collections.Generic;
using StashBot.Module.Message;
using StashBot.Module.Database;

namespace StashBot.Module.Session
{
    internal class SessionManager : ISessionManager
    {
        private const int TIMER_INTERVAL_CLEAR_CHAT_SESSIONS = 10;
        private const int CHAT_SESSION_LIVE_TIME_SEC = 60;

        private readonly Dictionary<long, IChatSession> currentChatSessions;

        internal SessionManager()
        {
            currentChatSessions = new Dictionary<long, IChatSession>();
            StartClearChatSessionsTimer();
        }

        public void CreateChatSession(long chatId)
        {
            ChatSession newChatSession = new ChatSession(chatId);
            currentChatSessions.Add(chatId, newChatSession);
        }

        public bool ContainsChatSession(long chatId)
        {
            return currentChatSessions.ContainsKey(chatId);
        }

        public IChatSession GetChatSession(long chatId)
        {
            if (!ContainsChatSession(chatId))
            {
                return null;
            }

            return currentChatSessions[chatId];
        }

        public void AuthorizeChatSession(long chatId)
        {
            currentChatSessions[chatId].Authorize();
        }

        public void UserSentMessage(long chatId, int messageId)
        {
            IChatSession chatSession = currentChatSessions[chatId];
            chatSession.UserSentMessage(messageId);
        }

        public void BotSentMessage(long chatId, int messageId)
        {
            IChatSession chatSession = currentChatSessions[chatId];
            chatSession.BotSentMessage(messageId);
        }

        private void StartClearChatSessionsTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ClearSessions;
            timer.Interval = TIMER_INTERVAL_CLEAR_CHAT_SESSIONS * 1000;
            timer.Enabled = true;
        }

        private void ClearSessions(object sender, ElapsedEventArgs e)
        {
            foreach (var s in currentChatSessions)
            {
                IChatSession chatSession = s.Value;
                DateTime endLiveTime = chatSession.LastUserMessage()
                    .AddSeconds(CHAT_SESSION_LIVE_TIME_SEC);
                if (endLiveTime <= DateTime.UtcNow)
                {
                    KillChatSession(chatSession.ChatId());
                }
            }
        }

        private void KillChatSession(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            List<int> botMessagesId = GetChatSession(chatId).BotMessagesId();
            messageManager.DeleteListBotMessages(chatId, botMessagesId);
            currentChatSessions.Remove(chatId);
            databaseManager.LogoutUser(chatId);
        }
    }
}
