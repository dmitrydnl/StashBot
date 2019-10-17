using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class RegistrationStateHandler : IChatStateHandler
    {
        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            if (string.Equals(message, "yes"))
            {
                string authCode = userManager.CreateNewUser(chatId);
                messageManager.SendTextMessage(chatId, SuccessMessage(authCode));
            }
            else
            {
                messageManager.SendTextMessage(chatId, CancelMessage());
            }

            context.ChangeChatState(new StartStateHandler());
        }

        private string SuccessMessage(string authCode)
        {
            return $"Registration success\nUse your code for auth:\n{authCode}";
        }

        private string CancelMessage()
        {
            return "Ok, cancel reg";
        }
    }
}
