namespace StashBot.Module.Database.Stash
{
    internal interface IStashMessageFactory
    {
        IStashMessage Create(ITelegramUserMessage telegramMessage);
    }
}
