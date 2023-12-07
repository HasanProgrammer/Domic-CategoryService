using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public string Token { get; set; }
    public string Id    { get; set; }
    public string Name  { get; set; }
}