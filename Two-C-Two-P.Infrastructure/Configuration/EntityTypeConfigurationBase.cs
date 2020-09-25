using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Two_C_Two_P.Core.Entities;

namespace Two_C_Two_P.Infrastructure.Configuration
{
    internal abstract class EntityTypeConfigurationBase<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase<TKey>
    {
        public const string NewId = "(newid())";

        protected abstract void ConfigureInternal(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(TKey) == typeof(Guid))
            {
                builder.Property(entity => entity.Id)
                .HasDefaultValueSql(NewId);
            }

            builder.Property(entity => entity.Status)
                .IsRequired();

            builder.Property(entity => entity.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(entity => entity.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(entity => entity.CreatedDate)
                .IsRequired();

            builder.Property(entity => entity.ModifiedDate)
                .IsRequired();

            ConfigureInternal(builder);
        }
    }
}
