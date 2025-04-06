using System;

namespace BetThanYes.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }                          // Clave primaria
        public long UserNumber { get; set; }                  // Número interno incremental
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
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
