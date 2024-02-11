using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Category.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Category_Category_Exchange)]
public class CategoryCreated : CreateDomainEvent<string>
{
    public string Name { get; set; }
}