namespace StashBot.Module.Session
{
    internal interface ISessionManager
    {
        void CreateChatSession(long chatId);
        bool IsChatSessionExist(long chatId);
        IChatSession GetChatSession(long chatId);
        void KillChatSession(long chatId);
        void UserSentMessage(long chatId, int messageId);
        void BotSentMessage(long chatId, int messageId);
    }
}
