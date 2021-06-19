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
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Model.Category>
    {
        
		public void Configure(EntityTypeBuilder<Category> builder)
		{
            builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


            builder.ToTable("Category", "dbo");

            builder.Property(e => e.CategoryId).ValueGeneratedNever();

            builder.HasKey(k => k.CategoryId);

            builder.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
        }
	}
}
