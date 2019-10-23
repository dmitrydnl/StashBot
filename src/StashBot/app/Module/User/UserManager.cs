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

        public void CreateNewUser(long chatId, string password)
        {
            userRegistration.CreateNewUser(chatId, password);
        }

        public bool LoginUser(long chatId, string password)
        {
            return userAuthorisation.LoginUser(chatId, password);
        }

        public void LogoutUser(long chatId)
        {
            userAuthorisation.LogoutUser(chatId);
        }
    }
}
