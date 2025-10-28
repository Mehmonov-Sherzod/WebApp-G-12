using WebApp.Domain.Entities;

namespace WebApp.Application.Models.User;

public class UserListResponseModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
}
