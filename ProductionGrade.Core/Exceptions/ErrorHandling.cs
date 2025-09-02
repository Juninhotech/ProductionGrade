using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionGrade.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
        protected DomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(string productName, int requested, int available) : base($"Insufficient stock for product '{productName}'. Requested: {requested}, Available: {available}")
        {

        }
    }

    public class ProductNotFoundException : DomainException
    {
        public ProductNotFoundException(int productId) : base($"Product with ID {productId} was not found")
        {

        }
    }

    public class OrderNotFoundException : DomainException
    {
        public OrderNotFoundException(int orderId) : base($"Order with ID {orderId} was not found")
        {

        }
    }
}
