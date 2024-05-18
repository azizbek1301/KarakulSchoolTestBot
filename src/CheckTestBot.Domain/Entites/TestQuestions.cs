using CheckTestBot.Domain.Common;

namespace CheckTestBot.Domain.Entites
{
    public class TestQuestions:Auiditable
    {
        public long Id { get; set; }
        public string TestKeys {  get; set; }
    }
}
