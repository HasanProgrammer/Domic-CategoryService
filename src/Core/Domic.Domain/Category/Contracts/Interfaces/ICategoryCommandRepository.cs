using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Category.Contracts.Interfaces;

public interface ICategoryCommandRepository : ICommandRepository<Entities.Category, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Entities.Category FindByName(string name) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Entities.Category> FindByNameAsync(string name, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
}