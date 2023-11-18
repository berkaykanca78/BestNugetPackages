using BaseWebApp.Models;

namespace BaseWebApp.Services.Abstract
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void Add(Product product);
    }
}
