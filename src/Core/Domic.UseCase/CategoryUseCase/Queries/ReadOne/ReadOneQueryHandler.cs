#pragma warning disable CS0649

using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.UseCase.CategoryUseCase.DTOs;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, CategoryDto>
{
    private readonly object _validationResult;
    
    [WithValidation]
    public async Task<CategoryDto> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        var result = _validationResult as Category;

        return await Task.FromResult(new CategoryDto {
            Id   = result.Id,
            Name = result.Name.Value
        });
    }
}