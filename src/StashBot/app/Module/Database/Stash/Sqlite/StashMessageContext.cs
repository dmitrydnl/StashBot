using Microsoft.EntityFrameworkCore;

namespace StashBot.Module.Database.Stash.Sqlite
{
    public class StashMessageContext : DbContext
    {
        public DbSet<StashMessageModel> Stashes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=stashes.db");
        }
    }
}
