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
    private readonly IJsonWebToken                  _jsonWebToken;
    private readonly IDotrisDateTime            _dotrisDateTime;
    private readonly ISerializer                _serializer;
    private readonly IEventCommandRepository    _eventCommandRepository;
    private readonly ICategoryCommandRepository _categoryCommandRepository;

    public CreateCommandHandler(ICategoryCommandRepository categoryCommandRepository, 
        IEventCommandRepository eventCommandRepository,
        IDotrisDateTime dotrisDateTime,
        IJsonWebToken jsonWebToken,
        ISerializer serializer
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
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newCategory = new Category(_dotrisDateTime, Guid.NewGuid().ToString(), command.Name);

        await _categoryCommandRepository.AddAsync(newCategory, cancellationToken);

        #region OutBox

        var events = newCategory.GetEvents.ToEntityOfEvent(_dotrisDateTime, _serializer, Service.CategoryService, 
            Table.Category, Action.Create, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return newCategory.Id;
    }
}