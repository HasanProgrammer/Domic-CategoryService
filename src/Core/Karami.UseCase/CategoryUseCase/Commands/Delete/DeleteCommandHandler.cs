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
    
    private readonly IDotrisDateTime            _dotrisDateTime;
    private readonly ISerializer                _serializer;
    private readonly IJsonWebToken              _jsonWebToken;
    private readonly IEventCommandRepository    _eventCommandRepository;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public DeleteCommandHandler(ICategoryCommandRepository categoryCommandRepository, 
        IEventCommandRepository eventCommandRepository, 
        IDotrisDateTime dotrisDateTime,
        ISerializer serializer, 
        IJsonWebToken jsonWebToken
    )
    {
        _dotrisDateTime            = dotrisDateTime;
        _serializer                = serializer;
        _jsonWebToken                  = jsonWebToken;
        _eventCommandRepository    = eventCommandRepository;
        _categoryCommandRepository = categoryCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetCategory = _validationResult as Category;
        
        targetCategory.Delete(_dotrisDateTime, command.Id);
        
        _categoryCommandRepository.Change(targetCategory);

        #region OutBox

        var events = targetCategory.GetEvents.ToEntityOfEvent(_dotrisDateTime, _serializer, Service.CategoryService, 
            Table.Category, Action.Delete, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return targetCategory.Id;
    }
}