﻿using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Exceptions;

namespace Domic.Domain.Category.ValueObjects;

public class Name : ValueObject
{
    public readonly string Value;

    /// <summary>
    /// 
    /// </summary>
    public Name() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new DomainException("فیلد نام الزامی می باشد !");
        
        if (value.Length is > 50 or < 3)
            throw new DomainException("فیلد نام نباید بیشتر از 50 و کمتر از 3 عبارت داشته باشد !");
        
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}