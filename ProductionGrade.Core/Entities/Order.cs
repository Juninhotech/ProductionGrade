using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerEmail { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderItem> OrderItems { get; set; } = new();
    }

  
}
