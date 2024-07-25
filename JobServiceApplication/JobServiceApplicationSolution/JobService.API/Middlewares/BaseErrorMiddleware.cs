﻿using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace JobService.API.Middlewares
{
    static public class BaseErrorMiddleware
    {

        public static void ConfigureExcepitonHandler(this WebApplication application)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature!.Error switch
                    {
                        NotFoundException => 404,
                        ClientSideException => 400,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = statusCode,
                        ErrorMessage = exceptionFeature.Error.Message
                    }));
                });
            });
        }
    }
}
