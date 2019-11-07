namespace StashBot.Module.Database.Account.Sqlite
{
    internal class UserSqliteFactory : IUserFactory
    {
        public IUser Create(long chatId, string hashPassword)
        {
            return new UserSqlite(chatId, hashPassword);
        }
    }
}
