using System;

namespace Base.Api.Application.Dtos;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}