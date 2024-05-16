using CheckTestBot.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CheckTestBot.Domain.Data
{
    public class BotDbContext : DbContext
    {

        public BotDbContext(DbContextOptions<BotDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Database.Migrate();

        }

        public virtual DbSet <Users> Users { get; set; }


    }
}
