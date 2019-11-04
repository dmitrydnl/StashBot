using StashBot.Module.Database;

namespace StashBot.Module.User.Registration
{
    internal class UserRegistration : IUserRegistration
    {
        public void CreateNewUser(long chatId, string password)
        {
            IDatabaseManager databaseManager = ModulesManager.GetModulesManager().GetDatabaseManager();

            databaseManager.CreateNewUser(chatId, password);
        }
    }
}
