using StashBot.Module.Message;
using StashBot.Module.Secure;

namespace StashBot.Module.Database.Stash
{
    internal class StashMessage : IStashMessage
    {
        public long ChatId
        {
            get;
        }

        public bool IsEncrypt
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        internal StashMessage(ITelegramUserMessage telegramMessage)
        {
            ChatId = telegramMessage.ChatId;
            IsEncrypt = false;
            Message = telegramMessage.Message;
        }

        public void Encrypt(IUser user)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            if (IsEncrypt)
            {
                return;
            }

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            Message = secureManager.EncryptWithAesHmac(Message, password);
            IsEncrypt = true;
        }

        public void Decrypt(IUser user)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            if (!IsEncrypt)
            {
                return;
            }

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            Message = secureManager.DecryptWithAesHmac(Message, password);
            IsEncrypt = false;
        }

        public void Send()
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendMessage(ChatId, Message);
        }
    }
}
