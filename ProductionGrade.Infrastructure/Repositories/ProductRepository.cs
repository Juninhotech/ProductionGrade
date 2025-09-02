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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<Dictionary<int, int>> GetStockQuantitiesAsync(IEnumerable<int> productIds)
        {
            return await _context.Products.Where(p => productIds.Contains(p.Id)).Select(p => new { p.Id, p.StockQuantity }).ToDictionaryAsync(p => p.Id, p => p.StockQuantity);
        }

        public async Task UpdateStockAsync(Dictionary<int, int> stockUpdates)
        {
            foreach (var update in stockUpdates)
            {
                var product = await _context.Products.FindAsync(update.Key);
                if (product != null)
                {
                    product.StockQuantity = update.Value;
                    product.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
