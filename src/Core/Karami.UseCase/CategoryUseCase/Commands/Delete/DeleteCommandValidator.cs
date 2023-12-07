using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.Category.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommandValidator : IValidator<DeleteCommand>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public DeleteCommandValidator(ICategoryCommandRepository categoryCommandRepository) 
        => _categoryCommandRepository = categoryCommandRepository;

    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var targetCategory = await _categoryCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (targetCategory is null)
            throw new UseCaseException( string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id) );

        return targetCategory;
    }
}