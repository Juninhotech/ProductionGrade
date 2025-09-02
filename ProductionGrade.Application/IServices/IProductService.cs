using ProductionGrade.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
