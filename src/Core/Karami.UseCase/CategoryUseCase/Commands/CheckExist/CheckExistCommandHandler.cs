using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.CheckExist;

public class CheckExistCommandHandler : IQueryHandler<CheckExistCommand, bool>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public CheckExistCommandHandler(ICategoryCommandRepository categoryCommandRepository) 
        => _categoryCommandRepository = categoryCommandRepository;

    public async Task<bool> HandleAsync(CheckExistCommand command, CancellationToken cancellationToken)
    {
        var result = await _categoryCommandRepository.FindByIdAsync(command.CategoryId, cancellationToken);

        return result is not null;
    }
}