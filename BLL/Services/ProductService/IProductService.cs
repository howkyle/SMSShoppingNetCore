using DAL.Models;
using System.Collections.Generic;

public interface IProductService
{
    List<Product> GetProducts();
    Product GetProduct(int id);

}