namespace StashBot.Module.Session
{
    internal enum ChatSessionState
    {
        FirstMessage,
        Start,
        Registration,
        CreateUserPassword,
        Authorisation,
        Authorized
    }
}
