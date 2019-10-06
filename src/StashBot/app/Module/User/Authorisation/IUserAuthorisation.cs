namespace StashBot.Module.User.Authorisation
{
    internal interface IUserAuthorisation
    {
        bool AuthorisationUser(long chatId, string authCode);
    }
}
