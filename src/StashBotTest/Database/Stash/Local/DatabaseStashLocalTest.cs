using NUnit.Framework;
using StashBot.Module.Database;
using StashBot.Module.Database.Stash.Local;

namespace StashBotTest.Database.Stash.Local
{
    public class DatabaseStashLocalTest
    {
        private DatabaseStashLocal databaseStash;

        [SetUp]
        public void Setup()
        {
            databaseStash = new DatabaseStashLocal();
        }
    }
}
