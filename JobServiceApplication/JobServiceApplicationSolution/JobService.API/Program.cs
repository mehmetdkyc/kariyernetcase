using JobService.API;
using JobService.API.Middlewares;
using BusinessLayer;
using MassTransit;
using JobService.API.Consumers;
using EventShared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddBusinessLayerServices();
builder.Services.AddElastic(builder.Configuration);

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CompanyCanShareJobEventConsumer>();
    configurator.AddConsumer<CompanyDontHaveJobCountEventConsumer>();

    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host("rabbitmq://" + builder.Configuration["RabbitMQ"], h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        _configurator.ReceiveEndpoint(RabbitMQSettings.CompanyCanShareJobEvent, e => e.ConfigureConsumer<CompanyCanShareJobEventConsumer>(context));
        _configurator.ReceiveEndpoint(RabbitMQSettings.CompanyDontHaveJobCountEventQueue, e => e.ConfigureConsumer<CompanyDontHaveJobCountEventConsumer>(context)); 

    });
});
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExcepitonHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
