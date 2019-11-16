namespace StashBot.Module.Session
{
    public interface IChatSession
    {
        long ChatId
        {
            get;
        }

        ChatSessionState State
        {
            get;
            set;
        }

        void UserSentMessage(int messageId);
        void BotSentMessage(int messageId);
        void Kill();
        bool IsNeedKill();
    }
}
