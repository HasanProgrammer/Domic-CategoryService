using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Enumerations;
using Karami.Core.Domain.ValueObjects;
using Karami.Domain.Category.Events;
using Karami.Domain.Category.ValueObjects;

#pragma warning disable CS0649

namespace Karami.Domain.Category.Entities;

public class Category : Entity<string>
{
    //Value Objects
    
    public Name Name { get; private set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private Category() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public Category(IDotrisDateTime dotrisDateTime, string id, string name)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        Id        = id;
        Name      = new Name(name);
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        IsActive  = IsActive.Active;
        
        AddEvent(
            new CategoryCreated {
                Id   = id   , 
                Name = name ,
                CreatedAt_EnglishDate = nowDateTime        ,
                UpdatedAt_EnglishDate = nowDateTime        ,
                CreatedAt_PersianDate = nowPersianDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /*---------------------------------------------------------------*/
    
    //Behaviors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="name"></param>
    public void Change(IDotrisDateTime dotrisDateTime, string name)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);
        
        Name      = new Name(name);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryUpdated {
                Id   = Id   , 
                Name = name ,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="id"></param>
    public void Delete(IDotrisDateTime dotrisDateTime, string id)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        IsDeleted = IsDeleted.Delete;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryDeleted
            {
                Id                    = id                  ,
                UpdatedAt_EnglishDate = nowDateTime         ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}