﻿using Base.Api.Application.Dtos.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Base.Api.Controllers;

public class TestsController : BaseApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Result(Response<NoContent>.Success("Success", 200));
    }
}