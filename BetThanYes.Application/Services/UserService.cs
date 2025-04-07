using BetThanYes.Application.DTOs.Request.User;
using BetThanYes.Application.DTOs.Response.User;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserDto dto)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "",
                    Email = dto.Email,
                    PasswordHash = dto.Password,
                    BirthDate = DateTime.Now,
                    Gender = "",
                    TimeZone = "",
                    ProfilePictureUrl = "",
                    CustomMotivationalQuote = "",
                    RegistrationDate = DateTime.UtcNow,
                    IsActive = true,
                    MotivationScore = 0
                };

                await _repository.AddAsync(user);

                return new CreateUserResponse
                {
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        //public async Task<IEnumerable<UserDto>> GetAllAsync()
        //{
        //    var users = await _repository.GetAllAsync();
        //    return users.Select(user => new UserDto
        //    {
        //        Id = user.Id,
        //        UserNumber = user.UserNumber,
        //        FullName = user.FullName,
        //        Email = user.Email,
        //        BirthDate = user.BirthDate,
        //        Gender = user.Gender,
        //        TimeZone = user.TimeZone,
        //        ProfilePictureUrl = user.ProfilePictureUrl,
        //        CustomMotivationalQuote = user.CustomMotivationalQuote,
        //        RegistrationDate = user.RegistrationDate,
        //        IsActive = user.IsActive,
        //        MotivationScore = user.MotivationScore
        //    });
        //}

        public async Task<CreateUserResponse?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new CreateUserResponse
            {
                Id = user.Id
                //UserNumber = user.UserNumber,
                //FullName = user.FullName,
                //Email = user.Email,
                //BirthDate = user.BirthDate,
                //Gender = user.Gender,
                //TimeZone = user.TimeZone,
                //ProfilePictureUrl = user.ProfilePictureUrl,
                //CustomMotivationalQuote = user.CustomMotivationalQuote,
                //RegistrationDate = user.RegistrationDate,
                //IsActive = user.IsActive,
                //MotivationScore = user.MotivationScore
            };
        }

        public async Task UpdateAsync(CreateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(new Guid());
            if (user == null) return;

            //user.FullName = dto.FullName;
            //user.BirthDate = dto.BirthDate;
            //user.Gender = dto.Gender;
            //user.TimeZone = dto.TimeZone;
            //user.ProfilePictureUrl = dto.ProfilePictureUrl;
            //user.CustomMotivationalQuote = dto.CustomMotivationalQuote;

            await _repository.UpdateAsync(user);
        }
    }
}
