using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Message.Sender
{
    internal interface IMessageSender
    {
        Task SendTextMessage(long chatId, string message, ReplyKeyboardMarkup replyKeyboard = null);
        Task SendPhotoMessage(long chatId, byte[] imageBytes);
    }
}
