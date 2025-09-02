using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductionGrade.Application.DTOs;
using ProductionGrade.Application.IServices;
using ProductionGrade.Core.Exceptions;

namespace ProductionGrade.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(createOrderDto);
                _logger.LogInformation("Order created with ID: {OrderId}", order.Id);

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InsufficientStockException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get an order by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (OrderNotFoundException)
            {
                return NotFound($"Order with ID {id} not found");
            }
        }

        /// <summary>
        /// Get orders by customer email
        /// </summary>
        [HttpGet("customer/{email}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomer(string email)
        {
            var orders = await _orderService.GetOrdersByCustomerEmailAsync(email);
            return Ok(orders);
        }
    }
}
