using System;

namespace BetThanYes.Application.DTOs.Response.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public long UserNumber { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CustomMotivationalQuote { get; set; }
        public string? TimeZone { get; set; }
        public DateTime? LastLogin { get; set; }
        public int MotivationScore { get; set; }
    }
}
