using CheckTestBot.Domain.Common;

namespace CheckTestBot.Domain.Entites
{
    public class Sertificate:Auiditable
    {
        public long Id { get; set; }
        public string FilePath { get; set; }

        public long UserId { get; set; }

        public Users User { get; set; }
    }
}
