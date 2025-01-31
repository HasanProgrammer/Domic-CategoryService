using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.Category.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Commands.Update;

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
            if (await _categoryCommandRepository.IsExistByNameAsync(input.Name, cancellationToken))
                throw new UseCaseException("نام مورد نظر شما قبلا انتخاب شده است !");

        return targetCategory;
    }
}