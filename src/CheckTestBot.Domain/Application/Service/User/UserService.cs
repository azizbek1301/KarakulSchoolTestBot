using CheckTestBot.Domain.Application.Abstruction;
using CheckTestBot.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CheckTestBot.Domain.Application.Service.User
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<Users> AddAsync(Users user)
        {
            var res=await _context.Users.FirstOrDefaultAsync(x=>x.Id == user.Id);
            if(res == null)
            {
                var entery= await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return entery.Entity;
            }
            return res;
            
        }

        public async ValueTask<bool> DeleteAsync(long userId)
        {
            var res = await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId);
            if(res == null)
            {
                return false;
            }
            _context.Users.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async ValueTask<Users> GetUserAsync(long userId)
        {
            var res= await _context.Users.FirstOrDefaultAsync(x=>x.Id== userId);
            return res;
        }

        public async ValueTask<Users> UpdateUserPhoneNumberAsync(long userId, string phoneNumber)
        {
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if( res == null)
            {
                return res;
            }
            else
            {
                res.Contact = phoneNumber;

                var entery=_context.Users.Update(res);
                await _context.SaveChangesAsync();
                return entery.Entity;
            }
        }
    }
}
