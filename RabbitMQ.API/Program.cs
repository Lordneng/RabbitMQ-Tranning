using MassTransit;
using RabbitMQ.Models.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
var eventBusSettingText = builder.Configuration.GetSection("EventBusSettings").GetSection("HostAddress").Value;
var orderCreatedEvent = builder.Configuration.GetSection("Queue").GetSection("OrderCreated").Value;

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(eventBusSettingText);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
