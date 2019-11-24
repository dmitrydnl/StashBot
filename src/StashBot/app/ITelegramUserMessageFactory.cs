using Telegram.Bot.Types;

namespace StashBot
{
    public interface ITelegramUserMessageFactory
    {
        ITelegramUserMessage Create(Message telegramMessage);
    }
}
