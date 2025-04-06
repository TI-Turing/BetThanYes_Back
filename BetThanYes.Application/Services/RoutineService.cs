using BetThanYes.Application.DTOs.Routine;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Interfaces;
using BetThanYes.Domain.Models;
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

        public async Task<RoutineDto> CreateAsync(CreateRoutineDto dto)
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

            return new RoutineDto
            {
                Id = routine.Id,
                RoutineNumber = routine.RoutineNumber,
                UserId = routine.UserId,
                Name = routine.RoutineName,
                Description = routine.Description,
                CreatedAt = routine.CreatedAt
            };
        }

        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<RoutineDto>> GetAllByUserIdAsync(Guid userId)
        {
            var routines = await _repository.GetAllByUserIdAsync(userId);

            return routines.Select(r => new RoutineDto
            {
                Id = r.Id,
                RoutineNumber = r.RoutineNumber,
                UserId = r.UserId,
                Name = r.RoutineName,
                Description = r.Description,
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<RoutineDto?> GetByIdAsync(Guid id)
        {
            var routine = await _repository.GetByIdAsync(id);
            if (routine == null) return null;

            return new RoutineDto
            {
                Id = routine.Id,
                RoutineNumber = routine.RoutineNumber,
                UserId = routine.UserId,
                Name = routine.RoutineName,
                Description = routine.Description,
                CreatedAt = routine.CreatedAt
            };
        }

        public async Task UpdateAsync(UpdateRoutineDto dto)
        {
            var routine = await _repository.GetByIdAsync(dto.Id);
            if (routine == null) return;

            routine.RoutineName = dto.Name;
            routine.Description = dto.Description;

            await _repository.UpdateAsync(routine);
        }
    }
}
