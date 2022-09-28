using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataDbContext context) : base(context) { }

        public async Task<Product> GetProductSupplier(Guid id)
        {
            return await Context.Products.AsNoTracking()
                .Include(supplier => supplier.Supplier)
                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsSupplier()
        {
            return await Context.Products.AsNoTracking()
                .Include(supplier => supplier.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await SearchEntity(product => product.SupplierId == supplierId);
        }
    }
}
