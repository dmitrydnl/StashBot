using Telegram.Bot.Types;

namespace StashBot
{
    internal interface ITelegramUserMessageFactory
    {
        ITelegramUserMessage Create(Message telegramMessage);
    }
}
