using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.NewPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasData(
                new Product { Id = 1, Name = "test", Description = "test",CategoryId=1, NewPrice = 12 }
                );
        }
    }
}
