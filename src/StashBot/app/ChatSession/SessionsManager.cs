using System;
using System.Timers;
using System.Collections.Generic;
using Telegram.Bot;

namespace StashBot.ChatSession
{
    internal class SessionsManager
    {
        private const int INTERVAL_CLEAR_SESSIONS = 10;
        private const int ALIVE_SESSION_TIME = 60;
        private readonly ITelegramBotClient botClient;
        private readonly Dictionary<long, Session> currentSessions;

        internal SessionsManager(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            currentSessions = new Dictionary<long, Session>();
            StartClearSessionsTimer();
        }

        internal bool ContainsSession(long chatId)
        {
            return currentSessions.ContainsKey(chatId);
        }

        internal void CreateSession(long chatId)
        {
            Session newSession = new Session(chatId);
            currentSessions.Add(chatId, newSession);
        }

        internal void SessionUserMessageReceived(long chatId)
        {
            Session session = currentSessions[chatId];
            session.UserMessageReceived();
        }

        internal void SessionBotMessageSent(long chatId, int messageId)
        {
            Session session = currentSessions[chatId];
            session.BotMessageSent(messageId);
        }

        internal void ShowDetails()
        {
            foreach (var s in currentSessions)
            {
                Session session = s.Value;
                Console.WriteLine(session.ChatId() + " " + session.LastUserMessage());
            }
        }

        private void StartClearSessionsTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ClearSessions;
            timer.Interval = INTERVAL_CLEAR_SESSIONS * 1000;
            timer.Enabled = true;
        }

        private void ClearSessions(object sender, ElapsedEventArgs e)
        {
            foreach (var s in currentSessions)
            {
                Session session = s.Value;
                if (session.LastUserMessage().AddSeconds(ALIVE_SESSION_TIME) <= DateTime.UtcNow)
                {
                    KillSession(session.ChatId());
                }
            }
        }

        private void KillSession(long chatId)
        {
            RemoveSessionBotMessages(currentSessions[chatId]);
            currentSessions.Remove(chatId);
        }

        private void RemoveSessionBotMessages(Session session)
        {
            foreach (int messageId in session.BotMessagesId())
            {
                botClient.DeleteMessageAsync(session.ChatId(), messageId);
            }
        }
    }
}
