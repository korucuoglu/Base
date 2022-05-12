using Base.Api.Application.Dtos;
using Base.Api.Application.Identity;

namespace Base.Api.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}