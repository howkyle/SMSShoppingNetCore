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
            List<Product> products;
            try
            {
                products = db.Products.ToList();
            }
            catch
            {
                products = null;
            }
           
            return products;
        }

        public Product GetProduct(int id)
        {
            Product prod;
            try
            {
                prod = db.Products.Single(p => p.Id == id);
            }catch{
                prod = null;
            }
           
           return prod;
        }
    }

    
}
