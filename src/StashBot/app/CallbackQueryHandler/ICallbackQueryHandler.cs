namespace StashBot.CallbackQueryHandler
{
    internal interface ICallbackQueryHandler
    {
        void Handle(string[] queryArray, int messageId);
    }
}
