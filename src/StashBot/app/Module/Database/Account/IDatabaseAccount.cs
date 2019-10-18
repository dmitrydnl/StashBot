namespace StashBot.Module.Database.Account
{
    internal interface IDatabaseAccount
    {
        void CreateNewUser(long chatId, string authCode);
        bool IsUserExist(long chatId);
        IUser GetUser(long chatId);
    }
}
