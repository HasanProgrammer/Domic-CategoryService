using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.DTOs.ViewModels;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<CategoriesViewModel>
{
    public string CategoryId { get; set; }
}