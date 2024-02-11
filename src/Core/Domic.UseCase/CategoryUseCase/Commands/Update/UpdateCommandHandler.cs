#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;

namespace Domic.UseCase.CategoryUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public UpdateCommandHandler(ICategoryCommandRepository categoryCommandRepository, IDateTime dateTime, 
        ISerializer serializer, IJsonWebToken jsonWebToken
    )
    {
        _serializer                = serializer;
        _jsonWebToken              = jsonWebToken;
        _dateTime                  = dateTime;
        _categoryCommandRepository = categoryCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;
        var updatedBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetCategory.Change(_dateTime, updatedBy, updatedRole, command.Name);
        
        _categoryCommandRepository.Change(targetCategory);

        return Task.FromResult(targetCategory.Id);
    }
}