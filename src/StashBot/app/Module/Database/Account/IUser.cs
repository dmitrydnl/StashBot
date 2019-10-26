namespace StashBot.Module.Database
{
    internal interface IUser
    {
        void Login(string password);
        void Logout();
        bool ValidatePassword(string password);
        string EncryptMessage(string secretMessage);
        string DecryptMessage(string encryptedMessage);
    }
}
