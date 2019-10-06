using StashBot.Module.ChatSession;
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
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();

            Telegram.Bot.Types.Message message = await telegramBotClient.SendTextMessageAsync(
              chatId: chatId,
              text: textMessage
            );
            sessionsManager.BotSentMessage(chatId, message.MessageId);
        }
    }
}
