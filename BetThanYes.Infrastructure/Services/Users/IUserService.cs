//using BetThanYes.Domain.Entities;
using BetThanYes.Domain.Models;

namespace BetThanYes.Infrastructure.Services.Users
{
    public interface IUserService
    {
        //Task<IEnumerable<User>> GetAllAsync(Guid userId);
        Task<User?> GetByIdAsync(Guid id);
        Task CreateAsync(User User);
        Task UpdateAsync(User User);
        Task DeleteAsync(Guid id);
    }
}
