using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommand : ICommand<string>
{
    public string Token { get; set; }
    public string Id    { get; set; }
}