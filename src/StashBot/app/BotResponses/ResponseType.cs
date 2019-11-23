namespace StashBot.BotResponses
{
    internal enum ResponseType
    {
        WelcomeMessage,
        MainCommands,
        Information,
        RegistrationWarning,
        RegistrationReady,
        SuccessRegistration,
        PasswordEmpty,
        PasswordMinLength,
        PasswordMaxLength,
        PasswordCharacters,
        AuthorisationReady,
        SuccessAuthorisation,
        FailAuthorisation,
        Login,
        Logout,
        EmptyStash,
        FullStashError
    }
}
