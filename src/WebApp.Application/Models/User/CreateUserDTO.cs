using WebApp.Domain.Entities;

namespace WebApp.Application.Models.User;

public class CreateUserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
}
