using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.DTOs.ViewModels;

namespace Domic.UseCase.CategoryUseCase.Caches;

public class CategoriesMemoryCache : IMemoryCacheSetter<List<CategoriesViewModel>>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public CategoriesMemoryCache(ICategoryCommandRepository categoryCommandRepository)
        => _categoryCommandRepository = categoryCommandRepository;

    [Config(Key = "Categories", Ttl = 60*24)]
    public async Task<List<CategoriesViewModel>> SetAsync(CancellationToken cancellationToken)
    {
        var result = await _categoryCommandRepository.FindAllWithOrderingAsync(Order.Date, false, cancellationToken);

        return result.Select(category => new CategoriesViewModel {
            Id   = category.Id,
            Name = category.Name.Value
        }).ToList();
    }
}