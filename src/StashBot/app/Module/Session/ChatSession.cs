using System;
using System.Collections.Generic;
using StashBot.Module.Message;
using StashBot.Module.User;
using StashBot.BotSettings;

namespace StashBot.Module.Session
{
    internal class ChatSession : IChatSession
    {
        public long ChatId
        {
            get;
        }

        public ChatSessionState State
        {
            get;
            set;
        }

        private DateTime lastUserMessage;
        private DateTime lastBotMessage;
        private readonly List<int> userMessagesId;
        private readonly List<int> botMessagesId;

        internal ChatSession(long chatId)
        {
            ChatId = chatId;
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

        public void MessageDeleted(int messageId)
        {
            userMessagesId.Remove(messageId);
            botMessagesId.Remove(messageId);
        }

        public void Kill()
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();
            IUserManager userManager = ModulesManager.GetUserManager();

            List<int> copyBotMessagesId = new List<int>(botMessagesId);
            List<int> copyUserMessagesId = new List<int>(userMessagesId);
            messageManager.DeleteListMessages(ChatId, copyBotMessagesId);
            messageManager.DeleteListMessages(ChatId, copyUserMessagesId);
            userManager.LogoutUser(ChatId);
        }

        public bool IsNeedKill()
        {
            DateTime endLiveTime = lastUserMessage.AddSeconds(ChatSessionSettings.ChatSessionLiveTime);
            return endLiveTime <= DateTime.UtcNow;
        }
    }
}
