//using BetThanYes.Domain.Entities;
using BetThanYes.Domain.Models;

namespace BetThanYes.Infrastructure.Services.Routines
{
    public interface IUserService
    {
        Task<IEnumerable<Routine>> GetAllAsync(Guid userId);
        Task<Routine?> GetByIdAsync(Guid id);
        Task CreateAsync(Routine routine);
        Task UpdateAsync(Routine routine);
        Task DeleteAsync(Guid id);
    }
}
