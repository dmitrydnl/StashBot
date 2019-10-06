namespace StashBot.Module.ChatSession
{
    internal interface ISessionsManager
    {
        void CreateSession(long chatId);
        bool ContainsSession(long chatId);
        ISession GetSession(long chatId);
        void UserSentMessage(long chatId, int messageId);
        void BotSentMessage(long chatId, int messageId);
    }
}
