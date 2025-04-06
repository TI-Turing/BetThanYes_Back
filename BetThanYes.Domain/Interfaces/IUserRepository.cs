using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(User User);
        Task UpdateAsync(User User);
        Task DeleteAsync(Guid id);
    }
}
