using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Two_C_Two_P.Core.Entities;

namespace Two_C_Two_P.Infrastructure.Configuration
{
    internal class TransactionConfiguration : EntityTypeConfigurationBase<Transaction, string>
    {
        protected override void ConfigureInternal(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(e => e.Ammount).HasColumnType("decimal(19, 4)");

            builder.Property(e => e.Currency)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
