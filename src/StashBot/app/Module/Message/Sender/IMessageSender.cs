using System.Threading.Tasks;

namespace StashBot.Module.Message.Sender
{
    internal interface IMessageSender
    {
        Task SendTextMessage(long chatId, string message);
        Task SendPhotoMessage(long chatId, byte[] imageBytes);
    }
}
