namespace StashBot.Module.Database.Account
{
    internal interface IDatabaseAccount
    {
        void CreateNewUser(long chatId, string authCode);
        bool IsUserExist(long chatId);
        bool ValidateUserAuthCode(long chatId, string authCode);
        IUser GetUser(long chatId);
        void AuthorizeUser(long chatId, string authCode);
    }
}
