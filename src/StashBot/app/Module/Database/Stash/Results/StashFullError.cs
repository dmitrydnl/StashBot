using StashBot.Module.Message;
using StashBot.BotResponses;

namespace StashBot.Module.Database.Stash.Results
{
    internal class StashFullError : IDatabaseResult
    {
        public void Handle(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.FullStashError), null);
        }
    }
}
