namespace StashBot.Module.Database.Stash.Sqlite
{
    public class StashMessageModel
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public StashMessageType Type { get; set; }
        public string Content { get; set; }
    }
}
