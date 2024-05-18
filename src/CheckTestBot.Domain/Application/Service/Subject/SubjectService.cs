using CheckTestBot.Domain.Application.Abstruction;
using CheckTestBot.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CheckTestBot.Domain.Application.Service.Subject
{
    public class SubjectService : ISubjectService
    {
        private readonly IApplicationDbContext _context;

        public SubjectService(IApplicationDbContext context)
        
           => _context = context;       
            
        
        public async ValueTask<Subjects> AddAsync(Subjects subject)
        {
            var storageSubject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == subject.Id);
            if(storageSubject == null)
            {
                var entry = await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            else
            {
                return storageSubject;
            }
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var storageSubject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            if(storageSubject == null)
            {
                return false;
            }
            else
            {
                _context.Subjects.Remove(storageSubject);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async ValueTask<Subjects> GetSubjectAsync(int id)
        {
            var storageSubject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            return storageSubject;
        }

        public async ValueTask<Subjects> UpdateAsync(Subjects subject, int id)
        {
            var storageSubject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            if(storageSubject == null)
            {
                return null;
            }
            else
            {
                storageSubject.SubjectName = subject.SubjectName;
                var entry=_context.Subjects.Update(storageSubject);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
        }
    }
}
