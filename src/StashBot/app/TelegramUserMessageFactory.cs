using Telegram.Bot.Types;

namespace StashBot
{
    internal class TelegramUserMessageFactory : ITelegramUserMessageFactory
    {
        public ITelegramUserMessage Create(Message telegramMessage)
        {
            return new TelegramUserMessage(telegramMessage);
        }
    }
}
