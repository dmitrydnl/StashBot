using System;
using System.Collections.Generic;
using StashBot.Module.Message;
using StashBot.Module.User;

namespace StashBot.Module.Session
{
    internal class ChatSession : IChatSession
    {
        private const int CHAT_SESSION_LIVE_TIME_SEC = 60;

        private DateTime lastUserMessage;
        private DateTime lastBotMessage;
        private readonly List<int> userMessagesId;
        private readonly List<int> botMessagesId;

        public long ChatId {
            get;
        }

        public ChatSessionState State {
            get;
            set;
        }

        internal ChatSession(long chatId)
        {
            this.ChatId = chatId;
            State = ChatSessionState.FirstMessage;
            lastUserMessage = DateTime.UtcNow;
            lastBotMessage = DateTime.UtcNow;
            userMessagesId = new List<int>();
            botMessagesId = new List<int>();
        }

        public void UserSentMessage(int messageId)
        {
            lastUserMessage = DateTime.UtcNow;
            userMessagesId.Add(messageId);
        }

        public void BotSentMessage(int messageId)
        {
            lastBotMessage = DateTime.UtcNow;
            botMessagesId.Add(messageId);
        }

        public void Kill()
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            messageManager.DeleteListMessages(ChatId, botMessagesId);
            botMessagesId.Clear();
            messageManager.DeleteListMessages(ChatId, userMessagesId);
            userMessagesId.Clear();
            userManager.LogoutUser(ChatId);
        }

        public bool NeedKill()
        {
            DateTime endLiveTime = lastUserMessage.AddSeconds(CHAT_SESSION_LIVE_TIME_SEC);
            return endLiveTime <= DateTime.UtcNow;
        }
    }
}
