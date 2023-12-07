using Karami.Core.Domain.Enumerations;
using Karami.Domain.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.C;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);
        
        /*-----------------------------------------------------------*/
        
        builder.OwnsOne(category => category.Name, name => {
            name.Property(vo => vo.Value).IsRequired().HasMaxLength(100).HasColumnName("Name");
        });
        
        builder.OwnsOne(category => category.CreatedAt, createdAt => {
            createdAt.Property(vo => vo.EnglishDate).IsRequired().HasColumnName("CreatedAt_EnglishDate");
            createdAt.Property(vo => vo.PersianDate).IsRequired().HasColumnName("CreatedAt_PersianDate");
        });
        
        builder.OwnsOne(category => category.UpdatedAt, updatedAt => {
            updatedAt.Property(vo => vo.EnglishDate).IsRequired().HasColumnName("UpdatedAt_EnglishDate");
            updatedAt.Property(vo => vo.PersianDate).IsRequired().HasColumnName("UpdatedAt_PersianDate");
        });

        builder.Property(category => category.IsActive)
               .HasConversion(new EnumToNumberConverter<IsActive , int>())
               .IsRequired();
    }
}