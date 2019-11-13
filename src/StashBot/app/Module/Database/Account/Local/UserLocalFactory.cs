namespace StashBot.Module.Database.Account.Local
{
    internal class UserLocalFactory : IUserFactory
    {
        public IUser Create(long chatId, string password)
        {
            return new UserLocal(chatId, password);
        }
    }
}
