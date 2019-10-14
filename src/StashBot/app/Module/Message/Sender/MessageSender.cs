using StashBot.Module.Session;
using Telegram.Bot;

namespace StashBot.Module.Message.Sender
{
    internal class MessageSender : IMessageSender
    {
        internal MessageSender()
        {
        }

        public async void SendTextMessage(long chatId, string textMessage)
        {
            ITelegramBotClient telegramBotClient =
                ModulesManager.GetModulesManager().GetTelegramBotClient();
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            Telegram.Bot.Types.Message message = await telegramBotClient.SendTextMessageAsync(
              chatId: chatId,
              text: textMessage
            );
            sessionManager.BotSentMessage(chatId, message.MessageId);
        }
    }
}
