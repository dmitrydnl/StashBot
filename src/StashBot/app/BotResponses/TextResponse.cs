using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StashBot.BotResponses
{
    internal static class TextResponse
    {
        private const string BOT_RESPONSES_FILE_NAME = "BotResponses.json";

        private static Dictionary<ResponseType, string> responses;

        internal static string Get(ResponseType responseType)
        {
            if (responses == null)
            {
                SetUpResponses();
            }
            return responses[responseType];
        }

        internal static void SetUpResponses()
        {
            if (responses != null)
            {
                return;
            }

            responses = new Dictionary<ResponseType, string>();
            string content = File.ReadAllText(BOT_RESPONSES_FILE_NAME);
            Dictionary<string, string> jsonResponses = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            foreach (var response in jsonResponses)
            {
                Enum.TryParse(response.Key, out ResponseType responseType);
                responses.Add(responseType, response.Value);
            }
        }
    }
}
