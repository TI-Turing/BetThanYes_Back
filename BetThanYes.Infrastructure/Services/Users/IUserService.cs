using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllByUserIdAsync(Guid userId);
        System.Threading.Tasks.Task AddAsync(User User);
        System.Threading.Tasks.Task UpdateAsync(User User);
        System.Threading.Tasks.Task DeleteAsync(Guid id);
    }
}
