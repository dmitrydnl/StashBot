namespace StashBot.Module.ChatSession
{
    internal interface ISession
    {
        void UserSentMessage(int messageId);
        void BotSentMessage(int messageId);
    }
}
