namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        private readonly long chatId;
        private readonly string hashAuthCode;

        internal User(long chatId, string hashAuthCode)
        {
            this.chatId = chatId;
            this.hashAuthCode = hashAuthCode;
        }
    }
}
