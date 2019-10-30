namespace StashBot.Module.Database
{
    internal interface IUser
    {
        long ChatId
        {
            get;
        }

        bool IsAuthorized
        {
            get;
        }

        string EncryptedPassword
        {
            get;
        }

        void Login(string password);
        void Logout();
        bool ValidatePassword(string password);
    }
}
