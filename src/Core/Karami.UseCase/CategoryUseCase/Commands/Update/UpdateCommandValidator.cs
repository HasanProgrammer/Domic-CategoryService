using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.Category.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Update;

public class UpdateCommandValidator : IValidator<UpdateCommand>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public UpdateCommandValidator(ICategoryCommandRepository categoryCommandRepository) 
        => _categoryCommandRepository = categoryCommandRepository; 

    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var targetCategory = await _categoryCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (targetCategory is null)
            throw new UseCaseException( string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id) );

        if (!targetCategory.Name.Value.Equals(input.Name))
            if (await _categoryCommandRepository.FindByNameAsync(input.Name, cancellationToken) is not null)
                throw new UseCaseException("نام مورد نظر شما قبلا انتخاب شده است !");

        return targetCategory;
    }
}