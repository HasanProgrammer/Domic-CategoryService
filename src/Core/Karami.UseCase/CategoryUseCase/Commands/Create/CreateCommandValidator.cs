using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.Category.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommandValidator : IValidator<CreateCommand>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public CreateCommandValidator(ICategoryCommandRepository categoryCommandRepository)
        => _categoryCommandRepository = categoryCommandRepository;

    public async Task<object> ValidateAsync(CreateCommand input, CancellationToken cancellationToken)
    {
        var result = await _categoryCommandRepository.FindByNameAsync(input.Name, cancellationToken);
        
        if (result is not null)
            throw new UseCaseException("فیلد نام قبلا انتخاب شده است !");

        return default;
    }
}