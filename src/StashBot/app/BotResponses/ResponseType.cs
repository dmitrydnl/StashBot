namespace StashBot.BotResponses
{
    internal enum ResponseType
    {
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
