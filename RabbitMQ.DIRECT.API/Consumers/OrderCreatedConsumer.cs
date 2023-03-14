using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Models.Entities;

namespace RabbitMQ.DIRECT.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation($"OrderCreated message: {jsonMessage}");
        }
    }
}
