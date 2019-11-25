namespace StashBot.BotResponses
{
    internal enum ResponseType
    {
        MainCommands,
        Information,
        RegistrationWarning,
        RegistrationReady,
        PasswordEmpty,
        PasswordMinLength,
        PasswordMaxLength,
        PasswordCharacters,
        AuthorisationReady,
        Success,
        FailAuthorisation,
        Login,
        Logout,
        EmptyStash,
        FullStashError,
        UnsupportedMessageFormat
    }
}
