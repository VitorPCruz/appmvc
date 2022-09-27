using System.Net;
using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(product => product.Description)
            .IsRequired()
            .HasColumnType("varchar(1000)");

        builder.Property(product => product.Image)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Products");
    }
}
