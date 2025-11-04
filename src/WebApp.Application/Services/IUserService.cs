using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.Application.Models.UserEmail;

namespace WebApp.Application.Services;

public interface IUserService
{
    Result<int> Create(CreateUserDTO createUserDTO);
    Result<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
    Result<PaginationResult<UserListResponseModel>> GetAll(PaginationOption model);
    Result<UserResponseModel> GetUser(int id);
    Result<int> UpdateUser(UpdateUserModel updateUserModel, int Id);

    Result<string> UodateUserPassword(UserUpdatePassword userUpdatePassword, int Id);

    Result<bool> SendOtp(UserSendOtp userSendOtp);

    Result<string> ForgotPassword(UserEmailForgot userEmailForgot);

    Result<string> DeleteUser(int Id);
}
