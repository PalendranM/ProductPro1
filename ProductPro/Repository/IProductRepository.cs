using ProductPro.Models;
using ProductPro.Repository;
using System.Linq.Expressions;

namespace productPro.Repository
{
    public interface IProductRepository: IRepository<Product>
    {
        //Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null);
        //Task<Product> Get(Expression<Func<Product, bool>> filter = null, bool tracked = true);
        //Task Create(Product entity);
        Task<Product> UpdateAsync(Product entity);
        //Task Remove(Product entity);
        //Task Save();



    }
}