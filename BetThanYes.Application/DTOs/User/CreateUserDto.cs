public class CreateUserDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // 👈 Esta sí debe llegar aquí
    public DateTime? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? TimeZone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? CustomMotivationalQuote { get; set; }
}
