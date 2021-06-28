using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Infrastructure
{
	public class ProductEntityConfiguration : IEntityTypeConfiguration<Model.Product>
	{
		public void Configure(EntityTypeBuilder<Model.Product> builder)
		{
			builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


			builder.ToTable("Products", "dbo");

			builder.Property(e => e.ProductId).ValueGeneratedNever();

			builder.HasKey(k => k.ProductId);

			builder.Property(e => e.Description)
					.IsRequired()
					.HasColumnType("text");

			builder.Property(e => e.Price).HasColumnType("money");

			builder.Property(e => e.ProductName)
					.IsRequired()
					.HasMaxLength(500);

			builder.Property(e => e.AvailableQuantity);

			builder.Property(e => e.Image)
						.HasMaxLength(500);

			builder.Property(e => e.MultiVariants).HasColumnName("MultiVariantProduct");

			builder.HasOne(d => d.Category)
					.WithMany(p => p.Products)
					.HasForeignKey(d => d.CategoryId);


		}
	}
}
