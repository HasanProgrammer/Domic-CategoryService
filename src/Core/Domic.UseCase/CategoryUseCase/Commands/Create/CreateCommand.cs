using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public string Name { get; set; }
}