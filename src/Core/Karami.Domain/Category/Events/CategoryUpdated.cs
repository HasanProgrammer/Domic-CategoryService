﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.Category.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Category_Category_Exchange)]
public class CategoryUpdated : UpdateDomainEvent
{
    public string Id   { get; set; }
    public string Name { get; set; }
}