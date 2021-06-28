using eShop.Products.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Infrastructure
{
	public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<Model.ProductVariant>
	{
		public void Configure(EntityTypeBuilder<ProductVariant> builder)
		{
			builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


			builder.ToTable("ProductVariants", "dbo");

			builder.Property(e => e.VariantId).ValueGeneratedNever();

			builder.Property(e => e.ProductId).ValueGeneratedNever();

			builder.HasKey(k => k.VariantId);

			builder.Property(e => e.VariantType)
					.IsRequired()
					.HasColumnType("text");

			builder.Property(e => e.VariantName)
					.IsRequired();


			builder.Property(e => e.VariantPrice).HasColumnType("money");


			builder.HasOne(d => d.Product)
					.WithMany(p => p.Variants)
					.HasForeignKey(d => d.ProductId);
		}
	}
}
