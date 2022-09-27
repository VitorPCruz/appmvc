using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class SupplierMapping : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(supplier => supplier.Id);

        builder.Property(supplier => supplier.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(supplier => supplier.Document)
            .IsRequired()
            .HasColumnType("varchar(14)");

        // 1:1 => Supplier > Address
        builder.HasOne(supplier => supplier.Address)
            .WithOne(address => address.Supplier);

        // 1:N => Supplier > Products
        builder.HasMany(supplier => supplier.Products)
            .WithOne(product => product.Supplier)
            .HasForeignKey(product => product.SupplierId);

        builder.ToTable("Suppliers");
    }
}
