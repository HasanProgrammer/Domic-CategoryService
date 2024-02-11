using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommand : ICommand<string>
{
    public string Token { get; set; }
    public string Id    { get; set; }
}