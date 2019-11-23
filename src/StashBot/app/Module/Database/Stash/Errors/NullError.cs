namespace StashBot.Module.Database.Stash.Errors
{
    internal class NullError : IDatabaseError
    {
        public void Handle(long chatId)
        {
            // Do nothing
        }
    }
}
