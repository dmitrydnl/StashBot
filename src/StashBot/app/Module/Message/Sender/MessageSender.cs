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
        public async Task SendTextMessage(long chatId, string messageText, ReplyKeyboardMarkup replyKeyboard)
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
                replyMarkup: replyKeyboard
                );
            sessionManager.BotSentMessage(chatId, message.MessageId);
        }

        public async Task SendPhotoMessage(long chatId, byte[] imageBytes)
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
                    new InputOnlineFile(stream)
                );
                sessionManager.BotSentMessage(chatId, message.MessageId);
            }
        }
    }
}
