using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;

namespace BaseWebApp.Services.Concrete
{
    public class ProductService : IProductService
    {
        List<Product> _products;
        public ProductService()
        {
            _products = new List<Product>();
            _products.Add(new Product { Id = 1, Name = "Ürün 1", Price = 1000 });
            _products.Add(new Product { Id = 2, Name = "Ürün 2", Price = 2000 });
            _products.Add(new Product { Id = 3, Name = "Ürün 3", Price = 3000 });
            _products.Add(new Product { Id = 4, Name = "Ürün 4", Price = 4000 });
            _products.Add(new Product { Id = 5, Name = "Ürün 5", Price = 5000 });
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public Product GetProductById(int id)
        {
            return _products.Find(x => x.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}
