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

        public void Kill()
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager = ModulesManager.GetModulesManager().GetUserManager();

            messageManager.DeleteListMessages(ChatId, botMessagesId);
            botMessagesId.Clear();
            messageManager.DeleteListMessages(ChatId, userMessagesId);
            userMessagesId.Clear();
            userManager.LogoutUser(ChatId);
        }

        public bool NeedKill()
        {
            DateTime endLiveTime = lastUserMessage.AddSeconds(ChatSessionSettings.ChatSessionLiveTime);
            return endLiveTime <= DateTime.UtcNow;
        }
    }
}
