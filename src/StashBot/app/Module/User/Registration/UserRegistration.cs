using StashBot.Module.Database;

namespace StashBot.Module.User.Registration
{
    internal class UserRegistration : IUserRegistration
    {
        public void CreateNewUser(long chatId, string password)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();

            if (databaseManager.IsStashExist(chatId))
            {
                databaseManager.ClearStash(chatId);
            }

            databaseManager.CreateUser(chatId, password);
        }
    }
}
