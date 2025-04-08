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
                Id = routine.Id,
                RoutineNumber = routine.RoutineNumber
            };
        }

        //public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        //public async Task<IEnumerable<RoutineResponse>> GetAllByUserIdAsync(Guid userId)
        //{
        //    var routines = await _repository.GetAllByUserIdAsync(userId);

        //    return routines.Select(r => new RoutineResponse
        //    {
        //        Id = r.Id,
        //        RoutineNumber = r.RoutineNumber,
        //        UserId = r.UserId,
        //        Name = r.RoutineName,
        //        Description = r.Description,
        //        CreatedAt = r.CreatedAt
        //    });
        //}

        //public async Task<RoutineResponse?> GetByIdAsync(Guid id)
        //{
        //    var routine = await _repository.GetByIdAsync(id);
        //    if (routine == null) return null;

        //    return new RoutineResponse
        //    {
        //        Id = routine.Id,
        //        RoutineNumber = routine.RoutineNumber,
        //        UserId = routine.UserId,
        //        Name = routine.RoutineName,
        //        Description = routine.Description,
        //        CreatedAt = routine.CreatedAt
        //    };
        //}

        //public async Task UpdateAsync(UpdateRoutineDto dto)
        //{
        //    var routine = await _repository.GetByIdAsync(dto.Id);
        //    if (routine == null) return;

        //    routine.RoutineName = dto.Name;
        //    routine.Description = dto.Description;

        //    await _repository.UpdateAsync(routine);
        //}
    }
}
