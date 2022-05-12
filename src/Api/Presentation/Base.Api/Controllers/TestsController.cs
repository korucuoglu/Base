using Microsoft.AspNetCore.Mvc;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Controllers;

public class TestsController : BaseApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Result(Response<NoContent>.Success("Success", 200));
    }
}