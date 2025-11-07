using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.User;
using WebApp.Application.Models.UserEmail;
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
        Result<int> Response = _userService.Create(UserDTO);

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

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
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

    [HttpPost("send-otp")]
    public IActionResult LoginAsync(UserSendOtp userSendOtp)
    {
        Result<bool> Response = _userService.SendOtp(userSendOtp);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(Response.Errors);
        }
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(UserEmailForgot userEmailForgot)
    {
        Result<string> Response = _userService.ForgotPassword(userEmailForgot);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        else
        {
            return BadRequest(Response.Errors);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)


    {
        Result<string> Response = _userService.DeleteUser(id);

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
