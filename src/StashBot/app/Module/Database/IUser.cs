namespace StashBot.Module.Database
{
    internal interface IUser
    {
        void Login(string authCode);
        void Logout();
        bool ValidateAuthCode(string authCode);
        string EncryptMessage(string message);
        string DecryptMessage(string encryptedMessage);
    }
}
