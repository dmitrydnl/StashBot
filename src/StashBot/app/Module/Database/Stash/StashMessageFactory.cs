namespace StashBot.Module.Database.Stash
{
    internal class StashMessageFactory : IStashMessageFactory
    {
        public IStashMessage Create(ITelegramUserMessage telegramMessage)
        {
            return new StashMessage(telegramMessage);
        }
    }
}
