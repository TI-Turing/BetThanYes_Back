using BetThanYes.Domain.DTOs.Request.Routine;
using BetThanYes.Domain.DTOs.Response.Routine;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Services.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _repository;


        public RoutineService(IRoutineRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateRoutineResponse> CreateAsync(CreateRoutineDto dto)
        {
            var routine = new Routine
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                RoutineName = dto.Name,
                IsDefault = dto.IsDefault,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(routine);

            return new CreateRoutineResponse
            {
                Id = routine.Id
            };
        }

        public async System.Threading.Tasks.Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        public async System.Threading.Tasks.Task<IEnumerable<Routine>> GetAllByUserIdAsync(Guid userId)
        {
            var routines = await _repository.GetAllByUserIdAsync(userId);

            return routines.Select(r => new Routine
            {
                Id = r.Id,
                RoutineNumber = r.RoutineNumber,
                UserId = r.UserId,
                RoutineName = r.RoutineName,
                IsDefault = r.IsDefault,
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<RoutineResponse?> GetByIdAsync(Guid id)
        {
            var routine = await _repository.GetByIdAsync(id);
            if (routine == null) return null;

            return new RoutineResponse
            {
                Id = routine.Id,
                RoutineNumber = routine.RoutineNumber,
                UserId = routine.UserId,
                Name = routine.RoutineName,
               
                CreatedAt = routine.CreatedAt
            };
        }

        public async Task<UpdateRoutineResponse> UpdateAsync(UpdateRoutineDto dto)
        {
            var routine = await _repository.GetByIdAsync(dto.Id);

            if (routine == null)
                return new UpdateRoutineResponse { Success = false };

            Routine updateRoutine = new Routine
            {
                Id = routine.Id,
                RoutineNumber = routine.RoutineNumber,
                UserId = routine.UserId,
                RoutineName = dto.Name ?? routine.RoutineName, // Si dto.Name es null, se mantiene routine.RoutineName
                IsDefault = dto.IsDefault ?? routine.IsDefault, // Si dto.IsDefault es null, se mantiene routine.IsDefault
                CreatedAt = routine.CreatedAt
            };

            var response = await _repository.UpdateAsync(updateRoutine);


            return new UpdateRoutineResponse { Success = response };
        }
    }
}
