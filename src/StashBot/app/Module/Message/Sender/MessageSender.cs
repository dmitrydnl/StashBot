using StashBot.Module.Session;
using Telegram.Bot;

namespace StashBot.Module.Message.Sender
{
    internal class MessageSender : IMessageSender
    {
        public async void SendMessage(long chatId, string messageText)
        {
            ITelegramBotClient telegramBotClient =
                ModulesManager.GetModulesManager().GetTelegramBotClient();
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            Telegram.Bot.Types.Message message = await telegramBotClient.SendTextMessageAsync(
              chatId: chatId,
              text: messageText
            );
            sessionManager.BotSentMessage(chatId, message.MessageId);
        }
    }
}
