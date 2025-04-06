using BetThanYes.Application.DTOs.Routine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IRoutineService
    {
        Task<RoutineDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<RoutineDto>> GetAllByUserIdAsync(Guid userId);
        Task<RoutineDto> CreateAsync(CreateRoutineDto dto);
        Task UpdateAsync(UpdateRoutineDto dto);
        Task DeleteAsync(Guid id);
    }
}
