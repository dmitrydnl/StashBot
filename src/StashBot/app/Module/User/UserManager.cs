using StashBot.Module.User.Registration;

namespace StashBot.Module.User
{
    internal class UserManager : IUserManager
    {
        private readonly IUserRegistration userRegistration;

        internal UserManager()
        {
            userRegistration = new UserRegistration();
        }
    }
}
