using ProductionGrade.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.IServices
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email);
    }
}
