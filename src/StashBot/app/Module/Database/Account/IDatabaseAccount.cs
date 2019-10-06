namespace StashBot.Module.Database.Account
{
    internal interface IDatabaseAccount
    {
        void CreateNewUser(long chatId, string hashAuthCode);
        IUser GetUser(long chatId);
    }
}
