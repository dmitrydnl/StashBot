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

            bool success = userManager.LoginUser(chatId, message);
            if (success)
            {
                messageManager.SendMessage(chatId, SuccessMessage());
                context.ChangeChatState(new AuthorizedStateHandler());
            }
            else
            {
                messageManager.SendMessage(chatId, FailMessage());
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
