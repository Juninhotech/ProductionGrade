using ProductionGrade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductionGrade.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
