using Newtonsoft.Json;

namespace StashBot.GetWorkData
{
    internal class BotToken
    {
        private const string botTokenFileName = "workData.json";
        private static string botToken;

        internal BotToken()
        {
            if (string.IsNullOrEmpty(botToken))
            {
                string text = TextFileIO.Read(botTokenFileName);
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(text);
                botToken = (string)jsonObject.botToken;
            }
        }

        internal string Get()
        {
            return botToken;
        }
    }
}
