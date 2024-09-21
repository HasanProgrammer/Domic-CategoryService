using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Category.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = Broker.Category_Category_Exchange)]
public class CategoryUpdated : UpdateDomainEvent<string>
{
    public string Name { get; set; }
}