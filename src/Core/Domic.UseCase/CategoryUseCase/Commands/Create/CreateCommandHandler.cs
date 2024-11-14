using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;

namespace Domic.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly ICategoryCommandRepository _categoryCommandRepository;
    private readonly IGlobalUniqueIdGenerator   _idGenerator;

    public CreateCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        IJsonWebToken jsonWebToken, ISerializer serializer, IGlobalUniqueIdGenerator idGenerator
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _jsonWebToken              = jsonWebToken;
        _categoryCommandRepository = categoryCommandRepository;
        _idGenerator               = idGenerator;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var createdBy   = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        var newCategory = new Category(_dateTime, _idGenerator.GetRandom(), createdBy, createdRole, command.Name);

        //ToDo : ( Tech Debt ) -> Should be used [Add] insted [AddAsync]
        await _categoryCommandRepository.AddAsync(newCategory, cancellationToken);

        return newCategory.Id;
    }

    public Task AfterTransactionHandleAsync(CreateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}