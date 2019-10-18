namespace StashBot.Module.Message.Sender
{
    internal interface IMessageSender
    {
        void SendTextMessage(long chatId, string textMessage);
    }
}
