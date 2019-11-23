namespace StashBot.Module.Database
{
    public interface IDatabaseError
    {
        void Handle(long chatId);
    }
}
