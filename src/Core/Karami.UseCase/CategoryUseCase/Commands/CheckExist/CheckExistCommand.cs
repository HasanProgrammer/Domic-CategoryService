using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.CheckExist;

public class CheckExistCommand : IQuery<bool>
{
    public required string CategoryId { get; set; }
}