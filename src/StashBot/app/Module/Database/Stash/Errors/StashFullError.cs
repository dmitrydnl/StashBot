using StashBot.Module.Message;
using StashBot.BotResponses;

namespace StashBot.Module.Database.Stash.Errors
{
    internal class StashFullError : IDatabaseError
    {
        public void Handle(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.FullStashError), null);
        }
    }
}
