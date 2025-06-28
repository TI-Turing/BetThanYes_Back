using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Request.Task;

namespace BetThanYes.Infrastructure.Services.Task
{
    public class TaskRepository : ITaskRepository
    {
            private readonly SqlDbContext _dbContext;

        public TaskRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
      
          
        public async Task<Guid> AddAsync(CreateTaskDto objTask)

        {
            
            Domain.Models.Task objInsert = new Domain.Models.Task();
            objInsert.Title = objTask.Title;
            objInsert.Hour = objTask.Hour;
            objInsert.RoutineId = objTask.RoutineId;
            objInsert.Id = Guid.NewGuid();
            
            const string sql = @"
                INSERT INTO Task (Id, Title, Hour, RoutineId)
                VALUES (@Id, @Title, @Hour, @RoutineId);
            ";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql,objInsert);
            return objInsert.Id;
        }

    }
}
