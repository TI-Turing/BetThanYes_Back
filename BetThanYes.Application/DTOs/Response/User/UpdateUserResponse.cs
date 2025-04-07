using System;

namespace BetThanYes.Application.DTOs.Response.User
{
    public class UpdateUserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? TimeZone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CustomMotivationalQuote { get; set; }
    }
}
