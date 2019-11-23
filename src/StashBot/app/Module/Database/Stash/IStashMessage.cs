using System.Threading.Tasks;

namespace StashBot.Module.Database.Stash
{
    public interface IStashMessage
    {
        long? DatabaseMessageId
        {
            get;
        }

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

        Task DownloadAsync();
        void Encrypt(IUser user);
        void Decrypt(IUser user);
        void Send();
    }
}
