using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public interface IInventoryService
    {
        List<Product> GetProducts();
        Product? GetProductById(int id);
        void SaveProduct(Product product);
        void DeleteProduct(int id);

        List<ProductCategory> GetCategories();
        void SaveCategory(ProductCategory category);

        List<Unit> GetUnits();
        void SaveUnit(Unit unit);

        List<Order> GetOrders();
        Order? GetOrderById(int id);
        void SaveOrder(Order order, List<OrderDetail> details);
    }
}

