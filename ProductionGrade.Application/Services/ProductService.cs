using ProductionGrade.Application.DTOs;
using ProductionGrade.Application.IServices;
using ProductionGrade.Application.Mappings;
using ProductionGrade.Core.Exceptions;
using ProductionGrade.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException(id);

            return ProductMapper.ToDto(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = ProductMapper.ToEntity(createProductDto);
            var createdProduct = await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return ProductMapper.ToDto(createdProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
                throw new ProductNotFoundException(id);

            ProductMapper.UpdateEntity(existingProduct, updateProductDto);
            existingProduct.UpdatedAt = DateTime.UtcNow;

            var updatedProduct = await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            return ProductMapper.ToDto(updatedProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var exists = await _unitOfWork.Products.ExistsAsync(id);
            if (!exists)
                throw new ProductNotFoundException(id);

            var result = await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
