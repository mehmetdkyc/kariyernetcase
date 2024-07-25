using ClientService.API.Consumers;
using ClientService.API.Middlewares;
using ClientService.Infrastructure;
using ClientService.Persistance;
using MassTransit;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options=> options.Filters.Add<ValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("PostgreSqlConnection")!);
builder.Services.AddHealthChecks();



builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CanCompanyShareJobEventConsumer>();
    configurator.AddConsumer<JobCannotCreatedEventConsumer>();
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host("rabbitmq://"+ builder.Configuration["RabbitMQ"], h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        _configurator.ReceiveEndpoint(RabbitMQSettings.JobCannotCreatedEventQueue, e => e.ConfigureConsumer<JobCannotCreatedEventConsumer>(context));
        _configurator.ReceiveEndpoint(RabbitMQSettings.CanCompanyShareJobEventQueue, e => e.ConfigureConsumer<CanCompanyShareJobEventConsumer>(context));
    });
});
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.BaseCustomErrorHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
