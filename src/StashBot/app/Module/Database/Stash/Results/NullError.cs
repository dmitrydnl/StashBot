namespace StashBot.Module.Database.Stash.Errors
{
    internal class NullError : IDatabaseResult
    {
        public void Handle(long chatId)
        {
            // Do nothing
        }
    }
}
