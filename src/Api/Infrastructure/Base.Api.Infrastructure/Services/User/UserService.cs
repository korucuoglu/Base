﻿using Base.Api.Application.Const;
using Base.Api.Application.Dtos;
using Base.Api.Application.Dtos.User;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Identity;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Services;
using Base.Common.Event;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Api.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;
    private readonly RabbitMQPublisher _rabbitMQPublisher;

    public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService, HashService hashService, RabbitMQPublisher rabbitMQPublisher, IIdentityService identityService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _hashService = hashService;
        _rabbitMQPublisher = rabbitMQPublisher;
        _identityService = identityService;
    }

    private Response<NoContent> SendValidateMail(ApplicationUser user, string link)
    {
        MailSendEvent userCreatedEvent = new()
        {
            MailAdress = user.Email,
            Message = $"<p>Mail adresini doğrulamak için <a href='{link}'>tıkla</a></p>",
            Subject = "Mail Onaylama"
        };

        _rabbitMQPublisher.Publish(userCreatedEvent);

        return Response<NoContent>.Success($"{user.Email} mail adresine doğrulama maili gönderildi", 201);
    }

    public async Task<Response<string>> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Response<string>.Fail(CustomResponseMessages.UserNotFound, 500);
        }

        if (!user.EmailConfirmed)
        {
            return Response<string>.Fail(CustomResponseMessages.EmailConfirmedError, 500);
        }

        var token = _tokenService.CreateToken(user);

        return Response<string>.Success(data: token, 200);
    }

    public async Task<Response<NoContent>> Register(RegisterModel model)
    {
        var user = new ApplicationUser()
        {
            UserName = model.Username,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        async Task<Response<NoContent>> GetResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                return Response<NoContent>.Fail(message: result.Errors.Select(x => x.Description).First(), 500);
            }

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string encodedConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            string link = $"{_identityService.AppBaseUrl}/users/validate-mail?userId={_hashService.Encode(user.Id)}&token={encodedConfirmationToken}";

            return SendValidateMail(user, link);
        }

        return await GetResult(result);
    }

    public async Task<Response<NoContent>> ValidateUserEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(_hashService.Decode(userId).ToString());

        async Task<Response<NoContent>> GetResult(ApplicationUser user)
        {
            if (user == null)
            {
                return Response<NoContent>.Fail(CustomResponseMessages.InvalidUrl, 404);
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                var error = result.Errors.Select(x => x.Description).First();

                return Response<NoContent>.Fail(message: error, 500);
            }

            return Response<NoContent>.Success($"{user.Email} {CustomResponseMessages.EmailConfirmed}", 200);
        }

        return await GetResult(user);
    }

    public async Task<Response<NoContent>> ResetPassword(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        async Task<Response<NoContent>> GetResult(ApplicationUser user)
        {
            if (user == null)
            {
                return Response<NoContent>.Fail(CustomResponseMessages.UserNotFound, 500);
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            string link = $"{_identityService.AppBaseUrl}/user/reset-passwordConfirm?userId={_hashService.Encode(user.Id)}&token={encodedToken}";

            SendValidateMail(user, link);

            return Response<NoContent>.Success($"{user.Email} ${CustomResponseMessages.EmailSended}", 200);
        }

        return await GetResult(user);
    }

    public async Task<Response<NoContent>> ResetPasswordConfirm(ResetPasswordConfirmModel model)
    {
        var user = await _userManager.FindByIdAsync(_hashService.Decode(model.UserId).ToString());

        async Task<Response<NoContent>> GetResult(ApplicationUser user)
        {
            if (user == null)
            {
                return Response<NoContent>.Fail(CustomResponseMessages.UserNotFound, 500);
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));

            IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (!result.Succeeded)
            {
                var error = result.Errors.First().Description;

                return Response<NoContent>.Fail(error, 500);
            }

            return Response<NoContent>.Success(CustomResponseMessages.PasswordChangedConfirm, 200);
        }

        return await GetResult(user);
    }

    public async Task<Response<NoContent>> Update(UpdateUserDto model)
    {
        var user = await _userManager.FindByIdAsync(_identityService.GetUserDecodeId.ToString());

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Response<NoContent>.Fail(CustomResponseMessages.UserNotFound, 500);
        }

        user.UserName = model.UserName;

        var message = CustomResponseMessages.UserInfoChangedConfirm;

        if (user.Email.ToLower() != model.Email.ToLower())
        {
            user.Email = model.Email;
            user.EmailConfirmed = false;

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string encodedConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            string link = $"{_identityService.AppBaseUrl}/users/validate-mail?userId={_hashService.Encode(user.Id)}&token={encodedConfirmationToken}";

            SendValidateMail(user, link);

            message = $"Güncelleme başarılı. {user.Email} {CustomResponseMessages.EmailSended}";
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var error = result.Errors.First().Description;

            return Response<NoContent>.Fail(error, 500);
        }

        return Response<NoContent>.Success(message: message, statusCode: 200);
    }

    public async Task<Response<NoContent>> ChangePassword(ChangePasswordModel model)
    {
        var user = await _userManager.FindByIdAsync(_identityService.GetUserDecodeId.ToString());

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
        {
            return Response<NoContent>.Fail(CustomResponseMessages.UserNotFound, 500);
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
            var error = result.Errors.First().Description;

            return Response<NoContent>.Fail(error, 500);
        }

        return Response<NoContent>.Success(CustomResponseMessages.PasswordChangedConfirm, 204);
    }
}