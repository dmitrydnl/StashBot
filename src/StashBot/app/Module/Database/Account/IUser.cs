namespace StashBot.Module.Database
{
    internal interface IUser
    {
        string EncryptedPassword
        {
            get;
        }

        bool IsAuthorized
        {
            get;
        }

        void Login(string password);
        void Logout();
        bool ValidatePassword(string password);
    }
}
