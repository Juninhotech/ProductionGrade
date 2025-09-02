using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

}
