using Base.Api.Application.Dtos;
using Base.Api.Application.Dtos.User;
using Base.Api.Application.Dtos.Wrappers;
using System.Threading.Tasks;

namespace Base.Api.Application.Interfaces.Services;

public interface IUserService
{
    Task<Response<string>> Login(LoginModel model);

    Task<Response<NoContent>> Register(RegisterModel model);

    Task<Response<NoContent>> ValidateUserEmail(string userId, string token);

    Task<Response<NoContent>> ResetPassword(ResetPasswordModel model);

    Task<Response<NoContent>> ResetPasswordConfirm(ResetPasswordConfirmModel model);

    Task<Response<NoContent>> Update(UpdateUserDto model);

    Task<Response<NoContent>> ChangePassword(ChangePasswordModel model);
}