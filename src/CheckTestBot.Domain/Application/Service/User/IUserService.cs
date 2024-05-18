using CheckTestBot.Domain.Entites;

namespace CheckTestBot.Domain.Application.Service.User
{
    public interface IUserService
    {
        public ValueTask<Users> AddAsync(Users user);
        public ValueTask<Users> UpdateUserPhoneNumberAsync(long userId, string phoneNumber);
        public ValueTask<Users> GetUserAsync(long userId);  
        public ValueTask<bool> DeleteAsync(long userId);
    }
}
