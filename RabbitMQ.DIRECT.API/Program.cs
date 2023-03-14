using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.DIRECT.API.Consumers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var orderCreatedEvent = builder.Configuration.GetSection("Queue").GetSection("OrderCreated").Value;
var eventBusSettingText = builder.Configuration.GetSection("EventBusSettings").GetSection("HostAddress").Value;

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(eventBusSettingText);

        cfg.ReceiveEndpoint(orderCreatedEvent, c => {
            c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
        });

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
