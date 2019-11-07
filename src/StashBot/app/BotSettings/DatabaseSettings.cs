using System.IO;
using Newtonsoft.Json;

namespace StashBot.BotSettings
{
    internal static class DatabaseSettings
    {
        private const string BOT_SETTINGS_FILE_NAME = "BotSettings.json";

        private static bool isSetUp;
        private static DatabaseType accountDatabaseType;
        private static DatabaseType stashDatabaseType;

        internal static DatabaseType AccountDatabaseType
        {
            get
            {
                SetUpSettings();
                return accountDatabaseType;
            }
        }

        internal static DatabaseType StashDatabaseType
        {
            get
            {
                SetUpSettings();
                return stashDatabaseType;
            }
        }

        private static void SetUpSettings()
        {
            if (isSetUp)
            {
                return;
            }

            string text = File.ReadAllText(BOT_SETTINGS_FILE_NAME);
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(text);
            accountDatabaseType = StringToDatabaseType((string)jsonObject.accountDatabaseType);
            stashDatabaseType = StringToDatabaseType((string)jsonObject.stashDatabaseType);
            isSetUp = true;
        }

        private static DatabaseType StringToDatabaseType(string type)
        {
            switch(type)
            {
                case "local":
                    return DatabaseType.Local;
                case "sqlite":
                    return DatabaseType.Sqlite;
                default:
                    return DatabaseType.Local;
            }
        }
    }
}
