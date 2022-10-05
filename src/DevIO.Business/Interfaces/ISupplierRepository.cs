using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAddress(Guid supplierId);
        Task<Supplier> GetSupplierProductsAddress(Guid supplierId);
        Task RemoveSupplier(Guid id);
    }
}
