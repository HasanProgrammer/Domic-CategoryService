using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.DTOs.ViewModels;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<CategoriesViewModel>>
{
    private readonly ICacheService _cacheService;

    public ReadAllPaginatedQueryHandler(ICacheService cacheService) => _cacheService = cacheService;

    [WithValidation]
    public async Task<PaginatedCollection<CategoriesViewModel>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var result = await _cacheService.GetAsync<List<CategoriesViewModel>>(cancellationToken);

        return result.ToPaginatedCollection(result.Count, query.CountPerPage ?? default(int),
            query.PageNumber ?? default(int)
        );
    }
}