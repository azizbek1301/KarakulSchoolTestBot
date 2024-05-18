using CheckTestBot.Domain.Entites;

namespace CheckTestBot.Domain.Application.Service.Subject
{
    public interface ISubjectService
    {
        ValueTask<Subjects> AddAsync(Subjects subject);
        ValueTask<Subjects> UpdateAsync(Subjects subject,int id);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<Subjects> GetSubjectAsync(int id);
    }
}
