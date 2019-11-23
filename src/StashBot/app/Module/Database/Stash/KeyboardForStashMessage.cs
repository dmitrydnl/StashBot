using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Database.Stash
{
    internal class KeyboardForStashMessage : IKeyboardForStashMessage
    {
        private readonly IStashMessage stashMessage;

        internal KeyboardForStashMessage(IStashMessage stashMessage)
        {
            this.stashMessage = stashMessage;
        }

        public InlineKeyboardMarkup ForTextMessage()
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Удалить", $"delete_message:{stashMessage.ChatId}:{stashMessage.DatabaseMessageId}")
            });

            return inlineKeyboardMarkup;
        }

        public InlineKeyboardMarkup ForPhotoMessage()
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Удалить", $"delete_message:{stashMessage.ChatId}:{stashMessage.DatabaseMessageId}")
            });

            return inlineKeyboardMarkup;
        }
    }
}
