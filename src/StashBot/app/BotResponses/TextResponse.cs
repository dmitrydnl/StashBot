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
            return responses[responseType];
        }

        internal static void SetUpResponses()
        {
            if (responses != null)
            {
                return;
            }

            responses = new Dictionary<ResponseType, string>();
            string text = File.ReadAllText(BOT_RESPONSES_FILE_NAME);
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(text);

            responses.Add(ResponseType.WelcomeMessage, (string)jsonObject.welcomeMessage);
            responses.Add(ResponseType.MainCommands, (string)jsonObject.mainCommands);
            responses.Add(ResponseType.Information, (string)jsonObject.information);
            responses.Add(ResponseType.RegistrationWarning, (string)jsonObject.registrationWarning);
            responses.Add(ResponseType.RegistrationReady, (string)jsonObject.registrationReady);
            responses.Add(ResponseType.SuccessRegistration, (string)jsonObject.successRegistration);
            responses.Add(ResponseType.PasswordEmpty, (string)jsonObject.passwordEmpty);
            responses.Add(ResponseType.PasswordMinLength, (string)jsonObject.passwordMinLength);
            responses.Add(ResponseType.PasswordMaxLength, (string)jsonObject.passwordMaxLength);
            responses.Add(ResponseType.PasswordCharacters, (string)jsonObject.passwordCharacters);
            responses.Add(ResponseType.AuthorisationReady, (string)jsonObject.authorisationReady);
            responses.Add(ResponseType.SuccessAuthorisation, (string)jsonObject.successAuthorisation);
            responses.Add(ResponseType.FailAuthorisation, (string)jsonObject.failAuthorisation);
            responses.Add(ResponseType.Login, (string)jsonObject.login);
            responses.Add(ResponseType.Logout, (string)jsonObject.logout);
        }
    }
}
