using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Category.Contracts.Interfaces;

public interface ICategoryCommandRepository : ICommandRepository<Entities.Category, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken);
}