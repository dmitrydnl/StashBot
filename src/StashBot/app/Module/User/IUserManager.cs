namespace StashBot.Module.User
{
    internal interface IUserManager
    {
        string CreateNewUser(long chatId);
        bool AuthorisationUser(long chatId, string authCode);
    }
}
