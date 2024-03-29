﻿using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.Category.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Queries.ReadOne;

public class ReadOneQueryValidator : IValidator<ReadOneQuery>
{
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public ReadOneQueryValidator(ICategoryCommandRepository categoryCommandRepository) 
        => _categoryCommandRepository = categoryCommandRepository;

    public async Task<object> ValidateAsync(ReadOneQuery input, CancellationToken cancellationToken)
    {
        var result = await _categoryCommandRepository.FindByIdAsync(input.CategoryId, cancellationToken);
        
        if(result is null)
            throw new UseCaseException(
                string.Format("دسته بندی با شناسه {0} وجود خارجی ندارد !", input.CategoryId ?? "_خالی_")
            );

        return result;
    }
}