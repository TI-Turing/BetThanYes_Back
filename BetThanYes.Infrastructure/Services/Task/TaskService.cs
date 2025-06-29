using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            using var connection = await _dbContext.CreateConnectionAsync();
            await connection.ExecuteAsync(sql, objInsert);
            return objInsert.Id;
        }
                            
        public async Task<List<Domain.Models.Task>> GetAsync()
        {
            const string sql = @"
                SELECT * FROM Task
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
            var result = await connection.QueryAsync<Domain.Models.Task>(sql);
            return result.ToList();
        }
    }
}
