using StashBot.Module.Database;

namespace StashBot.Module.User.Authorisation
{
    internal class UserAuthorisation : IUserAuthorisation
    {
        public bool LoginUser(long chatId, string password)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();

            return databaseManager.LoginUser(chatId, password);
        }

        public void LogoutUser(long chatId)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();

            databaseManager.LogoutUser(chatId);
        }
    }
}
