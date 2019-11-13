namespace StashBot.Module.Database.Account.Sqlite
{
    public class UserModel
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string HashPassword { get; set; }
    }
}
