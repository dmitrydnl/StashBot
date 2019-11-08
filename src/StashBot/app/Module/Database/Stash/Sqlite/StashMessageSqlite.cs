using System;
using System.Threading.Tasks;

namespace StashBot.Module.Database.Stash.Sqlite
{
    internal class StashMessageSqlite : IStashMessage
    {
        public long ChatId
        {
            get;
            private set;
        }

        public bool IsEncrypt
        {
            get;
            private set;
        }

        public bool IsDownloaded
        {
            get;
            private set;
        }

        internal StashMessageSqlite()
        {

        }

        public void Decrypt(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task Download()
        {
            throw new NotImplementedException();
        }

        public void Encrypt(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Send()
        {
            throw new NotImplementedException();
        }
    }
}
