using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be valid")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
