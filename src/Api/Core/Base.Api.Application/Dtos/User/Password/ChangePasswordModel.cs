﻿namespace Base.Api.Application.Dtos.User;

public class ChangePasswordModel
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}