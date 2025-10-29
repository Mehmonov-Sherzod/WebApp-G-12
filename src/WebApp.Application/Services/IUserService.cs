using WebApp.Application.Models;
using WebApp.Application.Models.User;

namespace WebApp.Application.Services;

public interface IUserService
{
    Guid Create(CreateUserDTO createUserDTO);
    LoginResponseModel LoginAsync(LoginUserModel loginUserModel);
    PaginationResult<UserListResponseModel> GetAll(PaginationOption model);
    UserResponseModel GetUser(Guid id);
}
