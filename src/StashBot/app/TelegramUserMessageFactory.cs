using Telegram.Bot.Types;

namespace StashBot
{
    public class TelegramUserMessageFactory : ITelegramUserMessageFactory
    {
        public ITelegramUserMessage Create(Message telegramMessage)
        {
            return new TelegramUserMessage(telegramMessage);
        }
    }
}
