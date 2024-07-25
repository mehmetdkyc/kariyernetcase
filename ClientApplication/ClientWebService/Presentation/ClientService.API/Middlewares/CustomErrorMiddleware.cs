﻿using ClientService.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ClientService.API.Middlewares
{
    public static class CustomErrorMiddleware
    {
        public static void BaseCustomErrorHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature!.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode= statusCode;
                    var errorDto = new
                    {
                        StatusCode = statusCode,
                        ErrorMessage = exceptionFeature.Error.Message,
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorDto));
                });
            });
        }
    }
}
