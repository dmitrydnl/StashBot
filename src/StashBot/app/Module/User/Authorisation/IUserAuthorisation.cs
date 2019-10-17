namespace StashBot.Module.User.Authorisation
{
    internal interface IUserAuthorisation
    {
        bool LoginUser(long chatId, string authCode);
        void LogoutUser(long chatId);
    }
}
