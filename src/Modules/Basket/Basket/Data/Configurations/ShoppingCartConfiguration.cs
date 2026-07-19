using Basket.Basket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Data.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.ShoppingCartId);
        }
    }
}
