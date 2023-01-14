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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(_ => _.Status).HasConversion<string>();
            builder.Property(_ => _.FreightType).HasConversion<int>();
            builder.Property(_ => _.Observation).HasColumnType("VARCHAR(512)");

            builder.HasMany(_ => _.Items)
                .WithOne(_ => _.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
