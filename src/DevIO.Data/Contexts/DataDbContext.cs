using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Contexts;

public class DataDbContext : DbContext
{
	public DataDbContext(DbContextOptions options) : base(options) { }

	public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
