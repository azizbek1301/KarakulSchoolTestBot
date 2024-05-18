using CheckTestBot.Domain.Common;

namespace CheckTestBot.Domain.Entites
{
    public class Users:Auiditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public string Contact {  get; set; }
    }
}
