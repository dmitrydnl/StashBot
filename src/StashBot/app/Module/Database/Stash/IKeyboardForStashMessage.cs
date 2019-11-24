using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Database.Stash
{
    internal interface IKeyboardForStashMessage
    {
        public InlineKeyboardMarkup ForTextMessage();
        public InlineKeyboardMarkup ForPhotoMessage();
    }
}
