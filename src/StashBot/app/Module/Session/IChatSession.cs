namespace StashBot.Module.Session
{
    internal interface IChatSession
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
        bool NeedKill();
    }
}
