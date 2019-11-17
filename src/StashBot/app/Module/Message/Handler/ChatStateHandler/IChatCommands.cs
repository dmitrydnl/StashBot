using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal interface IChatCommands
    {
        delegate void Command(long chatId, IChatStateHandlerContext context);

        void Add(string name, Command command);
        Command Get(string name);
        bool ContainsCommand(string name);
        ReplyKeyboardMarkup CreateReplyKeyboard();
    }
}
