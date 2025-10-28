using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext appContext)
    {
        _context = appContext;
    }

    public Guid Create(CreateUserDTO createUserDTO)
    {
        var result = new User
        {
            Password = createUserDTO.Password,
            Role = createUserDTO.Role,
            Salt = Guid.NewGuid().ToString(),
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
}
