using System.IO;
using Newtonsoft.Json;

namespace StashBot.BotSettings
{
    internal static class StashSettings
    {
        private const string BOT_SETTINGS_FILE_NAME = "BotSettings.json";

        private static bool isSetUp;
        private static int stashMessageLimit;

        internal static int StashMessageLimit
        {
            get
            {
                SetUpSettings();
                return stashMessageLimit;
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
            stashMessageLimit = (int)jsonObject.stashMessageLimit;
            isSetUp = true;
        }
    }
}
