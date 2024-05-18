using CheckTestBot.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CheckTestBot.Domain.Application.Abstruction
{
    public interface IApplicationDbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<TestQuestions> TestQuestions { get; set; }
        public DbSet<Sertificate> Sertificates { get; set; }

        public ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
