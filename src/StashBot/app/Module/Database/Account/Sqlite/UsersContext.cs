using Microsoft.EntityFrameworkCore;

namespace StashBot.Module.Database.Account.Sqlite
{
    public class UsersContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
        }
    }
}
