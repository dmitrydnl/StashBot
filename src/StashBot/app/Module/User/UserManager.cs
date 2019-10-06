using StashBot.Module.User.Registration;
using StashBot.Module.User.Authorisation;

namespace StashBot.Module.User
{
    internal class UserManager : IUserManager
    {
        private readonly IUserRegistration userRegistration;
        private readonly IUserAuthorisation userAuthorisation;

        internal UserManager()
        {
            userRegistration = new UserRegistration();
            userAuthorisation = new UserAuthorisation();
        }

        public string CreateNewUser(long chatId)
        {
            return userRegistration.CreateNewUser(chatId);
        }

        public bool AuthorisationUser(long chatId, string authCode)
        {
            return userAuthorisation.AuthorisationUser(chatId, authCode);
        }
    }
}
