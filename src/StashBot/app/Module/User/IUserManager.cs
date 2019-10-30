using StashBot.Module.User.Registration;
using StashBot.Module.User.Authorisation;

namespace StashBot.Module.User
{
    internal interface IUserManager :
        IUserRegistration,
        IUserAuthorisation
    {

    }
}
