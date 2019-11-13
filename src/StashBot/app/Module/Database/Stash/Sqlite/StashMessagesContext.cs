using Microsoft.EntityFrameworkCore;

namespace StashBot.Module.Database.Stash.Sqlite
{
    public class StashMessagesContext : DbContext
    {
        public DbSet<StashMessageModel> StashMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=stashMessages.db");
        }
    }
}
