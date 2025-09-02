using ProductionGrade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Dictionary<int, int>> GetStockQuantitiesAsync(IEnumerable<int> productIds);
        Task UpdateStockAsync(Dictionary<int, int> stockUpdates);
    }
}
