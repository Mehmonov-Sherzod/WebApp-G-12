namespace WebApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}
