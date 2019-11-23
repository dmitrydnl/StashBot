﻿using System.Collections.Generic;
using System.Threading.Tasks;
using StashBot.Module.Message.Handler;
using StashBot.Module.Message.Sender;
using StashBot.Module.Message.Delete;
using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Message
{
    internal class MessageManager : IMessageManager
    {
        private readonly IMessageHandler messageHandler;
        private readonly IMessageSender messageSender;
        private readonly IMessageDelete messageDelete;

        internal MessageManager()
        {
            messageHandler = new MessageHandler();
            messageSender = new MessageSender();
            messageDelete = new MessageDelete();
        }

        public void HandleUserMessage(ITelegramUserMessage message)
        {
            messageHandler.HandleUserMessage(message);
        }

        public Task SendTextMessage(long chatId, string textMessage, IReplyMarkup replyMarkup)
        {
            return messageSender.SendTextMessage(chatId, textMessage, replyMarkup);
        }

        public Task SendPhotoMessage(long chatId, byte[] imageBytes, IReplyMarkup replyMarkup)
        {
            return messageSender.SendPhotoMessage(chatId, imageBytes, replyMarkup);
        }

        public void DeleteMessage(long chatId, int messageId)
        {
            messageDelete.DeleteMessage(chatId, messageId);
        }

        public void DeleteListMessages(long chatId, List<int> messagesId)
        {
            messageDelete.DeleteListMessages(chatId, messagesId);
        }
    }
}
