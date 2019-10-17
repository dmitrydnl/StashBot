namespace StashBot.Module.Session
{
    internal interface IChatSession
    {
        void UserSentMessage(int messageId);
        void BotSentMessage(int messageId);
        void Authorize();
        void Kill();
        bool NeedKill();
        long ChatId();
        bool IsAuthorized();
    }
}
