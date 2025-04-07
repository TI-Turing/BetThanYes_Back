using BetThanYes.Application.DTOs.Request.User;
using BetThanYes.Application.DTOs.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse?> GetByIdAsync(Guid id);
        //Task<IEnumerable<UserDto>> GetAllByUserIdAsync(Guid userId);
        Task<CreateUserResponse> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(CreateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}
