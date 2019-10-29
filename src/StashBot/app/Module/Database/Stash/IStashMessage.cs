using System.Threading.Tasks;

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

        bool IsDownloaded
        {
            get;
        }

        string Message
        {
            get;
        }

        string Photo
        {
            get;
        }

        Task Download();
        void Encrypt(IUser user);
        void Decrypt(IUser user);
        void Send();
    }
}
