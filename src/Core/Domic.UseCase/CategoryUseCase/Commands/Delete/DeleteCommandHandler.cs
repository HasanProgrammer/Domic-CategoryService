#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;

namespace Domic.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;
    
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public DeleteCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        ISerializer serializer, IJsonWebToken jsonWebToken
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _jsonWebToken              = jsonWebToken;
        _categoryCommandRepository = categoryCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;
        var updatedBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetCategory.Delete(_dateTime, command.Id, updatedBy, updatedRole);
        
        _categoryCommandRepository.Change(targetCategory);

        return Task.FromResult(targetCategory.Id);
    }
}