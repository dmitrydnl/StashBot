using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorisationStateHandler : IChatStateHandler
    {
        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            bool success = userManager.AuthorisationUser(chatId, message);
            if (success)
            {
                messageManager.SendTextMessage(chatId, SuccessMessage());
                context.ChangeChatState(new AuthorizedStateHandler());
            }
            else
            {
                messageManager.SendTextMessage(chatId, FailMessage());
                context.ChangeChatState(new StartStateHandler());
            }
        }

        private string SuccessMessage()
        {
            return "Authorisation success";
        }

        private string FailMessage()
        {
            return "Authorisation fail";
        }
    }
}
