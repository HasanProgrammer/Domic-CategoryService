using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.CategoryUseCase.DTOs.ViewModels;

namespace Karami.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<CategoriesViewModel>
{
    public string CategoryId { get; set; }
}