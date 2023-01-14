using AppConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleEF.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Quantity).HasDefaultValue(1).IsRequired();
            builder.Property(_ => _.Price).IsRequired();
            builder.Property(_ => _.Discount).IsRequired();
        }
    }
}
