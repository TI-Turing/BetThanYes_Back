namespace BetThanYes.Application.DTOs.Request.User;
public class CreateUserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // 👈 Esta sí debe llegar aquí
    
}
