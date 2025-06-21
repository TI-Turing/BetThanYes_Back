using BetThanYes.Domain.DTOs.Request.User;
using BetThanYes.Domain.DTOs.Response.User;
using BetThanYes.Domain.Models;
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
        Task<UpdateUserResponse> UpdateAsync(UpdateUserDto dto);
        System.Threading.Tasks.Task DeleteAsync(Guid id);
        Task<User> GetUserByEmail(string email);
    }
}
