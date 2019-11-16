namespace StashBot.Module.Database.Stash.Sqlite
{
    internal interface IStashMessageDatabaseModelConverter
    {
        StashMessageModel ToStashMessageModel();
        void FromStashMessageModel(StashMessageModel messageModel);
    }
}
