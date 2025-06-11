using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        +
        public ScheduleService(IScheduleRepository scheduleRepository) // Constructor
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule> CreateAsync(CreateScheduleDto request)
        {
            var objResult = await _scheduleRepository.CreateAsync(request);
            
            return objResult;
        }
    }
}
