﻿using Base.Api.Application.Dtos.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Base.Api.Application.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(opt =>
            {
                opt.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>().Error.Message;

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(Response<NoContent>.Fail(error));
                });
            });
        }
    }
}