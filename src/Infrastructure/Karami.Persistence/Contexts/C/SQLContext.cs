using Karami.Core.Domain.Entities;
using Karami.Core.Persistence.Configs;
using Karami.Domain.Category.Entities;
using Karami.Persistence.Configs.C;
using Microsoft.EntityFrameworkCore;

namespace Karami.Persistence.Contexts.C;

/*Setting*/
public partial class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options)
    {
        
    }
}

/*Entity*/
public partial class SQLContext
{
    public DbSet<Event> Events        { get; set; }
    public DbSet<Category> Categories { get; set; }
}

/*Config*/
public partial class SQLContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new EventConfig());
        builder.ApplyConfiguration(new CategoryConfig());
    }
}