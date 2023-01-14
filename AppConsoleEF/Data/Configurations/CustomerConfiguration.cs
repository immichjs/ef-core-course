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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(_ => _.Phone).HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(_ => _.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(_ => _.UF).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(_ => _.City).HasMaxLength(60).IsRequired();
            builder.HasIndex(_ => _.Phone).HasName("idx_customer_phone");
        }
    }
}
