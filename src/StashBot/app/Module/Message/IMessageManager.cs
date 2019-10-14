using StashBot.Module.Message.Handler;
using StashBot.Module.Message.Sender;
using StashBot.Module.Message.Delete;

namespace StashBot.Module.Message
{
    internal interface IMessageManager : IMessageDelete, IMessageHandler, IMessageSender
    {
        void SendWelcomeMessage(long chatId);
        void SendRegistrationSuccessMessage(long chatId, string authCode);
        void SendAuthorisationSuccessMessage(long chatId);
        void SendAuthorisationFailMessage(long chatId);
    }
}
