using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Models.Entities;
using RabbitMQ.Models.Requests;

namespace RabbitMQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var orderCreated = new OrderCreated
            {
                Id = 1,
                ProductName = orderDto.ProductName,
                Quantity = orderDto.Quantity,
                Price = orderDto.Price
            };

            await _publishEndpoint.Publish<OrderCreated>(orderCreated);

            return Ok();
        }
    }
}
