using ProductionGrade.Application.DTOs;
using ProductionGrade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Application.Mappings
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public static Product ToEntity(CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };
        }

        public static void UpdateEntity(Product product, UpdateProductDto dto)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
        }
    }


}
