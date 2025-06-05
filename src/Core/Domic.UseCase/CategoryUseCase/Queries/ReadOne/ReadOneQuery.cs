using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.DTOs;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<CategoryDto>
{
    public string CategoryId { get; set; }
}