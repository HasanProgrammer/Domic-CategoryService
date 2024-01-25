#pragma warning disable CS0649

using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;
using Karami.Domain.Category.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.CategoryUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;
    
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly IEventCommandRepository    _eventCommandRepository;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public DeleteCommandHandler(ICategoryCommandRepository categoryCommandRepository, 
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, ISerializer serializer, 
        IJsonWebToken jsonWebToken
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _jsonWebToken              = jsonWebToken;
        _eventCommandRepository    = eventCommandRepository;
        _categoryCommandRepository = categoryCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;

        var updatedBy = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetCategory.Delete(_dateTime, command.Id, updatedBy, updatedRole);
        
        _categoryCommandRepository.Change(targetCategory);

        #region OutBox

        var events = targetCategory.GetEvents.ToEntityOfEvent(_dateTime, _serializer, Service.CategoryService, 
            Table.Category, Action.Delete, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return targetCategory.Id;
    }
}