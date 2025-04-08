using BetThanYes.Domain.DTOs.Request.Routine;
using BetThanYes.Domain.DTOs.Response.Routine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IRoutineService
    {
        //Task<RoutineResponse?> GetByIdAsync(Guid id);
        //Task<IEnumerable<RoutineResponse>> GetAllByUserIdAsync(Guid userId);
        Task<CreateRoutineResponse> CreateAsync(CreateRoutineDto dto);
        //Task UpdateAsync(UpdateRoutineResponse dto);
        //Task DeleteAsync(Guid id);
    }
}
