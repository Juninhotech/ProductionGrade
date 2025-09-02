using ProductionGrade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<IEnumerable<Order>> GetByCustomerEmailAsync(string email);
    }
}
