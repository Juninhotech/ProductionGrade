using Microsoft.Extensions.Logging;
using ProductionGrade.Application.DTOs;
using ProductionGrade.Application.IServices;
using ProductionGrade.Application.Mappings;
using ProductionGrade.Core.Entities;
using ProductionGrade.Core.Exceptions;
using ProductionGrade.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var productIds = createOrderDto.Items.Select(i => i.ProductId).ToList();

                var currentStocks = await _unitOfWork.Products.GetStockQuantitiesAsync(productIds);

                var stockUpdates = new Dictionary<int, int>();
                var orderItems = new List<OrderItem>();
                decimal totalAmount = 0;

                foreach (var item in createOrderDto.Items)
                {
                    if (!currentStocks.ContainsKey(item.ProductId))
                        throw new ProductNotFoundException(item.ProductId);

                    var availableStock = currentStocks[item.ProductId];
                    if (availableStock < item.Quantity)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                        throw new InsufficientStockException(product?.Name ?? "Unknown", item.Quantity, availableStock);
                    }

                    stockUpdates[item.ProductId] = availableStock - item.Quantity;

                    var productDetails = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    var itemTotal = productDetails!.Price * item.Quantity;
                    totalAmount += itemTotal;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = productDetails.Price
                    });
                }

                var order = new Order
                {
                    CustomerEmail = createOrderDto.CustomerEmail,
                    TotalAmount = totalAmount,
                    Status = OrderStatus.Confirmed,
                    OrderItems = orderItems
                };

                await _unitOfWork.Products.UpdateStockAsync(stockUpdates);

                var createdOrder = await _unitOfWork.Orders.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Order {OrderId} created successfully for customer {CustomerEmail}",
                createdOrder.Id, createOrderDto.CustomerEmail);

                return OrderMapper.ToDto(createdOrder);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                throw new OrderNotFoundException(id);

            return OrderMapper.ToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
        {
            var orders = await _unitOfWork.Orders.GetByCustomerEmailAsync(email);
            return orders.Select(OrderMapper.ToDto);
        }
    }
}
