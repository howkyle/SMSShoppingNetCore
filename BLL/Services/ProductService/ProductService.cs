using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext db;
        public ProductService(ApplicationDBContext context)
        {
            db = context;
        }

        public List<Product> GetProducts()
        {
            var products = db.Products.ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
           var  prod = db.Products.Single(p => p.Id == id);
           return prod;
        }
    }

    
}
