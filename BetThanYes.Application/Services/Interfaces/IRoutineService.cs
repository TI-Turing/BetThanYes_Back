using BetThanYes.Domain.DTOs.Request.Routine;
using BetThanYes.Domain.DTOs.Response.Routine;
using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IRoutineService
    {
        Task<RoutineResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<Routine>> GetAllByUserIdAsync(Guid userId);
        Task<CreateRoutineResponse> CreateAsync(CreateRoutineDto dto);
        Task<UpdateRoutineResponse> UpdateAsync(UpdateRoutineDto dto);
        System.Threading.Tasks.Task DeleteAsync(Guid id);
    }
}
