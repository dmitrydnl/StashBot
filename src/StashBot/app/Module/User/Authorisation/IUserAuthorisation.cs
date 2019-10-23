namespace StashBot.Module.User.Authorisation
{
    internal interface IUserAuthorisation
    {
        bool LoginUser(long chatId, string password);
        void LogoutUser(long chatId);
    }
}
