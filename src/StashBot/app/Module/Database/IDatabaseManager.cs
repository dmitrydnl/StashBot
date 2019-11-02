using StashBot.Module.Database.Account;
using StashBot.Module.Database.Stash;

namespace StashBot.Module.Database
{
    internal interface IDatabaseManager :
        IDatabaseAccount,
        IDatabaseStash
    {

    }
}
