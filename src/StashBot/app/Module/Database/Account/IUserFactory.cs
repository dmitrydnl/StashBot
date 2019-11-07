namespace StashBot.Module.Database.Account
{
    internal interface IUserFactory
    {
        IUser Create(long chatId, string password);
    }
}
