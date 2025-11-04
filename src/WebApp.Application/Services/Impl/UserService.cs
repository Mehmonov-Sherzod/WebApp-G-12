using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Exceptions;
using WebApp.Application.Helpers.GenerateJwt;
using WebApp.Application.Helpers.PasswordHashers;
using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.Application.Models.UserEmail;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl;

public partial class UserService : IUserService
{
    private readonly AppDbContext _context;
    public readonly JwtService _jwtService;
    public readonly PasswordHelper _passwordHelper;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;

    public UserService(AppDbContext appContext, JwtService jwtService, PasswordHelper passwordHelper, IMapper mapper, IEmailService emailService, IOtpService otpService)
    {
        _context = appContext;
        _jwtService = jwtService;
        _passwordHelper = passwordHelper;
        _mapper = mapper;
        _emailService = emailService;
        _otpService = otpService;
    }

    public Result<int> Create(CreateUserDTO createUserDTO)
    {
        string salt = Guid.NewGuid().ToString();
        string HashPass = _passwordHelper.Incrypt(createUserDTO.Password, salt);

        var user = new User
        {
            Username = createUserDTO.Username,
            Email = createUserDTO.Email,
            Password = HashPass,
            Salt = salt,
            Role = createUserDTO.Role,

        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Result<int>.Succuss(user.Id);
    }

    public Result<PaginationResult<UserListResponseModel>> GetAll(PaginationOption model)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(model.Search))
        {
            query = query.Where(s => s.Username.Contains(model.Search));
        }

        if (!string.IsNullOrEmpty(model.Search))
        {
            query = query.Where(s => s.Username.Contains(model.Search));
        }

        //TODO: add filter for users lisgt based on user role

        List<UserListResponseModel> users = query
            .Skip(model.PageSize * (model.PageNumber - 1))
            .Take(model.PageSize)
            .Select(s => new UserListResponseModel
            {
                Id = s.Id,
                Username = s.Username
            })
            .ToList();

        int count = _context.Users.Count();

        var result =  new PaginationResult<UserListResponseModel>
        {
            Values = users,
            PageSize = model.PageSize,
            PageNumber = model.PageNumber,
            TotalCount = count
        };

        return Result<PaginationResult<UserListResponseModel>>.Succuss(result);
    }

    public Result<UserResponseModel> GetUser(int id)
    {
        UserResponseModel? User = _context.Users
            .Where(s => s.Id == id)
            .Select(s => new UserResponseModel
            {
                Id = s.Id,
                Username = s.Username
            })
            .FirstOrDefault();

        if (User == null)
        {
            return Result<UserResponseModel>.Failure(new List<string> { "bunday Id li User Yoq" });
        }

        return Result<UserResponseModel>.Succuss(User); ;
    }

    public Result<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == loginUserModel.Email);

        if (user == null)

            return Result<LoginResponseModel>.Failure(new List<string> { "Email Hato" });

        string token = _jwtService.Generate(user);

        var result = new  LoginResponseModel
        {
            Email = user.Email,
            Username = user.Username,
            Token = token,
        };

        return Result<LoginResponseModel>.Succuss(result);
    }

    public Result<int> UpdateUser(UpdateUserModel updateUserModel, int Id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == Id);

        if (user == null)
            return Result<int>.Failure(new List<string> { "bunday Emailga Ega User Yoq" });

        user.Username = updateUserModel.Username;
        user.Email = updateUserModel.Email;

        _context.SaveChanges();

        return Result<int>.Succuss(user.Id);

    }

    public Result<string> UodateUserPassword(UserUpdatePassword userUpdatePassword, int Id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == Id);

        if(_passwordHelper.Verify(userUpdatePassword.OldPassword, user.Salt, user.Password))
        {
            user.Password = _passwordHelper.Incrypt(userUpdatePassword.NewPassword, user.Salt);

            _context.Update(user);
            _context.SaveChangesAsync();
        }
        else
        {
            return Result<string>.Failure(new List<string> { "Eski Parol hato" });
        }

        return Result<string>.Succuss("O'zgardi");

    }

    public Result<bool> SendOtp(UserSendOtp userSendOtp)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == userSendOtp.Email);

        if (user == null)
            return Result<bool>.Failure(new List<string> { "Email Topilmadi" });

        var otp = _otpService.GenerateAndSaveOtpAsync(user.Id);

        _emailService.SendOtpAsync(user.Email, otp);

        return Result<bool>.Succuss(true);
    }

    public Result<string> ForgotPassword(UserEmailForgot userEmailForgot)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == userEmailForgot.Email);

        if (user == null)
            return Result<string>.Failure(new List<string> { "Email Topilmadi" });

        var UserOtp = _context.userOTPs
            .Where(x => x.Code == userEmailForgot.OtpCode)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();

        if (UserOtp.ExpiredAt < DateTime.UtcNow)
            throw new BadRequestException("Muddati tugagan");

        user.Password = _passwordHelper.Incrypt(userEmailForgot.NewPassword, user.Salt);

        _context.Update(user);
        _context.SaveChanges();

        return Result<string>.Succuss("Parol O'zgardi");

    }

    public Result<string> DeleteUser(int Id)
    {
        var user = _context.Users.FirstOrDefault(x => Id == Id);

        _context.Users.Remove(user);
         _context.SaveChanges();

        return Result<string>.Succuss("User O'chirildi");
    }
}
