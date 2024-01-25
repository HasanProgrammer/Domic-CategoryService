using Karami.Core.Persistence.Configs;
using Karami.Domain.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.C;

public class CategoryConfig : BaseEntityConfig<Category, string>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("Categories");
        
        builder.OwnsOne(category => category.Name, name => {
            name.Property(vo => vo.Value).IsRequired().HasMaxLength(100).HasColumnName("Name");
        });
    }
}