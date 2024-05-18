using CheckTestBot.Domain.Application.Abstruction;
using CheckTestBot.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CheckTestBot.Domain.Data
{
    public class BotDbContext : DbContext, IApplicationDbContext
    {

        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            Database.Migrate();

        }

        public virtual DbSet<Users> Users { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<TestQuestions> TestQuestions { get; set; }
        public DbSet<Sertificate> Sertificates { get; set; }

        async ValueTask<int> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
