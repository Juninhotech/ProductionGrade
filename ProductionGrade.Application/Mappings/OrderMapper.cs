using ProductionGrade.Application.DTOs;
using ProductionGrade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.Mappings
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerEmail = order.CustomerEmail,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(ToDto).ToList()
            };
        }

        public static OrderItemDto ToDto(OrderItem orderItem)
        {
            return new OrderItemDto
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.Product?.Name ?? "Unknown",
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice
            };
        }
    }
}
