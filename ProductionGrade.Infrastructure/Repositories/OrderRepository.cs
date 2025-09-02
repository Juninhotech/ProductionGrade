using Microsoft.EntityFrameworkCore;
using ProductionGrade.Core.Entities;
using ProductionGrade.Core.Interfaces;
using ProductionGrade.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(x => x.OrderItems).ThenInclude(y => y.Product).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetByCustomerEmailAsync(string email)
        {
            return await _context.Orders.Include(x => x.OrderItems).ThenInclude(y => y.Product).Where(x => x.CustomerEmail == email).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}
