namespace StashBot.Module.Message.Sender
{
    internal interface IMessageSender
    {
        void SendMessage(long chatId, string message);
    }
}
