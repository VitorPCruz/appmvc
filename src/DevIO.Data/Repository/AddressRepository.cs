using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataDbContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await Context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(supplier => supplier.Id == supplierId);
        }
    }
}
