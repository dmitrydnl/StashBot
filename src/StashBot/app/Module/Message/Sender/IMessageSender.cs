using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace StashBot.Module.Message.Sender
{
    internal interface IMessageSender
    {
        Task SendTextMessageAsync(long chatId, string message, IReplyMarkup replyMarkup);
        Task SendPhotoMessageAsync(long chatId, byte[] imageBytes, IReplyMarkup replyMarkup);
    }
}
