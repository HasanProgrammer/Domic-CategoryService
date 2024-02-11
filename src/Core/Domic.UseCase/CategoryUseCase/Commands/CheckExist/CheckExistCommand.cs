using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Commands.CheckExist;

public class CheckExistCommand : IQuery<bool>
{
    public required string CategoryId { get; set; }
}