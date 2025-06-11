using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Schedule
{
    public interface IScheduleRepository
    {

        Task CreateAsync(CreateScheduleDto objSchedule);

    }
}
