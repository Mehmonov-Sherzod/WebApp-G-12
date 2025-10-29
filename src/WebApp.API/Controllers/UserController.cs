using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.Application.Services;
using WebApp.Application.Services.Impl;

namespace WebApp.API.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult PostUser(CreateUserDTO UserDTO)
    {
        var id = _userService.Create(UserDTO);

        return Ok(id);
    }

    [Authorize(Roles ="")]
    [HttpPost("get-all")]
    public IActionResult GetAll([FromBody] PaginationOption model)
    {
        var result = _userService.GetAll(model);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var result = _userService.GetUser(id);

        return Ok(result);
    }

    [HttpPost("Login")]
    public  IActionResult LoginAsync(LoginUserModel loginUserModel)
    {
        var result =  _userService.LoginAsync(loginUserModel);

        return Ok(result);
    }
}
