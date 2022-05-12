namespace Base.Api.Application.Dtos;

public class LoginResponseDto : TokenDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
}