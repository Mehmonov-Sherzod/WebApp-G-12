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

    public Guid Create(CreateUserDTO createUserDTO)
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

        return result.Id;
    }
    public PaginationResult<UserListResponseModel> GetAll(PaginationOption model)
    {
        var query = _context.Users.AsQueryable();

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

        return new PaginationResult<UserListResponseModel>
        {
            Values = users,
            PageSize = model.PageSize,
            PageNumber = model.PageNumber,
            TotalCount = count
        };
    }
    public UserResponseModel GetUser(Guid id)
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
            throw new NotImplementedException();
        }

        return User;
    }
    public LoginResponseModel LoginAsync(LoginUserModel loginUserModel)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == loginUserModel.Email);

        if (user == null)
            throw new NotImplementedException("Username or Email is incorrect");

        string token = _jwtService.Generate(user);

        return new LoginResponseModel
        {
            Email = user.Email,
            Username = user.Username,
            Token = token,
        };
    }
}
