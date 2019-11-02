using System.Collections.Generic;
using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class ChatStateHandlerFactory : IChatStateHandlerFactory
    {
        private readonly Dictionary<ChatSessionState, IChatStateHandler> chatStateHandlers;

        internal ChatStateHandlerFactory()
        {
            chatStateHandlers =
                new Dictionary<ChatSessionState, IChatStateHandler>
            {
                {
                    ChatSessionState.FirstMessage,
                    new FirstMessageStateHandler()
                },
                {
                    ChatSessionState.Start,
                    new StartStateHandler()
                },
                {
                    ChatSessionState.Registration,
                    new RegistrationStateHandler()
                },
                {
                    ChatSessionState.CreateUserPassword,
                    new CreateUserPasswordStateHandler()
                },
                {
                    ChatSessionState.Authorisation,
                    new AuthorisationStateHandler()
                },
                {
                    ChatSessionState.Authorized,
                    new AuthorizedStateHandler()
                }
            };
        }

        public IChatStateHandler GetChatStateHandler(ChatSessionState chatSessionState)
        {
            return chatStateHandlers[chatSessionState];
        }
    }
}
