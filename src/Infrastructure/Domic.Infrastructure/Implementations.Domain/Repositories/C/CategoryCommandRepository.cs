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

    public Task ChangeAsync(Category entity, CancellationToken cancellationToken)
    {
        _sqlContext.Categories.Update(entity);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(Category entity, CancellationToken cancellationToken)
    {
        _sqlContext.Categories.Remove(entity);

        return Task.CompletedTask;
    }
}

//Query
public partial class CategoryCommandRepository
{
    public Task<Category> FindByIdAsync(object id, CancellationToken cancellationToken)
        => _sqlContext.Categories.FirstOrDefaultAsync(category => category.Id.Equals(id), cancellationToken);

    public Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken) 
        =>  _sqlContext.Categories.AnyAsync(category => category.Name.Value == name);

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
}