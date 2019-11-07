namespace StashBot.Module.Database.Account.Local
{
    internal class UserDatabaseLocalFactory : IUserFactory
    {
        public IUser Create(long chatId, string password)
        {
            return new UserDatabaseLocal(chatId, password);
        }
    }
}
