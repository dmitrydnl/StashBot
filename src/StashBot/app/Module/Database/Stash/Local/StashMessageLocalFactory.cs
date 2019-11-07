namespace StashBot.Module.Database.Stash.Local
{
    internal class StashMessageLocalFactory : IStashMessageFactory
    {
        public IStashMessage Create(ITelegramUserMessage telegramMessage)
        {
            return new StashMessageLocal(telegramMessage);
        }
    }
}
