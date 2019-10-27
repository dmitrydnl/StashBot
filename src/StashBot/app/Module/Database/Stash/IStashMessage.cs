namespace StashBot.Module.Database.Stash
{
    internal interface IStashMessage
    {
        long ChatId
        {
            get;
        }

        bool IsEncrypt
        {
            get;
        }

        string Message
        {
            get;
        }

        void Encrypt(IUser user);
        void Decrypt(IUser user);
        void Send();
    }
}
