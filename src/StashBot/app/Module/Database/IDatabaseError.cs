namespace StashBot.Module.Database
{
    public interface IDatabaseResult
    {
        void Handle(long chatId);
    }
}
