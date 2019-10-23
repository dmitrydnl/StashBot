namespace StashBot.Module.Database.Account
{
    internal interface IDatabaseAccount
    {
        void CreateNewUser(long chatId, string password);
        bool IsUserExist(long chatId);
        IUser GetUser(long chatId);
    }
}
