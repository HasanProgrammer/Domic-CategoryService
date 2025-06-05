using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.DTOs;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : 
    IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<CategoryDto>>
{
    private readonly IInternalDistributedCacheMediator _distributedCacheMediator;

    public ReadAllPaginatedQueryHandler(IInternalDistributedCacheMediator distributedCacheMediator) 
        => _distributedCacheMediator = distributedCacheMediator;

    [WithValidation]
    public async Task<PaginatedCollection<CategoryDto>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var result = await _distributedCacheMediator.GetAsync<List<CategoryDto>>(cancellationToken);

        return result.ToPaginatedCollection(result.Count, query.CountPerPage ?? default(int),
            query.PageNumber ?? default(int)
        );
    }
}