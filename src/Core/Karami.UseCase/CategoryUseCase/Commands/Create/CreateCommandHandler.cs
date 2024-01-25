using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;
using Karami.Domain.Category.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly IDateTime                  _dateTime;
    private readonly ISerializer                _serializer;
    private readonly IEventCommandRepository    _eventCommandRepository;
    private readonly ICategoryCommandRepository _categoryCommandRepository;
    private readonly IGlobalUniqueIdGenerator   _idGenerator;

    public CreateCommandHandler(ICategoryCommandRepository categoryCommandRepository, 
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, IJsonWebToken jsonWebToken, 
        ISerializer serializer, IGlobalUniqueIdGenerator idGenerator
    )
    {
        _dateTime                  = dateTime;
        _serializer                = serializer;
        _jsonWebToken              = jsonWebToken;
        _eventCommandRepository    = eventCommandRepository;
        _categoryCommandRepository = categoryCommandRepository;
        _idGenerator               = idGenerator;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var createdBy = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        var newCategory = new Category(_dateTime, _idGenerator.GetRandom(), createdBy, createdRole, command.Name);

        //ToDo : ( Tech Debt ) -> Should be used [Add] insted [AddAsync]
        await _categoryCommandRepository.AddAsync(newCategory, cancellationToken);

        #region OutBox

        //ToDo : ( Tech Debt ) -> should be used [ Interceptor ]
        var events = newCategory.GetEvents.ToEntityOfEvent(_dateTime, _serializer, Service.CategoryService, 
            Table.Category, Action.Create, _jsonWebToken.GetUsername(command.Token)
        );

        //ToDo : ( Tech Debt ) -> Should be used [Add] insted [AddAsync]
        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return newCategory.Id;
    }
}