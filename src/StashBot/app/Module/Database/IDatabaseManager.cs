namespace StashBot.Module.Database
{
    internal interface IDatabaseManager
    {
        void CreateNewUser(long chatId, string hashAuthCode);
    }
}
