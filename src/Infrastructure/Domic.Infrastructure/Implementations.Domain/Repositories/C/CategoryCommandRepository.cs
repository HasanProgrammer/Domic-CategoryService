using Domic.Core.Domain.Enumerations;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

//Config
public partial class CategoryCommandRepository : ICategoryCommandRepository
{
    private readonly SQLContext _sqlContext;

    public CategoryCommandRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class CategoryCommandRepository
{
    public async Task AddAsync(Category entity, CancellationToken cancellationToken)
        => await _sqlContext.Categories.AddAsync(entity, cancellationToken);

    public void Change(Category entity) => _sqlContext.Categories.Update(entity);

    public void Remove(Category entity) => _sqlContext.Categories.Remove(entity);
}

//Query
public partial class CategoryCommandRepository
{
    public async Task<Category> FindByIdAsync(object id, CancellationToken cancellationToken)
        => await _sqlContext.Categories.FirstOrDefaultAsync(category => category.Id.Equals(id), cancellationToken);

    public async Task<IEnumerable<Category>> FindAllWithOrderingAsync(Order order, bool accending, 
        CancellationToken cancellationToken
    )
    {
        var query = _sqlContext.Categories.AsNoTracking();
        
        return order switch {
            Order.Date => await query.OrderBy(category => category.CreatedAt.EnglishDate).ToListAsync(cancellationToken),
            Order.Id   => await query.OrderBy(category => category.Id).ToListAsync(cancellationToken),
            _ => null
        };
    }

    public async Task<Category> FindByNameAsync(string name, CancellationToken cancellationToken)
        => await _sqlContext.Categories.FirstOrDefaultAsync(category => category.Name.Value.Equals(name));
}