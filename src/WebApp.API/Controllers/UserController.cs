using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.Application.Services;

namespace WebApp.API.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _UserService;
    public UserController(IUserService UserService)
    {
        _UserService = UserService;
    }

    [HttpPost]
    public IActionResult PostUser(CreateUserDTO UserDTO)
    {
        var id = _UserService.Create(UserDTO);

        return Ok(id);
    }

    [HttpPost("get-all")]
    public IActionResult GetAll([FromBody] PaginationOption model)
    {
        var result = _UserService.GetAll(model);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var result = _UserService.GetUser(id);

        return Ok(result);
    }
}
