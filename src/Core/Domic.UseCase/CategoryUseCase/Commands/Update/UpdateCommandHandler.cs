#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.CategoryUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IIdentityUser              _identityUser;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public UpdateCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        ISerializer serializer, [FromKeyedServices("Http1")] IIdentityUser identityUser
    )
    {
        _serializer                = serializer;
        _identityUser              = identityUser;
        _dateTime                  = dateTime;
        _categoryCommandRepository = categoryCommandRepository;
    }

    public Task BeforeHandleAsync(UpdateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;
        
        targetCategory.Change(_dateTime, _identityUser, _serializer, command.Name);
        
        await _categoryCommandRepository.ChangeAsync(targetCategory, cancellationToken);

        return targetCategory.Id;
    }

    public Task AfterHandleAsync(UpdateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}