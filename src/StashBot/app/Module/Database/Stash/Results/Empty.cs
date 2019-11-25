namespace StashBot.Module.Database.Stash.Results
{
    internal class Empty : IDatabaseResult
    {
        public void Handle(long chatId)
        {
            // Do nothing
        }
    }
}
