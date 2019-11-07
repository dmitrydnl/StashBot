using System.IO;
using Newtonsoft.Json;

namespace StashBot.BotSettings
{
    internal static class ChatSessionSettings
    {
        private const string BOT_SETTINGS_FILE_NAME = "BotSettings.json";

        private static bool isSetUp;
        private static int chatSessionsClearInterval;
        private static int chatSessionLiveTime;

        internal static int ChatSessionsClearInterval
        {
            get
            {
                SetUpSettings();
                return chatSessionsClearInterval;
            }
        }

        internal static int ChatSessionLiveTime
        {
            get
            {
                SetUpSettings();
                return chatSessionLiveTime;
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
            chatSessionsClearInterval = (int)jsonObject.chatSessionsClearInterval;
            chatSessionLiveTime = (int)jsonObject.chatSessionLiveTime;
            isSetUp = true;
        }
    }
}
