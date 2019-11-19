using NUnit.Framework;
using StashBot.Module.Database;
using StashBot.Module.Database.Account.Sqlite;

namespace StashBotTest.Database.Account.Sqlite
{
    public class DatabaseAccountSqliteTest
    {
        private DatabaseAccountSqlite databaseAccount;

        [SetUp]
        public void Setup()
        {
            databaseAccount = new DatabaseAccountSqlite();
        }

        [Test]
        public void CreateUserTest()
        {
            const int chatId = 1;
            const string password = "super_secure_password";
            databaseAccount.CreateUser(chatId, password);
            bool exist = databaseAccount.IsUserExist(chatId);
            Assert.AreEqual(exist, true);
        }

        [Test]
        public void GetUserTest()
        {
            const int chatId = 12;
            const string password = "super_secure_password";
            databaseAccount.CreateUser(chatId, password);
            IUser user = databaseAccount.GetUser(chatId);
            Assert.AreNotEqual(user, null);
        }

        [Test]
        public void LoginUserTest()
        {
            const int chatId = 123;
            const string password = "super_secure_password";
            databaseAccount.CreateUser(chatId, password);
            IUser user1 = databaseAccount.GetUser(chatId);
            bool login1 = user1.IsAuthorized;
            databaseAccount.LoginUser(chatId, password);
            IUser user2 = databaseAccount.GetUser(chatId);
            bool login2 = user2.IsAuthorized;
            Assert.AreEqual(login1, false);
            Assert.AreEqual(login2, true);
        }

        [Test]
        public void LogoutUserTest()
        {
            const int chatId = 1234;
            const string password = "super_secure_password";
            databaseAccount.CreateUser(chatId, password);
            IUser user1 = databaseAccount.GetUser(chatId);
            bool login1 = user1.IsAuthorized;
            databaseAccount.LoginUser(chatId, password);
            IUser user2 = databaseAccount.GetUser(chatId);
            bool login2 = user2.IsAuthorized;
            databaseAccount.LogoutUser(chatId);
            IUser user3 = databaseAccount.GetUser(chatId);
            bool login3 = user3.IsAuthorized;
            Assert.AreEqual(login1, false);
            Assert.AreEqual(login2, true);
            Assert.AreEqual(login3, false);
        }
    }
}
