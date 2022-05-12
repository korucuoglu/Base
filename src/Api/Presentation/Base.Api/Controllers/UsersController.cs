using Base.Api.Application.Dtos;
using Base.Api.Application.Dtos.User;
using Base.Api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Base.Api.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserDto model)
    {
        return Result(await _service.Update(model));
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword(ChangePasswordModel model)
    {
        return Result(await _service.ChangePassword(model));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        return Result(await _service.Login(model));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        return Result(await _service.Register(model));
    }

    [HttpGet("validate-mail")]
    public async Task<IActionResult> ValidateUserEmail(string userId, string token)
    {
        return Result(await _service.ValidateUserEmail(userId, token));
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        return Result(await _service.ResetPassword(model));
    }

    [HttpPost("resetpassword-confirm")]
    public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordConfirmModel model)
    {
        return Result(await _service.ResetPasswordConfirm(model));
    }
}