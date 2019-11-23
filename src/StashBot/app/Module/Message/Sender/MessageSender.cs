using System.IO;
using System.Threading.Tasks;
using StashBot.Module.Session;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Message.Sender
{
    internal class MessageSender : IMessageSender
    {
        public async Task SendTextMessageAsync(long chatId, string messageText, IReplyMarkup replyMarkup)
        {
            if (string.IsNullOrEmpty(messageText))
            {
                return;
            }

            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            Telegram.Bot.Types.Message message = await telegramBotClient.SendTextMessageAsync(
                chatId: chatId,
                text: messageText,
                replyMarkup: replyMarkup
                );
            sessionManager.BotSentMessage(chatId, message.MessageId);
        }

        public async Task SendPhotoMessageAsync(long chatId, byte[] imageBytes, IReplyMarkup replyMarkup)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return;
            }

            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            using (Stream stream = new MemoryStream(imageBytes))
            {
                Telegram.Bot.Types.Message message = await telegramBotClient.SendPhotoAsync(
                    chatId,
                    new InputOnlineFile(stream),
                    replyMarkup: replyMarkup
                );
                sessionManager.BotSentMessage(chatId, message.MessageId);
            }
        }
    }
}
