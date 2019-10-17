namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal interface IChatStateHandler
    {
        void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context);
    }
}
