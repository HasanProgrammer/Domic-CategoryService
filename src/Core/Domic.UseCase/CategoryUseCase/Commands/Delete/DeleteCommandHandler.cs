#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;
    
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IIdentityUser              _identityUser;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public DeleteCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        ISerializer serializer, [FromKeyedServices("Http2")] IIdentityUser identityUser
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _identityUser              = identityUser;
        _categoryCommandRepository = categoryCommandRepository;
    }

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "Categories")]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;
        
        targetCategory.Delete(_dateTime, _identityUser, _serializer, command.Id);
        
        await _categoryCommandRepository.ChangeAsync(targetCategory, cancellationToken);

        return targetCategory.Id;
    }

    public Task AfterHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}