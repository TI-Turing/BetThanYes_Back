using BetThanYes.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(Guid id);
        //Task<IEnumerable<UserDto>> GetAllByUserIdAsync(Guid userId);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(UpdateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}
