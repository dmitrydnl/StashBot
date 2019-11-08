namespace StashBot.Module.Database.Stash.Sqlite
{
    internal class StashMessageSqliteFactory : IStashMessageFactory
    {
        public IStashMessage Create(ITelegramUserMessage telegramMessage)
        {
            return new StashMessageSqlite();
        }
    }
}
