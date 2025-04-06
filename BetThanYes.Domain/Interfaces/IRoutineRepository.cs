using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Domain.Interfaces
{
    public interface IRoutineRepository
    {
        Task<Routine?> GetByIdAsync(Guid id);
        Task<IEnumerable<Routine>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Routine routine);
        Task UpdateAsync(Routine routine);
        Task DeleteAsync(Guid id);
    }
}
