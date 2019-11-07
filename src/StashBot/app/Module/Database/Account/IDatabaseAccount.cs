namespace StashBot.Module.Database.Account
{
    internal interface IDatabaseAccount
    {
        void CreateUser(long chatId, string password);
        IUser GetUser(long chatId);
        bool IsUserExist(long chatId);
        bool LoginUser(long chatId, string password);
        void LogoutUser(long chatId);
    }
}
