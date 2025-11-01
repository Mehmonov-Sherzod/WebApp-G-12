using WebApp.Application.Models;
using WebApp.Application.Models.User;

namespace WebApp.Application.Services;

public interface IUserService
{
    Result<Guid> Create(CreateUserDTO createUserDTO);
    Result<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
    Result<PaginationResult<UserListResponseModel>> GetAll(PaginationOption model);
    Result<UserResponseModel> GetUser(Guid id);
}
