namespace StashBot.Module.Message
{
    internal interface IMessageManager
    {
        void UserSentTextMessage(long chatId, int messageId, string textMessage);
        void SendTextMessage(long chatId, string textMessage);
    }
}
