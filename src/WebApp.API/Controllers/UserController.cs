using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        Result<Guid> Response = _userService.Create(UserDTO);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(Response.Errors);
        }


    }


    [HttpPost("get-all")]
    public IActionResult GetAll([FromBody] PaginationOption model)
    {
        Result<PaginationResult<UserListResponseModel>> response = _userService.GetAll(model);

        if (response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(response.Errors);
        }
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        Result<UserResponseModel> response = _userService.GetUser(id);

        if (response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(response.Errors);
        }
    }

    [HttpPost("Login")]
    public  IActionResult LoginAsync(LoginUserModel loginUserModel)
    {
        Result<LoginResponseModel> Response = _userService.LoginAsync(loginUserModel);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(Response.Errors);
        }
    }
}
