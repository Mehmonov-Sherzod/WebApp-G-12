using WebApp.Application.Helpers.GenerateJwt;
using WebApp.Application.Helpers.PasswordHashers;
using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl;

public partial class UserService : IUserService
{
    private readonly AppDbContext _context;
    public readonly JwtService _jwtService;
    public readonly PasswordHelper _passwordHelper;
    public UserService(AppDbContext appContext, JwtService jwtService, PasswordHelper passwordHelper)
    {
        _context = appContext;
        _jwtService = jwtService;
        _passwordHelper = passwordHelper;
    }

    public Result<Guid> Create(CreateUserDTO createUserDTO)
    {
        string salt = Guid.NewGuid().ToString();
        string HashPass = _passwordHelper.Incrypt(createUserDTO.Password, salt);
        var result = new User
        {
            Email = createUserDTO.Email,
            Password = HashPass,
            Role = createUserDTO.Role,
            Salt = salt,
            Username = createUserDTO.Username
        };

        _context.Users.Add(result);
        _context.SaveChanges();

        var id = result.Id;

        return Result<Guid>.Succuss(id);
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

    public Result<UserResponseModel> GetUser(Guid id)
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
}
