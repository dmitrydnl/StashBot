namespace StashBot.Module.User.Registration
{
    internal interface IUserRegistration
    {
        void CreateNewUser(long chatId, string password);
    }
}
