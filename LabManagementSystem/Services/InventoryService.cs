using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly Func<LabContext> _contextFactory;

        public InventoryService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<Product> GetProducts()
        {
            using var db = _contextFactory();
            return db.Products
                .AsNoTracking()
                .Include(p => p.ProductCategory)
                .Include(p => p.BaseUnit)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public Product? GetProductById(int id)
        {
            using var db = _contextFactory();
            return db.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.BaseUnit)
                .FirstOrDefault(p => p.Id == id);
        }

        public void SaveProduct(Product product)
        {
            using var db = _contextFactory();
            if (product.Id == 0)
            {
                product.CreateDate = DateTime.Now;
                db.Products.Add(product);
            }
            else
            {
                var existing = db.Products.Find(product.Id);
                if (existing != null)
                {
                    existing.Name = product.Name;
                    existing.MinAmount = product.MinAmount;
                    existing.InitialAmount = product.InitialAmount;
                    existing.BaseUnitId = product.BaseUnitId;
                    existing.ProductCategoryId = product.ProductCategoryId;
                }
            }
            db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            using var db = _contextFactory();
            var prod = db.Products.Include(p => p.OrderDetails).Include(p => p.ProductUsages).FirstOrDefault(p => p.Id == id);
            if (prod == null) return;
            if (prod.OrderDetails.Any() || prod.ProductUsages.Any())
                throw new InvalidOperationException("لا يمكن حذف الصنف لوجود طلبات شراء أو استخدامات مسجلة له.");

            db.Products.Remove(prod);
            db.SaveChanges();
        }

        public List<ProductCategory> GetCategories()
        {
            using var db = _contextFactory();
            return db.ProductCategorys.AsNoTracking().OrderBy(c => c.Name).ToList();
        }

        public void SaveCategory(ProductCategory category)
        {
            using var db = _contextFactory();
            if (category.Id == 0)
                db.ProductCategorys.Add(category);
            else
            {
                var existing = db.ProductCategorys.Find(category.Id);
                if (existing != null)
                    existing.Name = category.Name;
            }
            db.SaveChanges();
        }

        public List<Unit> GetUnits()
        {
            using var db = _contextFactory();
            return db.Units.AsNoTracking().OrderBy(u => u.Name).ToList();
        }

        public void SaveUnit(Unit unit)
        {
            using var db = _contextFactory();
            if (unit.Id == 0)
                db.Units.Add(unit);
            else
            {
                var existing = db.Units.Find(unit.Id);
                if (existing != null)
                    existing.Name = unit.Name;
            }
            db.SaveChanges();
        }

        public List<Order> GetOrders()
        {
            using var db = _contextFactory();
            return db.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetOrderById(int id)
        {
            using var db = _contextFactory();
            return db.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Unit)
                .FirstOrDefault(o => o.Id == id);
        }

        public void SaveOrder(Order order, List<OrderDetail> details)
        {
            using var db = _contextFactory();
            using var tx = db.Database.BeginTransaction();
            try
            {
                decimal total = details.Sum(d => d.Price * d.Quantity);
                order.TotalPrice = total;
                order.NetPrice = total;
                order.CreateDate = DateTime.Now;

                if (order.Id == 0)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                else
                {
                    var existing = db.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.Id == order.Id);
                    if (existing != null)
                    {
                        existing.OrderDate = order.OrderDate;
                        existing.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
                        existing.Notes = order.Notes;
                        existing.TotalPrice = total;
                        existing.NetPrice = total;

                        db.OrderDetails.RemoveRange(existing.OrderDetails);
                    }
                }

                foreach (var d in details)
                {
                    d.OrderId = order.Id;
                    d.CreateDate = DateTime.Now;
                    db.OrderDetails.Add(d);
                }

                db.SaveChanges();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}

