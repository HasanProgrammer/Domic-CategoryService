using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly ICategoryCommandRepository _categoryCommandRepository;
    private readonly IGlobalUniqueIdGenerator   _idGenerator;
    private readonly IIdentityUser              _identityUser;

    public CreateCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        ISerializer serializer, IGlobalUniqueIdGenerator idGenerator, 
        [FromKeyedServices("Http2")] IIdentityUser identityUser
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _categoryCommandRepository = categoryCommandRepository;
        _idGenerator               = idGenerator;
        _identityUser              = identityUser;
    }

    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newCategory = new Category(_dateTime, _idGenerator, _identityUser, _serializer, command.Name);

        await _categoryCommandRepository.AddAsync(newCategory, cancellationToken);

        return newCategory.Id;
    }

    public Task AfterHandleAsync(CreateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}