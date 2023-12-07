#pragma warning disable CS0649

using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Entities;
using Karami.UseCase.CategoryUseCase.DTOs.ViewModels;

namespace Karami.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, CategoriesViewModel>
{
    private readonly object _validationResult;
    
    [WithValidation]
    public async Task<CategoriesViewModel> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        var result = _validationResult as Category;

        return await Task.FromResult(new CategoriesViewModel {
            Id   = result.Id,
            Name = result.Name.Value
        });
    }
}