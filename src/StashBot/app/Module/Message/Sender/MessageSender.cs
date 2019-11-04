using System.IO;
using System.Threading.Tasks;
using StashBot.Module.Session;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace StashBot.Module.Message.Sender
{
    internal class MessageSender : IMessageSender
    {
        public async Task SendTextMessage(long chatId, string messageText)
        {
            if (string.IsNullOrEmpty(messageText))
            {
                return;
            }

            ITelegramBotClient telegramBotClient = ModulesManager.GetModulesManager().GetTelegramBotClient();
            ISessionManager sessionManager = ModulesManager.GetModulesManager().GetSessionManager();

            Telegram.Bot.Types.Message message = await telegramBotClient.SendTextMessageAsync(
                chatId: chatId,
                text: messageText
            );
            sessionManager.BotSentMessage(chatId, message.MessageId);
        }

        public async Task SendPhotoMessage(long chatId, byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return;
            }

            ITelegramBotClient telegramBotClient = ModulesManager.GetModulesManager().GetTelegramBotClient();
            ISessionManager sessionManager = ModulesManager.GetModulesManager().GetSessionManager();

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
